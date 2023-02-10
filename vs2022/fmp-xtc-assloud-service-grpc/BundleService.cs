
using Google.Rpc;
using Grpc.Core;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class BundleService : BundleServiceBase
    {
        private readonly SingletonServices singletonServices_;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        /// 支持多个参数，均为自动注入，注入点位于MyProgram.PreBuild
        /// </remarks>
        /// <param name="_singletonServices">自动注入的单例服务</param>
        public BundleService(SingletonServices _singletonServices)
        {
            singletonServices_ = _singletonServices;
        }

        protected override async Task<UuidResponse> safeCreate(BundleCreateRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Name, "Name");

            var bundle = await singletonServices_.getBundleDAO().FindWithNameAsync(_request.Name);
            if (null != bundle)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "bundle is exists" },
                    Uuid = bundle.Uuid.ToString(),
                };
            }

            bundle = new BundleEntity();
            bundle.Uuid = Guid.NewGuid();
            bundle.name = _request.Name;
            bundle.summary = _request.Summary;

            await singletonServices_.getBundleDAO().CreateAsync(bundle);
            await singletonServices_.getBundleDAO().PutBucketEntityToMinIO(bundle, singletonServices_.getMinioClient());

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = bundle.Uuid.ToString(),
            };
        }

        protected override async Task<UuidResponse> safeUpdate(BundleUpdateRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };
            }

            bundle.name = _request.Name;
            bundle.summary = _request.Summary;
            bundle.labelS = new string[_request.LabelS.Count];
            for (int i = 0; i < _request.LabelS.Count; ++i)
                bundle.labelS[i] = _request.LabelS[i].ToString();
            bundle.tagS = new string[_request.TagS.Count];
            for (int i = 0; i < _request.TagS.Count; ++i)
                bundle.tagS[i] = _request.TagS[i].ToString();
            foreach (var pair in _request.SummaryI18NS)
                bundle.summary_i18nS[pair.Key] = pair.Value;

            await singletonServices_.getBundleDAO().UpdateAsync(_request.Uuid, bundle);
            //将meta存入对象存储引擎中
            await singletonServices_.getBundleDAO().PutBucketEntityToMinIO(bundle, singletonServices_.getMinioClient());
            await singletonServices_.getMinioClient().GenerateManifestAsync(_request.Uuid);

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = bundle.Uuid.ToString(),
            };
        }

        protected override async Task<BundleRetrieveResponse> safeRetrieve(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new BundleRetrieveResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };
            }

            //TODO use lookup
            var response = new BundleRetrieveResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Bundle = singletonServices_.getBundleDAO().ToProtoEntity(bundle)
            };
            foreach (var contentUuid in bundle.foreign_content_uuidS)
            {
                var content = await singletonServices_.getContentDAO().GetAsync(contentUuid.ToString());
                if (null != content)
                    response.Bundle.ExterContentNameS[contentUuid.ToString()] = content.name;
            }
            return response;
        }

        protected override async Task<UuidResponse> safeDelete(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };

            }

            await singletonServices_.getBundleDAO().RemoveAsync(_request.Uuid);
            await singletonServices_.getBundleDAO().RemoveBundleFromMinIO(bundle, singletonServices_.getMinioClient());

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = _request.Uuid,
            };
        }

        protected override async Task<BundleListResponse> safeList(BundleListRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredNumber((int)_request.Count, "Count");

            var response = new BundleListResponse()
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
            };

            response.Total = await singletonServices_.getBundleDAO().CountAsync();
            var bundles = await singletonServices_.getBundleDAO().ListAsync((int)_request.Offset, (int)_request.Count);

            foreach (var bundle in bundles)
            {
                response.BundleS.Add(singletonServices_.getBundleDAO().ToProtoEntity(bundle));
            }
            return response;
        }

        protected override async Task<BundleListResponse> safeSearch(BundleSearchRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredNumber((int)_request.Count, "Count");

            var response = new BundleListResponse()
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
            };

            var result = await singletonServices_.getBundleDAO().SearchAsync((int)_request.Offset, (int)_request.Count, _request.Name, _request.LabelS.ToArray(), _request.TagS.ToArray());

            response.Total = result.Key;
            foreach (var bundle in result.Value)
            {
                response.BundleS.Add(singletonServices_.getBundleDAO().ToProtoEntity(bundle));
            }
            return response;
        }

        protected override async Task<PrepareUploadResponse> safePrepareUploadResource(PrepareUploadRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");
            ArgumentChecker.CheckRequiredString(_request.Filepath, "Filepath");

            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new PrepareUploadResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            string filepath = String.Format("{0}/_resources/{1}", bundle.Uuid.ToString(), _request.Filepath);

            var response = new PrepareUploadResponse()
            {
                Status = new LIB.Proto.Status(),
            };

            response.Filepath = _request.Filepath;
            // 有效期1小时
            response.Url = await singletonServices_.getMinioClient().PresignedPutObject(filepath, 60 * 60);
            return response;
        }

        protected override async Task<FlushUploadResponse> safeFlushUploadResource(FlushUploadRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");
            ArgumentChecker.CheckRequiredString(_request.Filepath, "Filepath");

            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new FlushUploadResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            string filepath = String.Format("{0}/_resources/{1}", bundle.Uuid.ToString(), _request.Filepath);
            var result = await singletonServices_.getMinioClient().StateObject(filepath);
            var url = singletonServices_.getMinioClient().GetAddressUrl(filepath);

            List<FileSubEntity> resourceS = new List<FileSubEntity>(bundle.resourceS);
            var resource = resourceS.Find((_item) =>
            {
                return _item.path.Equals(_request.Filepath);
            });
            if (null == resource)
            {
                resource = new FileSubEntity();
                resourceS.Add(resource);
            }
            resource.path = _request.Filepath;
            resource.hash = result.Key;
            resource.size = result.Value;
            bundle.resourceS = resourceS.ToArray();
            await singletonServices_.getBundleDAO().UpdateAsync(_request.Uuid, bundle);

            return new FlushUploadResponse()
            {
                Status = new LIB.Proto.Status(),
                Filepath = _request.Filepath,
                Hash = result.Key,
                Size = result.Value,
                Url = url,
            };
        }

        protected override async Task<BundleFetchResourcesResponse> safeFetchResources(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new BundleFetchResourcesResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            var response = new BundleFetchResourcesResponse()
            {
                Status = new LIB.Proto.Status(),
                Uuid = bundle.Uuid.ToString(),
            };
            foreach (var resource in bundle.resourceS)
            {
                response.ResourceS.Add(new LIB.Proto.FileSubEntity
                {
                    Path = resource.path,
                    Hash = resource.hash,
                    Size = resource.size,
                });
            }
            return response;
        }

        protected override async Task<UuidResponse> safeTidy(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };

            }

            var contentUuidS = new List<Guid>();
            using (var cursor = await singletonServices_.getContentDAO().AggregateListAsync(0, int.MaxValue, _request.Uuid))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var doc in batch)
                    {
                        try
                        {
                            var extraEntity = BsonSerializer.Deserialize<ExtraContentEntity>(doc);
                            var content = singletonServices_.getContentDAO().ExtraToProtoEntity(extraEntity);
                            contentUuidS.Add(new Guid(content.Uuid));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            bundle.foreign_content_uuidS = contentUuidS.ToArray();

            await singletonServices_.getBundleDAO().UpdateAsync(_request.Uuid, bundle);
            //将meta存入对象存储引擎中
            await singletonServices_.getBundleDAO().PutBucketEntityToMinIO(bundle, singletonServices_.getMinioClient());
            await singletonServices_.getMinioClient().GenerateManifestAsync(_request.Uuid);

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = _request.Uuid,
            };
        }

        protected override async Task<DeleteUploadResponse> safeDeleteResource(DeleteUploadRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            // 取bundle
            var bundle = await singletonServices_.getBundleDAO().GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new DeleteUploadResponse() { Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" } };
            }

            string filepath = String.Format("{0}/_resources/{1}", bundle.Uuid.ToString(), _request.Filepath);

            // 删除resource
            List<FileSubEntity> resourceS = new List<FileSubEntity>(bundle.resourceS);
            resourceS.RemoveAll((_item) =>
            {
                return _item.path.Equals(_request.Filepath);
            });
            bundle.resourceS = resourceS.ToArray();
            // 更新数据
            await singletonServices_.getBundleDAO().UpdateAsync(_request.Uuid, bundle);

            // 更新minio
            await singletonServices_.getMinioClient().RemoveObject(filepath);

            return new DeleteUploadResponse()
            {
                Status = new LIB.Proto.Status(),
                Filepath = _request.Filepath,
            };
        }
    }
}
