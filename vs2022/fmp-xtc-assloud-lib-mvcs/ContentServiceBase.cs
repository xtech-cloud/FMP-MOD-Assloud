
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Content服务层基类
    /// </summary>
    public class ContentServiceBase : Service
    {
        public ContentServiceMock mock { get; set; } = new ContentServiceMock();
    
        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public ContentServiceBase(string _uid, string _gid) : base(_uid)
        {
            gid_ = _gid;
        }

        /// <summary>
        /// 注入GRPC通道
        /// </summary>
        /// <param name="_channel">GRPC通道</param>
        public void InjectGrpcChannel(GrpcChannel? _channel)
        {
            grpcChannel_ = _channel;
        }


        /// <summary>
        /// 调用Create
        /// </summary>
        /// <param name="_request">Create的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallCreate(ContentCreateRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call Create ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            UuidResponse? response = null;
            if (null != mock.CallCreateDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallCreateDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.CreateAsync(_request);
            }

            getModel()?.UpdateProtoCreate(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用Update
        /// </summary>
        /// <param name="_request">Update的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallUpdate(ContentUpdateRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call Update ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            UuidResponse? response = null;
            if (null != mock.CallUpdateDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallUpdateDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.UpdateAsync(_request);
            }

            getModel()?.UpdateProtoUpdate(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用Retrieve
        /// </summary>
        /// <param name="_request">Retrieve的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallRetrieve(UuidRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call Retrieve ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            ContentRetrieveResponse? response = null;
            if (null != mock.CallRetrieveDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallRetrieveDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.RetrieveAsync(_request);
            }

            getModel()?.UpdateProtoRetrieve(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用Delete
        /// </summary>
        /// <param name="_request">Delete的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallDelete(UuidRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call Delete ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            UuidResponse? response = null;
            if (null != mock.CallDeleteDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallDeleteDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.DeleteAsync(_request);
            }

            getModel()?.UpdateProtoDelete(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用List
        /// </summary>
        /// <param name="_request">List的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallList(ContentListRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call List ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            ContentListResponse? response = null;
            if (null != mock.CallListDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallListDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.ListAsync(_request);
            }

            getModel()?.UpdateProtoList(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用Search
        /// </summary>
        /// <param name="_request">Search的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallSearch(ContentSearchRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call Search ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            ContentListResponse? response = null;
            if (null != mock.CallSearchDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallSearchDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.SearchAsync(_request);
            }

            getModel()?.UpdateProtoSearch(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用Match
        /// </summary>
        /// <param name="_request">Match的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallMatch(ContentMatchRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call Match ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            ContentListResponse? response = null;
            if (null != mock.CallMatchDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallMatchDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.MatchAsync(_request);
            }

            getModel()?.UpdateProtoMatch(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用PrepareUpload
        /// </summary>
        /// <param name="_request">PrepareUpload的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallPrepareUpload(PrepareUploadRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call PrepareUpload ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            PrepareUploadResponse? response = null;
            if (null != mock.CallPrepareUploadDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallPrepareUploadDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.PrepareUploadAsync(_request);
            }

            getModel()?.UpdateProtoPrepareUpload(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用FlushUpload
        /// </summary>
        /// <param name="_request">FlushUpload的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallFlushUpload(FlushUploadRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call FlushUpload ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            FlushUploadResponse? response = null;
            if (null != mock.CallFlushUploadDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallFlushUploadDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.FlushUploadAsync(_request);
            }

            getModel()?.UpdateProtoFlushUpload(response, _context);
            return Error.OK;
        }

        /// <summary>
        /// 调用FetchAttachments
        /// </summary>
        /// <param name="_request">FetchAttachments的请求</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> CallFetchAttachments(UuidRequest? _request, object? _context)
        {
            getLogger()?.Trace("Call FetchAttachments ...");
            if (null == _request)
            {
                return Error.NewNullErr("parameter:_request is null");
            }

            ContentFetchAttachmentsResponse? response = null;
            if (null != mock.CallFetchAttachmentsDelegate)
            {
                getLogger()?.Trace("use mock ...");
                response = await mock.CallFetchAttachmentsDelegate(_request);
            }
            else
            {
                var client = getGrpcClient();
                if (null == client)
                {
                    return await Task.FromResult(Error.NewNullErr("client is null"));
                }
                response = await client.FetchAttachmentsAsync(_request);
            }

            getModel()?.UpdateProtoFetchAttachments(response, _context);
            return Error.OK;
        }


        /// <summary>
        /// 获取直系数据层
        /// </summary>
        /// <returns>数据层</returns>
        protected ContentModel? getModel()
        {
            if(null == model_)
                model_ = findModel(ContentModel.NAME + "." + gid_) as ContentModel;
            return model_;
        }

        /// <summary>
        /// 获取GRPC客户端
        /// </summary>
        /// <returns>GRPC客户端</returns>
        protected Content.ContentClient? getGrpcClient()
        {
            if (null == grpcChannel_)
                return null;

            if(null == clientContent_)
            {
                clientContent_ = new Content.ContentClient(grpcChannel_);
            }
            return clientContent_;
        }

        /// <summary>
        /// 直系的MVCS的四个组件的组的ID
        /// </summary>
        protected string gid_ = "";

        /// <summary>
        /// GRPC客户端
        /// </summary>
        protected Content.ContentClient? clientContent_;

        /// <summary>
        /// GRPC通道
        /// </summary>
        protected GrpcChannel? grpcChannel_;

        /// <summary>
        /// 直系数据层
        /// </summary>
        private ContentModel? model_;
    }

}
