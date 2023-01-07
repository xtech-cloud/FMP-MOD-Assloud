using Google.Rpc;
using Grpc.Core;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class ContentService : ContentServiceBase
    {
        private readonly SingletonServices singletonServices_;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        /// 支持多个参数，均为自动注入，注入点位于MyProgram.PreBuild
        /// </remarks>
        /// <param name="_singletonServices">自动注入的单例服务</param>
        public ContentService(SingletonServices _singletonServices)
        {
            singletonServices_ = _singletonServices;
        }

        protected override async Task<UuidResponse> safeCreate(ContentCreateRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.BundleUuid, "Bundle_Uuid");
            ArgumentChecker.CheckRequiredString(_request.Name, "Name");

            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.BundleUuid);
            if (null == bundle)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "bundle not found" },
                };
            }

            var content = await singletonServices_.getContentDAO().FindWithNameAsync(_request.Name);
            if (null != content)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "content is exists" },
                    Uuid = content.Uuid.ToString(),
                };
            }

            Guid contentGuid = Guid.NewGuid();
            content = new ContentEntity();
            content.Uuid = contentGuid;
            content.foreign_bundle_uuid = Guid.Parse(_request.BundleUuid);
            content.name = _request.Name;

            await singletonServices_.getContentDAO().CreateAsync(content);
            await singletonServices_.getContentDAO().PutContentEntityToMinIO(content, singletonServices_.getMinioClient());

            // 更新Bundle
            List<Guid> contentS = new List<Guid>(bundle.foreign_content_uuidS);
            if (!contentS.Contains(contentGuid))
            {
                contentS.Add(contentGuid);
            }
            bundle.foreign_content_uuidS = contentS.ToArray();
            await singletonServices_.getBundleDAO().UpdateAsync(_request.BundleUuid, bundle);
            await singletonServices_.getBundleDAO().PutBucketEntityToMinIO(bundle, singletonServices_.getMinioClient());
            await singletonServices_.getMinioClient().GenerateManifestAsync(_request.BundleUuid);

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = content.Uuid.ToString(),
            };
        }

        protected override async Task<UuidResponse> safeUpdate(ContentUpdateRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var content = await singletonServices_.getContentDAO().GetAsync(_request.Uuid);
            if (null == content)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };
            }

            content.name = _request.Name;
            content.kvS.Clear();
            foreach (var pair in _request.KvS)
                content.kvS[pair.Key] = pair.Value;
            content.alias = _request.Alias;
            content.title = _request.Title;
            content.caption = _request.Caption;
            content.label = _request.Label;
            content.topic = _request.Topic;
            content.description = _request.Description;
            content.alias_i18nS.Clear();
            foreach (var pair in _request.AliasI18NS)
                content.alias_i18nS[pair.Key] = pair.Value;
            content.title_i18nS.Clear();
            foreach (var pair in _request.TitleI18NS)
                content.title_i18nS[pair.Key] = pair.Value;
            content.caption_i18nS.Clear();
            foreach (var pair in _request.CaptionI18NS)
                content.caption_i18nS[pair.Key] = pair.Value;
            content.label_i18nS.Clear();
            foreach (var pair in _request.LabelI18NS)
                content.label_i18nS[pair.Key] = pair.Value;
            content.topic_i18nS.Clear();
            foreach (var pair in _request.TopicI18NS)
                content.topic_i18nS[pair.Key] = pair.Value;
            content.description_i18nS.Clear();
            foreach (var pair in _request.DescriptionI18NS)
                content.description_i18nS[pair.Key] = pair.Value;

            content.labelS = new string[_request.LabelS.Count];
            for (int i = 0; i < _request.LabelS.Count; ++i)
                content.labelS[i] = _request.LabelS[i].ToString();
            content.tagS = new string[_request.TagS.Count];
            for (int i = 0; i < _request.TagS.Count; ++i)
                content.tagS[i] = _request.TagS[i].ToString();

            await singletonServices_.getContentDAO().UpdateAsync(_request.Uuid, content);
            await singletonServices_.getContentDAO().PutContentEntityToMinIO(content, singletonServices_.getMinioClient());
            await singletonServices_.getMinioClient().GenerateManifestAsync(content.foreign_bundle_uuid.ToString());

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = content.Uuid.ToString(),
            };
        }

        protected override async Task<ContentRetrieveResponse> safeRetrieve(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var content = await singletonServices_.getContentDAO().GetAsync(_request.Uuid);
            if (null == content)
            {
                return new ContentRetrieveResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Content Not Found" },
                };
            }

            Guid? bundleUUID = content.foreign_bundle_uuid;
            if (null == bundleUUID)
            {
                return new ContentRetrieveResponse
                {
                    Status = new LIB.Proto.Status() { Code = 2, Message = "BundleUUID is null" },
                };
            }
            var bundle = await singletonServices_.getBundleDAO().GetAsync(bundleUUID.ToString());
            if (bundle == null)
            {
                return new ContentRetrieveResponse
                {
                    Status = new LIB.Proto.Status() { Code = 3, Message = "Bundle Not Found" },
                };
            }

            var response = new ContentRetrieveResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Content = singletonServices_.getContentDAO().ToProtoEntity(content),
            };
            response.Content.ExtraBundleName = bundle.name;
            return response;
        }

        protected override async Task<UuidResponse> safeDelete(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var content = await singletonServices_.getContentDAO().GetAsync(_request.Uuid);
            if (null == content)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Content Not Found" },
                };
            }

            string? bundleUUID = content.foreign_bundle_uuid?.ToString();
            if (null == bundleUUID)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Uuid is null" },
                };
            }

            var bundle = await singletonServices_.getBundleDAO().GetAsync(bundleUUID);
            if (null == bundle)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 2, Message = "Bundle Not Found" },
                };
            }

            await singletonServices_.getContentDAO().RemoveAsync(_request.Uuid);
            await singletonServices_.getContentDAO().RemoveContentEntityFromMinIO(content, singletonServices_.getMinioClient());

            List<Guid> contentS = new List<Guid>(bundle.foreign_content_uuidS);
            Guid contentUuid = Guid.Parse(_request.Uuid);
            if (contentS.Contains(contentUuid))
            {
                contentS.Remove(contentUuid);
            }
            bundle.foreign_content_uuidS = contentS.ToArray();
            await singletonServices_.getBundleDAO().UpdateAsync(bundleUUID, bundle);
            await singletonServices_.getBundleDAO().PutBucketEntityToMinIO(bundle, singletonServices_.getMinioClient());
            await singletonServices_.getMinioClient().GenerateManifestAsync(bundleUUID.ToString());

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = _request.Uuid,
            };
        }

        protected override async Task<ContentListResponse> safeList(ContentListRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredNumber((int)_request.Count, "Count");

            var response = new ContentListResponse()
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
            };

            if (string.IsNullOrEmpty(_request.BundleUuid))
            {
                response.Total = await singletonServices_.getContentDAO().CountAsync();
            }
            else
            {
                response.Total = await singletonServices_.getContentDAO().CountAsync(_request.BundleUuid);
            }
            using (var cursor = await singletonServices_.getContentDAO().AggregateListAsync((int)_request.Offset, (int)_request.Count, _request.BundleUuid))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var content in batch)
                    {
                        try
                        {
                            var extraEntity = BsonSerializer.Deserialize<ExtraContentEntity>(content);
                            response.ContentS.Add(singletonServices_.getContentDAO().ExtraToProtoEntity(extraEntity));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            return response;
        }

        protected override async Task<ContentListResponse> safeSearch(ContentSearchRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredNumber((int)_request.Count, "Count");

            var response = new ContentListResponse()
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
            };

            var result = await singletonServices_.getContentDAO().SearchAsync((int)_request.Offset, (int)_request.Count, _request.Name, _request.LabelS.ToArray(), _request.TagS.ToArray());

            response.Total = result.Key;
            foreach (var content in result.Value)
            {
                response.ContentS.Add(singletonServices_.getContentDAO().ToProtoEntity(content));
            }
            return response;
        }

        protected override async Task<PrepareUploadResponse> safePrepareUpload(PrepareUploadRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");
            ArgumentChecker.CheckRequiredString(_request.Filepath, "Filepath");

            var content = await singletonServices_.getContentDAO().GetAsync(_request.Uuid);
            if (null == content)
            {
                return new PrepareUploadResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            string filepath = String.Format("{0}/{1}/{2}", content.foreign_bundle_uuid, content.Uuid, _request.Filepath);

            var response = new PrepareUploadResponse()
            {
                Status = new LIB.Proto.Status(),
            };

            response.Filepath = _request.Filepath;
            // 有效期1小时
            response.Url = await singletonServices_.getMinioClient().PresignedPutObject(filepath, 60 * 60);
            return response;
        }

        protected override async Task<FlushUploadResponse> safeFlushUpload(FlushUploadRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");
            ArgumentChecker.CheckRequiredString(_request.Filepath, "Filepath");

            var content = await singletonServices_.getContentDAO().GetAsync(_request.Uuid);
            if (null == content)
            {
                return new FlushUploadResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            string filepath = String.Format("{0}/{1}/{2}", content.foreign_bundle_uuid, content.Uuid, _request.Filepath);
            var result = await singletonServices_.getMinioClient().StateObject(filepath);

            List<FileSubEntity> attachmentsS = new List<FileSubEntity>(content.AttachmentS);
            var attachments = attachmentsS.Find((_item) =>
            {
                return _item.path.Equals(_request.Filepath);
            });
            if (null == attachments)
            {
                attachments = new FileSubEntity();
                attachmentsS.Add(attachments);
            }

            //TODO 处理删除掉的附件

            attachments.path = _request.Filepath;
            attachments.hash = result.Key;
            attachments.size = result.Value;
            attachments.url = "";
            content.AttachmentS = attachmentsS.ToArray();
            await singletonServices_.getContentDAO().UpdateAsync(_request.Uuid, content);
            await singletonServices_.getMinioClient().GenerateManifestAsync(content.foreign_bundle_uuid.ToString());

            string url = singletonServices_.getMinioClient().GetAddressUrl(filepath);
            return new FlushUploadResponse()
            {
                Status = new LIB.Proto.Status(),
                Filepath = _request.Filepath,
                Hash = result.Key,
                Size = result.Value,
                Url = url,
            };
        }

        protected override async Task<ContentFetchAttachmentsResponse> safeFetchAttachments(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var content = await singletonServices_.getContentDAO().GetAsync(_request.Uuid);
            if (null == content)
            {
                return new ContentFetchAttachmentsResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            var response = new ContentFetchAttachmentsResponse()
            {
                Status = new LIB.Proto.Status(),
                Uuid = content.Uuid.ToString(),
            };

            foreach (var attachment in content.AttachmentS)
            {
                string url = attachment.url;
                if (string.IsNullOrEmpty(url))
                {
                    string filepath = String.Format("{0}/{1}/{2}", content.foreign_bundle_uuid, content.Uuid, attachment.path);
                    url = singletonServices_.getMinioClient().GetAddressUrl(filepath);
                }
                response.AttachmentS.Add(new LIB.Proto.FileSubEntity
                {
                    Path = attachment.path,
                    Hash = attachment.hash,
                    Size = attachment.size,
                    Url = url,
                });
            }
            return response;
        }
    }
}
