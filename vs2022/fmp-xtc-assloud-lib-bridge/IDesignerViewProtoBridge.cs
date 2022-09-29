
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.56.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using System.Threading.Tasks;
using XTC.FMP.LIB.MVCS;

namespace XTC.FMP.MOD.Assloud.LIB.Bridge
{
    /// <summary>
    /// Designer的视图桥接层（协议部分）
    /// 处理UI的事件
    /// </summary>
    public interface IDesignerViewProtoBridge : View.Facade.Bridge
    {

        /// <summary>
        /// 处理ReadStyleSheet的提交
        /// </summary>
        Task<Error> OnReadStyleSheetSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理WriteStyle的提交
        /// </summary>
        Task<Error> OnWriteStyleSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理ReadInstances的提交
        /// </summary>
        Task<Error> OnReadInstancesSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理WriteInstances的提交
        /// </summary>
        Task<Error> OnWriteInstancesSubmit(IDTO _dto, object? _context);


    }
}

