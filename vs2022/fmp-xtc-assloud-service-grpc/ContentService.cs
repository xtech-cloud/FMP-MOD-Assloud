using Google.Rpc;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class ContentService : ContentServiceBase
    {
        private readonly ContentDAO contentDAO_;
        private readonly BundleDAO bundleDAO_;
        private readonly MinIOClient minioClient_;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        /// 支持多个参数，均为自动注入，注入点位于MyProgram.PreBuild
        /// </remarks>
        /// <param name="_contentDAO">自动注入的数据操作对象</param>
        public ContentService(ContentDAO _contentDAO, BundleDAO _bundleDAO, MinIOClient _minioClient)
        {
            contentDAO_ = _contentDAO;
            bundleDAO_ = _bundleDAO;
            minioClient_ = _minioClient;
        }

        protected override async Task<UuidResponse> safeCreate(ContentCreateRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.BundleUuid, "Bundle_Uuid");
            ArgumentChecker.CheckRequiredString(_request.Name, "Name");

            var bundle = await bundleDAO_.GetAsync(_request.BundleUuid);

            var content = await contentDAO_.FindWithNameAsync(_request.Name);
            if (null != content)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "content is exists" },
                    Uuid = content.Uuid.ToString(),
                };
            }

            content = new ContentEntity();
            content.Uuid = Guid.NewGuid();
            content.Bundle_Uuid = _request.BundleUuid;
            content.Bundle_Name = bundle?.Name ?? string.Empty;
            content.Name = _request.Name;

            await contentDAO_.CreateAsync(content);

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = content.Uuid.ToString(),
            };
        }

        protected override async Task<UuidResponse> safeUpdate(ContentUpdateRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var content = await contentDAO_.GetAsync(_request.Uuid);
            if (null == content)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };
            }

            content.Name = _request.Name;
            content.KV.Clear();
            foreach (var pair in _request.Kv)
                content.KV[pair.Key] = pair.Value;
            content.Alias = _request.Alias;
            content.Title = _request.Title;
            content.Caption = _request.Caption;
            content.Label = _request.Label;
            content.Topic = _request.Topic;
            content.Description = _request.Description;
            content.Alias_I18N.Clear();
            foreach (var pair in _request.AliasI18N)
                content.Alias_I18N[pair.Key] = pair.Value;
            content.Title_I18N.Clear();
            foreach (var pair in _request.TitleI18N)
                content.Title_I18N[pair.Key] = pair.Value;
            content.Caption_I18N.Clear();
            foreach (var pair in _request.CaptionI18N)
                content.Caption_I18N[pair.Key] = pair.Value;
            content.Label_I18N.Clear();
            foreach (var pair in _request.LabelI18N)
                content.Label_I18N[pair.Key] = pair.Value;
            content.Topic_I18N.Clear();
            foreach (var pair in _request.TopicI18N)
                content.Topic_I18N[pair.Key] = pair.Value;
            content.Description_I18N.Clear();
            foreach (var pair in _request.DescriptionI18N)
                content.Description_I18N[pair.Key] = pair.Value;

            content.Labels = new string[_request.Labels.Count];
            for (int i = 0; i < _request.Labels.Count; ++i)
                content.Labels[i] = _request.Labels[i].ToString();
            content.Tags = new string[_request.Tags.Count];
            for (int i = 0; i < _request.Tags.Count; ++i)
                content.Tags[i] = _request.Tags[i].ToString();

            await contentDAO_.UpdateAsync(_request.Uuid, content);

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = content.Uuid.ToString(),
            };
        }

        protected override async Task<ContentRetrieveResponse> safeRetrieve(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var content = await contentDAO_.GetAsync(_request.Uuid);
            if (null == content)
            {
                return new ContentRetrieveResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };
            }

            return new ContentRetrieveResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Content = contentDAO_.ToProtoEntity(content),
            };
        }

        protected override async Task<UuidResponse> safeDelete(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var content = await contentDAO_.GetAsync(_request.Uuid);
            if (null == content)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };

            }

            await contentDAO_.RemoveAsync(_request.Uuid);

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
                response.Total = await contentDAO_.CountAsync();
                var contents = await contentDAO_.ListAsync((int)_request.Offset, (int)_request.Count);
                foreach (var content in contents)
                {
                    response.Contents.Add(contentDAO_.ToProtoEntity(content));
                }
            }
            else
            {
                response.Total = await contentDAO_.CountAsync(_request.BundleUuid);
                var contents = await contentDAO_.ListAsync((int)_request.Offset, (int)_request.Count, _request.BundleUuid);
                foreach (var content in contents)
                {
                    response.Contents.Add(contentDAO_.ToProtoEntity(content));
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

            var result = await contentDAO_.SearchAsync((int)_request.Offset, (int)_request.Count, _request.Name, _request.Labels.ToArray(), _request.Tags.ToArray());

            response.Total = result.Key;
            foreach (var content in result.Value)
            {
                response.Contents.Add(contentDAO_.ToProtoEntity(content));
            }
            return response;
        }

        protected override async Task<PrepareUploadResponse> safePrepareUpload(PrepareUploadRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");
            ArgumentChecker.CheckRequiredString(_request.Filepath, "Filepath");

            var content = await contentDAO_.GetAsync(_request.Uuid);
            if (null == content)
            {
                return new PrepareUploadResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            string filepath = String.Format("{0}/{1}/{2}", content.Bundle_Name, content.Name, _request.Filepath);

            var response = new PrepareUploadResponse()
            {
                Status = new LIB.Proto.Status(),
            };

            response.Filepath = _request.Filepath;
            // 有效期1小时
            response.Url = await minioClient_.PresignedPutObject(filepath, 60 * 60);
            return response;
        }

        protected override async Task<FlushUploadResponse> safeFlushUpload(FlushUploadRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");
            ArgumentChecker.CheckRequiredString(_request.Filepath, "Filepath");

            var content = await contentDAO_.GetAsync(_request.Uuid);
            if (null == content)
            {
                return new FlushUploadResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            string filepath = String.Format("{0}/{1}/{2}", content.Bundle_Name, content.Name, _request.Filepath);
            var result = await minioClient_.StateObject(filepath);

            List<FileSubEntity> attachmentsS = new List<FileSubEntity>(content.Attachments);
            var attachments = attachmentsS.Find((_item) =>
            {
                return _item.Path.Equals(_request.Filepath);
            });
            if (null == attachments)
            {
                attachments = new FileSubEntity();
                attachmentsS.Add(attachments);
            }

            //TODO 处理删除掉的附件

            attachments.Path = _request.Filepath;
            attachments.Hash = result.Key;
            attachments.Size = result.Value;
            attachments.Url = "";
            content.Attachments = attachmentsS.ToArray();
            await contentDAO_.UpdateAsync(_request.Uuid, content);

            string url = minioClient_.GetAddressUrl(filepath);
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

            var content = await contentDAO_.GetAsync(_request.Uuid);
            if (null == content)
            {
                return new ContentFetchAttachmentsResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            var response = new ContentFetchAttachmentsResponse()
            {
                Status = new LIB.Proto.Status(),
                Uuid = content.Uuid.ToString(),
            };

            //TODO 协议的硬编码
            foreach (var attachment in content.Attachments)
            {
                string url = attachment.Url;
                if (string.IsNullOrEmpty(url))
                {
                    string filepath = String.Format("{0}/{1}/{2}", content.Bundle_Name, content.Name, attachment.Path);
                    url = minioClient_.GetAddressUrl(filepath);
                }
                response.Attachments.Add(new LIB.Proto.FileSubEntity
                {
                    Path = attachment.Path,
                    Hash = attachment.Hash,
                    Size = attachment.Size,
                    Url = "http://" + url,
                });
            }
            return response;
        }
    }
}
