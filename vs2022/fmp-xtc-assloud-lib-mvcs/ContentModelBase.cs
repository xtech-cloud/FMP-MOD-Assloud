
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.12.0.  DO NOT EDIT!
//*************************************************************************************

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
        /// 更新Match的数据
        /// </summary>
        /// <param name="_response">Match的回复</param>
        public virtual void UpdateProtoMatch(ContentListResponse _response)
        {
            getController()?.UpdateProtoMatch(status_ as ContentModel.ContentStatus, _response);
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


