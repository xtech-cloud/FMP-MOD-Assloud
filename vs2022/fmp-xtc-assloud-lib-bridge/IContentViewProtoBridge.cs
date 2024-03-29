
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using System.Threading.Tasks;
using XTC.FMP.LIB.MVCS;

namespace XTC.FMP.MOD.Assloud.LIB.Bridge
{
    /// <summary>
    /// Content的视图桥接层（协议部分）
    /// 处理UI的事件
    /// </summary>
    public interface IContentViewProtoBridge : View.Facade.Bridge
    {

        /// <summary>
        /// 处理Create的提交
        /// </summary>
        Task<Error> OnCreateSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理Update的提交
        /// </summary>
        Task<Error> OnUpdateSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理Retrieve的提交
        /// </summary>
        Task<Error> OnRetrieveSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理Delete的提交
        /// </summary>
        Task<Error> OnDeleteSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理List的提交
        /// </summary>
        Task<Error> OnListSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理Search的提交
        /// </summary>
        Task<Error> OnSearchSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理Match的提交
        /// </summary>
        Task<Error> OnMatchSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理PrepareUpload的提交
        /// </summary>
        Task<Error> OnPrepareUploadSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理FlushUpload的提交
        /// </summary>
        Task<Error> OnFlushUploadSubmit(IDTO _dto, object? _context);


        /// <summary>
        /// 处理FetchAttachments的提交
        /// </summary>
        Task<Error> OnFetchAttachmentsSubmit(IDTO _dto, object? _context);


    }
}

