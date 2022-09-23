
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.36.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using System.Threading.Tasks;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Bridge;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Designer的视图桥接层基类（协议部分）
    /// 处理UI的事件
    /// </summary>
    public class DesignerViewBridgeBase : IDesignerViewBridge
    {

        /// <summary>
        /// 直系服务层
        /// </summary>
        public DesignerService? service { get; set; }


        /// <summary>
        /// 处理ReadStyleSheet的提交
        /// </summary>
        /// <param name="_dto">ScopeRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnReadStyleSheetSubmit(IDTO _dto, object? _context)
        {
            ScopeRequestDTO? dto = _dto as ScopeRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallReadStyleSheet(dto?.Value, _context);
        }

        /// <summary>
        /// 处理WriteStyle的提交
        /// </summary>
        /// <param name="_dto">DesignerWriteStylesRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnWriteStyleSubmit(IDTO _dto, object? _context)
        {
            DesignerWriteStylesRequestDTO? dto = _dto as DesignerWriteStylesRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallWriteStyle(dto?.Value, _context);
        }

        /// <summary>
        /// 处理ReadInstances的提交
        /// </summary>
        /// <param name="_dto">ScopeRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnReadInstancesSubmit(IDTO _dto, object? _context)
        {
            ScopeRequestDTO? dto = _dto as ScopeRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallReadInstances(dto?.Value, _context);
        }

        /// <summary>
        /// 处理WriteInstances的提交
        /// </summary>
        /// <param name="_dto">DesignerWriteInstancesRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnWriteInstancesSubmit(IDTO _dto, object? _context)
        {
            DesignerWriteInstancesRequestDTO? dto = _dto as DesignerWriteInstancesRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallWriteInstances(dto?.Value, _context);
        }


    }
}
