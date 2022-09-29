
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.56.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using XTC.FMP.LIB.MVCS;

namespace XTC.FMP.MOD.Assloud.LIB.Bridge
{
    /// <summary>
    /// Content的UI桥接层（协议部分）
    /// 刷新从视图收到的数据
    /// </summary>
    public interface IContentUiProtoBridge : View.Facade.Bridge
    {
        /// <summary>
        /// 全局警告
        /// </summary>
        /// <param name="_code">错误码</param>
        /// <param name="_code">错误信息</param>
        void Alert(string _code, string _message, object? _context);

        /// <summary>
        /// 刷新Create的数据
        /// </summary>
        void RefreshCreate(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新Update的数据
        /// </summary>
        void RefreshUpdate(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新Retrieve的数据
        /// </summary>
        void RefreshRetrieve(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新Delete的数据
        /// </summary>
        void RefreshDelete(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新List的数据
        /// </summary>
        void RefreshList(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新Search的数据
        /// </summary>
        void RefreshSearch(IDTO _dto, object? _context);


        /// <summary>
        /// 刷新Match的数据
        /// </summary>
        void RefreshMatch(IDTO _dto, object? _context);


    }
}

