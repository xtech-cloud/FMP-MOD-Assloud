
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!
//*************************************************************************************

using System.Net;
using Grpc.Core;
using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    /// <summary>
    /// Bundle基类
    /// </summary>
    public class BundleServiceBase : LIB.Proto.Bundle.BundleBase
    {
    

        public override async Task<UuidResponse> Create(BundleCreateRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeCreate(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<UuidResponse> Update(BundleUpdateRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeUpdate(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<BundleRetrieveResponse> Retrieve(UuidRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeRetrieve(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new BundleRetrieveResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BundleRetrieveResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<UuidResponse> Delete(UuidRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeDelete(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<BundleListResponse> List(BundleListRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeList(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new BundleListResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BundleListResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<BundleListResponse> Search(BundleSearchRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeSearch(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new BundleListResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BundleListResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<PrepareUploadResponse> PrepareUpload(PrepareUploadRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safePrepareUpload(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new PrepareUploadResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new PrepareUploadResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<FlushUploadResponse> FlushUpload(FlushUploadRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeFlushUpload(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new FlushUploadResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new FlushUploadResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<BundleFetchResourcesResponse> FetchResources(UuidRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeFetchResources(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new BundleFetchResourcesResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BundleFetchResourcesResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }

        public override async Task<UuidResponse> Tidy(UuidRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeTidy(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new UuidResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }



        protected virtual async Task<UuidResponse> safeCreate(BundleCreateRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new UuidResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<UuidResponse> safeUpdate(BundleUpdateRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new UuidResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<BundleRetrieveResponse> safeRetrieve(UuidRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new BundleRetrieveResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<UuidResponse> safeDelete(UuidRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new UuidResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<BundleListResponse> safeList(BundleListRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new BundleListResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<BundleListResponse> safeSearch(BundleSearchRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new BundleListResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<PrepareUploadResponse> safePrepareUpload(PrepareUploadRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new PrepareUploadResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<FlushUploadResponse> safeFlushUpload(FlushUploadRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new FlushUploadResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<BundleFetchResourcesResponse> safeFetchResources(UuidRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new BundleFetchResourcesResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

        protected virtual async Task<UuidResponse> safeTidy(UuidRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new UuidResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

    }
}

