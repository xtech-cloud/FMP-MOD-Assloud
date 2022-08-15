
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.12.0.  DO NOT EDIT!
//*************************************************************************************

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
        /// 处理Match的提交
        /// </summary>
        /// <param name="_dto">ContentMatchRequest的数据传输对象</param>
        /// <returns>错误</returns>
        public virtual async Task<Error> OnMatchSubmit(IDTO _dto)
        {
            ContentMatchRequestDTO? dto = _dto as ContentMatchRequestDTO;
            if(null == service)
            {
                return Error.NewNullErr("service is null");
            }
            return await service.CallMatch(dto?.Value);
        }


    }
}
