
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Content数据层基类
    /// </summary>
    public class ContentModelBase : Model
    {
        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public ContentModelBase(string _uid, string _gid) : base(_uid)
        {
            gid_ = _gid;
        }


        /// <summary>
        /// 更新Create的数据
        /// </summary>
        /// <param name="_response">Create的回复</param>
        public virtual void UpdateProtoCreate(UuidResponse _response, object? _context)
        {
            getController()?.UpdateProtoCreate(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新Update的数据
        /// </summary>
        /// <param name="_response">Update的回复</param>
        public virtual void UpdateProtoUpdate(UuidResponse _response, object? _context)
        {
            getController()?.UpdateProtoUpdate(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新Retrieve的数据
        /// </summary>
        /// <param name="_response">Retrieve的回复</param>
        public virtual void UpdateProtoRetrieve(ContentRetrieveResponse _response, object? _context)
        {
            getController()?.UpdateProtoRetrieve(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新Delete的数据
        /// </summary>
        /// <param name="_response">Delete的回复</param>
        public virtual void UpdateProtoDelete(UuidResponse _response, object? _context)
        {
            getController()?.UpdateProtoDelete(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新List的数据
        /// </summary>
        /// <param name="_response">List的回复</param>
        public virtual void UpdateProtoList(ContentListResponse _response, object? _context)
        {
            getController()?.UpdateProtoList(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新Search的数据
        /// </summary>
        /// <param name="_response">Search的回复</param>
        public virtual void UpdateProtoSearch(ContentListResponse _response, object? _context)
        {
            getController()?.UpdateProtoSearch(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新Match的数据
        /// </summary>
        /// <param name="_response">Match的回复</param>
        public virtual void UpdateProtoMatch(ContentListResponse _response, object? _context)
        {
            getController()?.UpdateProtoMatch(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新PrepareUpload的数据
        /// </summary>
        /// <param name="_response">PrepareUpload的回复</param>
        public virtual void UpdateProtoPrepareUpload(PrepareUploadResponse _response, object? _context)
        {
            getController()?.UpdateProtoPrepareUpload(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新FlushUpload的数据
        /// </summary>
        /// <param name="_response">FlushUpload的回复</param>
        public virtual void UpdateProtoFlushUpload(FlushUploadResponse _response, object? _context)
        {
            getController()?.UpdateProtoFlushUpload(status_ as ContentModel.ContentStatus, _response, _context);
        }

        /// <summary>
        /// 更新FetchAttachments的数据
        /// </summary>
        /// <param name="_response">FetchAttachments的回复</param>
        public virtual void UpdateProtoFetchAttachments(ContentFetchAttachmentsResponse _response, object? _context)
        {
            getController()?.UpdateProtoFetchAttachments(status_ as ContentModel.ContentStatus, _response, _context);
        }


        /// <summary>
        /// 获取直系控制层
        /// </summary>
        /// <returns>控制层</returns>
        protected ContentController? getController()
        {
            if(null == controller_)
                controller_ = findController(ContentController.NAME + "." + gid_) as ContentController;
            return controller_;
        }

        /// <summary>
        /// 直系的MVCS的四个组件的组的ID
        /// </summary>
        protected string gid_ = "";

        /// <summary>
        /// 直系控制层
        /// </summary>
        private ContentController? controller_;
    }
}


