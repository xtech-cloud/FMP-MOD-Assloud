
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using System.Threading.Tasks;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Bridge;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Content的视图桥接层基类（协议部分）
    /// 处理UI的事件
    /// </summary>
    public class ContentViewBridgeBase : IContentViewBridge
    {

        /// <summary>
        /// 直系服务层
        /// </summary>
        public ContentService? service { get; set; }


        /// <summary>
        /// 处理Create的提交
        /// </summary>
        /// <param name="_dto">ContentCreateRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnCreateSubmit(IDTO _dto, object? _context)
        {
            ContentCreateRequestDTO? dto = _dto as ContentCreateRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallCreate(dto?.Value, _context);
        }

        /// <summary>
        /// 处理Update的提交
        /// </summary>
        /// <param name="_dto">ContentUpdateRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnUpdateSubmit(IDTO _dto, object? _context)
        {
            ContentUpdateRequestDTO? dto = _dto as ContentUpdateRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallUpdate(dto?.Value, _context);
        }

        /// <summary>
        /// 处理Retrieve的提交
        /// </summary>
        /// <param name="_dto">UuidRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnRetrieveSubmit(IDTO _dto, object? _context)
        {
            UuidRequestDTO? dto = _dto as UuidRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallRetrieve(dto?.Value, _context);
        }

        /// <summary>
        /// 处理Delete的提交
        /// </summary>
        /// <param name="_dto">UuidRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnDeleteSubmit(IDTO _dto, object? _context)
        {
            UuidRequestDTO? dto = _dto as UuidRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallDelete(dto?.Value, _context);
        }

        /// <summary>
        /// 处理List的提交
        /// </summary>
        /// <param name="_dto">ContentListRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnListSubmit(IDTO _dto, object? _context)
        {
            ContentListRequestDTO? dto = _dto as ContentListRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallList(dto?.Value, _context);
        }

        /// <summary>
        /// 处理Search的提交
        /// </summary>
        /// <param name="_dto">ContentSearchRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnSearchSubmit(IDTO _dto, object? _context)
        {
            ContentSearchRequestDTO? dto = _dto as ContentSearchRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallSearch(dto?.Value, _context);
        }

        /// <summary>
        /// 处理Match的提交
        /// </summary>
        /// <param name="_dto">ContentMatchRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnMatchSubmit(IDTO _dto, object? _context)
        {
            ContentMatchRequestDTO? dto = _dto as ContentMatchRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallMatch(dto?.Value, _context);
        }

        /// <summary>
        /// 处理PrepareUpload的提交
        /// </summary>
        /// <param name="_dto">PrepareUploadRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnPrepareUploadSubmit(IDTO _dto, object? _context)
        {
            PrepareUploadRequestDTO? dto = _dto as PrepareUploadRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallPrepareUpload(dto?.Value, _context);
        }

        /// <summary>
        /// 处理FlushUpload的提交
        /// </summary>
        /// <param name="_dto">FlushUploadRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnFlushUploadSubmit(IDTO _dto, object? _context)
        {
            FlushUploadRequestDTO? dto = _dto as FlushUploadRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallFlushUpload(dto?.Value, _context);
        }

        /// <summary>
        /// 处理FetchAttachments的提交
        /// </summary>
        /// <param name="_dto">UuidRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnFetchAttachmentsSubmit(IDTO _dto, object? _context)
        {
            UuidRequestDTO? dto = _dto as UuidRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallFetchAttachments(dto?.Value, _context);
        }


    }
}
