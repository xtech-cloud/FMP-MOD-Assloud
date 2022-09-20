
using Google.Rpc;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class BundleService : BundleServiceBase
    {
        // 解开以下代码的注释，可支持数据库操作
        private readonly BundleDAO bundleDAO_;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        /// 支持多个参数，均为自动注入，注入点位于MyProgram.PreBuild
        /// </remarks>
        /// <param name="_bundleDAO">自动注入的数据操作对象</param>
        public BundleService(BundleDAO _bundleDAO)
        {
            bundleDAO_ = _bundleDAO;
        }

        protected override async Task<UuidResponse> safeCreate(BundleCreateRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Name, "Name");

            var bundle = await bundleDAO_.FindWithNameAsync(_request.Name);
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
            bundle.Name = _request.Name;
            bundle.Summary = _request.Summary;

            await bundleDAO_.CreateAsync(bundle);

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = bundle.Uuid.ToString(),
            };
        }

        protected override async Task<UuidResponse> safeUpdate(BundleUpdateRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var bundle = await bundleDAO_.GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };
            }

            bundle.Name = _request.Name;
            bundle.Summary = _request.Summary;
            bundle.Labels = new string[_request.Labels.Count];
            for (int i = 0; i < _request.Labels.Count; ++i)
                bundle.Labels[i] = _request.Labels[i].ToString();
            bundle.Tags = new string[_request.Tags.Count];
            for (int i = 0; i < _request.Tags.Count; ++i)
                bundle.Tags[i] = _request.Tags[i].ToString();
            foreach (var pair in _request.SummaryI18N)
                bundle.Summary_i18n[pair.Key] = pair.Value;

            await bundleDAO_.UpdateAsync(_request.Uuid, bundle);

            return new UuidResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Uuid = bundle.Uuid.ToString(),
            };
        }

        protected override async Task<BundleRetrieveResponse> safeRetrieve(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var bundle = await bundleDAO_.GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new BundleRetrieveResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };

            }

            return new BundleRetrieveResponse
            {
                Status = new LIB.Proto.Status() { Code = 0, Message = "" },
                Bundle = bundleDAO_.ToProtoEntity(bundle)
            };
        }

        protected override async Task<UuidResponse> safeDelete(UuidRequest _request, ServerCallContext _context)
        {
            ArgumentChecker.CheckRequiredString(_request.Uuid, "Uuid");

            var bundle = await bundleDAO_.GetAsync(_request.Uuid);
            if (null == bundle)
            {
                return new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = 1, Message = "Not Found" },
                };

            }

            await bundleDAO_.RemoveAsync(_request.Uuid);

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

            response.Total = await bundleDAO_.CountAsync();
            var bundles = await bundleDAO_.ListAsync((int)_request.Offset, (int)_request.Count);

            foreach (var bundle in bundles)
            {
                response.Bundles.Add(bundleDAO_.ToProtoEntity(bundle));
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

            var result = await bundleDAO_.SearchAsync((int)_request.Offset, (int)_request.Count, _request.Name, _request.Labels.ToArray(), _request.Tags.ToArray());

            response.Total = result.Key;
            foreach (var bundle in result.Value)
            {
                response.Bundles.Add(bundleDAO_.ToProtoEntity(bundle));
            }
            return response;
        }
    }
}
