
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.12.0.  DO NOT EDIT!
//*************************************************************************************

using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Bridge;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Designer视图层基类
    /// </summary>
    public class DesignerViewBase : View
    {
        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public DesignerViewBase(string _uid, string _gid) : base(_uid)
        {
            gid_ = _gid;
        }


        /// <summary>
        /// 刷新ReadStyleSheet的数据
        /// </summary>
        /// <param name="_err">错误</param>
        /// <param name="_dto">DesignerReadStylesResponse的数据传输对象</param>
        public void RefreshProtoReadStyleSheet(Error _err, DesignerReadStylesResponseDTO _dto)
        {
            var bridge = getFacade()?.getUiBridge() as IDesignerUiBridge; 
            if (!Error.IsOK(_err))
            {
                bridge?.Alert(string.Format("errcode_ReadStyleSheet_{0}", _err.getCode()), _err.getMessage());
                return;
            }
            bridge?.RefreshReadStyleSheet(_dto);
        }

        /// <summary>
        /// 刷新WriteStyle的数据
        /// </summary>
        /// <param name="_err">错误</param>
        /// <param name="_dto">BlankResponse的数据传输对象</param>
        public void RefreshProtoWriteStyle(Error _err, BlankResponseDTO _dto)
        {
            var bridge = getFacade()?.getUiBridge() as IDesignerUiBridge; 
            if (!Error.IsOK(_err))
            {
                bridge?.Alert(string.Format("errcode_WriteStyle_{0}", _err.getCode()), _err.getMessage());
                return;
            }
            bridge?.RefreshWriteStyle(_dto);
        }

        /// <summary>
        /// 刷新ReadInstances的数据
        /// </summary>
        /// <param name="_err">错误</param>
        /// <param name="_dto">DesignerReadInstancesResponse的数据传输对象</param>
        public void RefreshProtoReadInstances(Error _err, DesignerReadInstancesResponseDTO _dto)
        {
            var bridge = getFacade()?.getUiBridge() as IDesignerUiBridge; 
            if (!Error.IsOK(_err))
            {
                bridge?.Alert(string.Format("errcode_ReadInstances_{0}", _err.getCode()), _err.getMessage());
                return;
            }
            bridge?.RefreshReadInstances(_dto);
        }

        /// <summary>
        /// 刷新WriteInstances的数据
        /// </summary>
        /// <param name="_err">错误</param>
        /// <param name="_dto">BlankResponse的数据传输对象</param>
        public void RefreshProtoWriteInstances(Error _err, BlankResponseDTO _dto)
        {
            var bridge = getFacade()?.getUiBridge() as IDesignerUiBridge; 
            if (!Error.IsOK(_err))
            {
                bridge?.Alert(string.Format("errcode_WriteInstances_{0}", _err.getCode()), _err.getMessage());
                return;
            }
            bridge?.RefreshWriteInstances(_dto);
        }


        /// <summary>
        /// 获取直系数据层
        /// </summary>
        /// <returns>数据层</returns>
        protected DesignerModel? getModel()
        {
            if(null == model_)
                model_ = findModel(DesignerModel.NAME + "." + gid_) as DesignerModel;
            return model_;
        }

        /// <summary>
        /// 获取直系服务层
        /// </summary>
        /// <returns>服务层</returns>
        protected DesignerService? getService()
        {
            if(null == service_)
                service_ = findService(DesignerService.NAME + "." + gid_) as DesignerService;
            return service_;
        }

        /// <summary>
        /// 获取直系UI装饰层
        /// </summary>
        /// <returns>UI装饰层</returns>
        protected DesignerFacade? getFacade()
        {
            if(null == facade_)
                facade_ = findFacade(DesignerFacade.NAME + "." + gid_) as DesignerFacade;
            return facade_;
        }

        /// <summary>
        /// 直系的MVCS的四个组件的组的ID
        /// </summary>
        protected string gid_ = "";

        /// <summary>
        /// 直系数据层
        /// </summary>
        private DesignerModel? model_;

        /// <summary>
        /// 直系服务层
        /// </summary>
        private DesignerService? service_;

        /// <summary>
        /// 直系UI装饰层
        /// </summary>
        private DesignerFacade? facade_;
    }
}
