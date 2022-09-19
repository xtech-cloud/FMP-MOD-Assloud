
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.33.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Designer数据层基类
    /// </summary>
    public class DesignerModelBase : Model
    {
        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public DesignerModelBase(string _uid, string _gid) : base(_uid)
        {
            gid_ = _gid;
        }


        /// <summary>
        /// 更新ReadStyleSheet的数据
        /// </summary>
        /// <param name="_response">ReadStyleSheet的回复</param>
        public virtual void UpdateProtoReadStyleSheet(DesignerReadStylesResponse _response, object? _context)
        {
            getController()?.UpdateProtoReadStyleSheet(status_ as DesignerModel.DesignerStatus, _response, _context);
        }

        /// <summary>
        /// 更新WriteStyle的数据
        /// </summary>
        /// <param name="_response">WriteStyle的回复</param>
        public virtual void UpdateProtoWriteStyle(BlankResponse _response, object? _context)
        {
            getController()?.UpdateProtoWriteStyle(status_ as DesignerModel.DesignerStatus, _response, _context);
        }

        /// <summary>
        /// 更新ReadInstances的数据
        /// </summary>
        /// <param name="_response">ReadInstances的回复</param>
        public virtual void UpdateProtoReadInstances(DesignerReadInstancesResponse _response, object? _context)
        {
            getController()?.UpdateProtoReadInstances(status_ as DesignerModel.DesignerStatus, _response, _context);
        }

        /// <summary>
        /// 更新WriteInstances的数据
        /// </summary>
        /// <param name="_response">WriteInstances的回复</param>
        public virtual void UpdateProtoWriteInstances(BlankResponse _response, object? _context)
        {
            getController()?.UpdateProtoWriteInstances(status_ as DesignerModel.DesignerStatus, _response, _context);
        }


        /// <summary>
        /// 获取直系控制层
        /// </summary>
        /// <returns>控制层</returns>
        protected DesignerController? getController()
        {
            if(null == controller_)
                controller_ = findController(DesignerController.NAME + "." + gid_) as DesignerController;
            return controller_;
        }

        /// <summary>
        /// 直系的MVCS的四个组件的组的ID
        /// </summary>
        protected string gid_ = "";

        /// <summary>
        /// 直系控制层
        /// </summary>
        private DesignerController? controller_;
    }
}


