
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.60.0.  DO NOT EDIT!
//*************************************************************************************

using System.Collections.Generic;
using Grpc.Net.Client;
using XTC.FMP.LIB.MVCS;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{

    /// <summary>
    /// 模块选项
    /// </summary>
    public class Options : UserData
    {
        /// 获取GRPC通道
        /// </summary>
        public GrpcChannel? getChannel()
        {
            return channel_;
        }

        /// <summary>
        /// 设置GRPC通道
        /// </summary>
        /// <param name="_channel">GRPC通道</param>
        public void setChannel(GrpcChannel? _channel)
        {
            channel_ = _channel;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        public Dictionary<string,string> getPermissionS()
        {
            return permissionS_;
        }

        /// <summary>
        /// 设置权限列表
        /// </summary>
        /// <param name="_permissionS">权限列表</param>
        public void setPermissionS(Dictionary<string,string> _permissionS)
        {
            permissionS_ = _permissionS;
        }

        /// <summary>
        /// GRPC通道
        /// </summary>
        private GrpcChannel? channel_;

        /// <summary>
        /// 权限列表
        /// </summary>
        private Dictionary<string, string> permissionS_ = new Dictionary<string, string>();
    }

    /// <summary>
    /// 模块入口基类
    /// </summary>
    public class EntryBase : UserData
    {
        /// <summary>
        /// 模块选项
        /// </summary>
        protected Options? options_;

        protected Dictionary<string, BundleFacade?> facadeBundleStaticMap_ = new Dictionary<string, BundleFacade?>();
        protected Dictionary<string, BundleModel?> modelBundleStaticMap_ = new Dictionary<string, BundleModel?>();
        protected Dictionary<string, BundleView?> viewBundleStaticMap_ = new Dictionary<string, BundleView?>();
        protected Dictionary<string, BundleController?> controllerBundleStaticMap_ = new Dictionary<string, BundleController?>();
        protected Dictionary<string, BundleService?> serviceBundleStaticMap_ = new Dictionary<string, BundleService?>();

        protected Dictionary<string, BundleFacade?> facadeBundleDynamicMap_ = new Dictionary<string, BundleFacade?>();
        protected Dictionary<string, BundleModel?> modelBundleDynamicMap_ = new Dictionary<string, BundleModel?>();
        protected Dictionary<string, BundleView?> viewBundleDynamicMap_ = new Dictionary<string, BundleView?>();
        protected Dictionary<string, BundleController?> controllerBundleDynamicMap_ = new Dictionary<string, BundleController?>();
        protected Dictionary<string, BundleService?> serviceBundleDynamicMap_ = new Dictionary<string, BundleService?>();

        /// <summary>
        /// 获取Bundle的UI装饰层
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <returns>UI装饰层</returns>
        public BundleFacade? getStaticBundleFacade(string _gid)
        {
            BundleFacade? facade = null;
            if (!facadeBundleStaticMap_.TryGetValue(BundleFacade.NAME + "." + _gid, out facade))
                return null;
            return facade;
        }

        /// <summary>
        /// 获取Bundle的UI装饰层
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <returns>UI装饰层</returns>
        public BundleFacade? getDynamicBundleFacade(string _gid)
        {
            BundleFacade? facade = null;
            if (!facadeBundleDynamicMap_.TryGetValue(BundleFacade.NAME + "." + _gid, out facade))
                return null;
            return facade;
        }

        protected Dictionary<string, ContentFacade?> facadeContentStaticMap_ = new Dictionary<string, ContentFacade?>();
        protected Dictionary<string, ContentModel?> modelContentStaticMap_ = new Dictionary<string, ContentModel?>();
        protected Dictionary<string, ContentView?> viewContentStaticMap_ = new Dictionary<string, ContentView?>();
        protected Dictionary<string, ContentController?> controllerContentStaticMap_ = new Dictionary<string, ContentController?>();
        protected Dictionary<string, ContentService?> serviceContentStaticMap_ = new Dictionary<string, ContentService?>();

        protected Dictionary<string, ContentFacade?> facadeContentDynamicMap_ = new Dictionary<string, ContentFacade?>();
        protected Dictionary<string, ContentModel?> modelContentDynamicMap_ = new Dictionary<string, ContentModel?>();
        protected Dictionary<string, ContentView?> viewContentDynamicMap_ = new Dictionary<string, ContentView?>();
        protected Dictionary<string, ContentController?> controllerContentDynamicMap_ = new Dictionary<string, ContentController?>();
        protected Dictionary<string, ContentService?> serviceContentDynamicMap_ = new Dictionary<string, ContentService?>();

        /// <summary>
        /// 获取Content的UI装饰层
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <returns>UI装饰层</returns>
        public ContentFacade? getStaticContentFacade(string _gid)
        {
            ContentFacade? facade = null;
            if (!facadeContentStaticMap_.TryGetValue(ContentFacade.NAME + "." + _gid, out facade))
                return null;
            return facade;
        }

        /// <summary>
        /// 获取Content的UI装饰层
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <returns>UI装饰层</returns>
        public ContentFacade? getDynamicContentFacade(string _gid)
        {
            ContentFacade? facade = null;
            if (!facadeContentDynamicMap_.TryGetValue(ContentFacade.NAME + "." + _gid, out facade))
                return null;
            return facade;
        }

        protected Dictionary<string, DesignerFacade?> facadeDesignerStaticMap_ = new Dictionary<string, DesignerFacade?>();
        protected Dictionary<string, DesignerModel?> modelDesignerStaticMap_ = new Dictionary<string, DesignerModel?>();
        protected Dictionary<string, DesignerView?> viewDesignerStaticMap_ = new Dictionary<string, DesignerView?>();
        protected Dictionary<string, DesignerController?> controllerDesignerStaticMap_ = new Dictionary<string, DesignerController?>();
        protected Dictionary<string, DesignerService?> serviceDesignerStaticMap_ = new Dictionary<string, DesignerService?>();

        protected Dictionary<string, DesignerFacade?> facadeDesignerDynamicMap_ = new Dictionary<string, DesignerFacade?>();
        protected Dictionary<string, DesignerModel?> modelDesignerDynamicMap_ = new Dictionary<string, DesignerModel?>();
        protected Dictionary<string, DesignerView?> viewDesignerDynamicMap_ = new Dictionary<string, DesignerView?>();
        protected Dictionary<string, DesignerController?> controllerDesignerDynamicMap_ = new Dictionary<string, DesignerController?>();
        protected Dictionary<string, DesignerService?> serviceDesignerDynamicMap_ = new Dictionary<string, DesignerService?>();

        /// <summary>
        /// 获取Designer的UI装饰层
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <returns>UI装饰层</returns>
        public DesignerFacade? getStaticDesignerFacade(string _gid)
        {
            DesignerFacade? facade = null;
            if (!facadeDesignerStaticMap_.TryGetValue(DesignerFacade.NAME + "." + _gid, out facade))
                return null;
            return facade;
        }

        /// <summary>
        /// 获取Designer的UI装饰层
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <returns>UI装饰层</returns>
        public DesignerFacade? getDynamicDesignerFacade(string _gid)
        {
            DesignerFacade? facade = null;
            if (!facadeDesignerDynamicMap_.TryGetValue(DesignerFacade.NAME + "." + _gid, out facade))
                return null;
            return facade;
        }

        protected Dictionary<string, HealthyFacade?> facadeHealthyStaticMap_ = new Dictionary<string, HealthyFacade?>();
        protected Dictionary<string, HealthyModel?> modelHealthyStaticMap_ = new Dictionary<string, HealthyModel?>();
        protected Dictionary<string, HealthyView?> viewHealthyStaticMap_ = new Dictionary<string, HealthyView?>();
        protected Dictionary<string, HealthyController?> controllerHealthyStaticMap_ = new Dictionary<string, HealthyController?>();
        protected Dictionary<string, HealthyService?> serviceHealthyStaticMap_ = new Dictionary<string, HealthyService?>();

        protected Dictionary<string, HealthyFacade?> facadeHealthyDynamicMap_ = new Dictionary<string, HealthyFacade?>();
        protected Dictionary<string, HealthyModel?> modelHealthyDynamicMap_ = new Dictionary<string, HealthyModel?>();
        protected Dictionary<string, HealthyView?> viewHealthyDynamicMap_ = new Dictionary<string, HealthyView?>();
        protected Dictionary<string, HealthyController?> controllerHealthyDynamicMap_ = new Dictionary<string, HealthyController?>();
        protected Dictionary<string, HealthyService?> serviceHealthyDynamicMap_ = new Dictionary<string, HealthyService?>();

        /// <summary>
        /// 获取Healthy的UI装饰层
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <returns>UI装饰层</returns>
        public HealthyFacade? getStaticHealthyFacade(string _gid)
        {
            HealthyFacade? facade = null;
            if (!facadeHealthyStaticMap_.TryGetValue(HealthyFacade.NAME + "." + _gid, out facade))
                return null;
            return facade;
        }

        /// <summary>
        /// 获取Healthy的UI装饰层
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <returns>UI装饰层</returns>
        public HealthyFacade? getDynamicHealthyFacade(string _gid)
        {
            HealthyFacade? facade = null;
            if (!facadeHealthyDynamicMap_.TryGetValue(HealthyFacade.NAME + "." + _gid, out facade))
                return null;
            return facade;
        }


        /// <summary>
        /// 注入MVCS框架
        /// </summary>
        /// <param name="_framework">MVCS框架</param>
        /// <param name="_options">模块选项</param>
        public void Inject(Framework _framework, Options _options)
        {
            framework_ = _framework;
            options_ = _options;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        public Dictionary<string, string> getPermissionS()
        {
            return options_?.getPermissionS() ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// 静态注册
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <param name="_logger">日志</param>
        /// <returns>错误</returns>
        protected Error staticRegister(string _gid, Logger? _logger)
        {
            _logger?.Trace("StaticRegister");

            if (null == framework_)
            {
                return Error.NewNullErr("framework is null");
            }

            // 注册数据层
            var modelBundle = new BundleModel(BundleModel.NAME + "." + _gid, _gid);
            modelBundleStaticMap_[BundleModel.NAME + "." + _gid] = modelBundle;
            framework_.getStaticPipe().RegisterModel(modelBundle);
            // 注册视图层
            var viewBundle = new BundleView(BundleView.NAME + "." + _gid, _gid);
            viewBundleStaticMap_[BundleView.NAME + "." + _gid] = viewBundle;
            framework_.getStaticPipe().RegisterView(viewBundle);
            // 注册控制层
            var controllerBundle = new BundleController(BundleController.NAME + "." + _gid, _gid);
            controllerBundleStaticMap_[BundleController.NAME + "." + _gid] = controllerBundle;
            framework_.getStaticPipe().RegisterController(controllerBundle);
            // 注册服务层
            var serviceBundle = new BundleService(BundleService.NAME + "." + _gid, _gid);
            serviceBundleStaticMap_[BundleService.NAME + "." + _gid] = serviceBundle;
            framework_.getStaticPipe().RegisterService(serviceBundle);
            serviceBundle.InjectGrpcChannel(options_?.getChannel());
            // 注册UI装饰层
            var facadeBundle = new BundleFacade(BundleFacade.NAME + "." + _gid, _gid);
            facadeBundleStaticMap_[BundleFacade.NAME + "." + _gid] = facadeBundle;
            var bridgeBundle = new BundleViewBridge();
            bridgeBundle.service = serviceBundle;
            facadeBundle.setViewBridge(bridgeBundle);
            framework_.getStaticPipe().RegisterFacade(facadeBundle);

            // 注册数据层
            var modelContent = new ContentModel(ContentModel.NAME + "." + _gid, _gid);
            modelContentStaticMap_[ContentModel.NAME + "." + _gid] = modelContent;
            framework_.getStaticPipe().RegisterModel(modelContent);
            // 注册视图层
            var viewContent = new ContentView(ContentView.NAME + "." + _gid, _gid);
            viewContentStaticMap_[ContentView.NAME + "." + _gid] = viewContent;
            framework_.getStaticPipe().RegisterView(viewContent);
            // 注册控制层
            var controllerContent = new ContentController(ContentController.NAME + "." + _gid, _gid);
            controllerContentStaticMap_[ContentController.NAME + "." + _gid] = controllerContent;
            framework_.getStaticPipe().RegisterController(controllerContent);
            // 注册服务层
            var serviceContent = new ContentService(ContentService.NAME + "." + _gid, _gid);
            serviceContentStaticMap_[ContentService.NAME + "." + _gid] = serviceContent;
            framework_.getStaticPipe().RegisterService(serviceContent);
            serviceContent.InjectGrpcChannel(options_?.getChannel());
            // 注册UI装饰层
            var facadeContent = new ContentFacade(ContentFacade.NAME + "." + _gid, _gid);
            facadeContentStaticMap_[ContentFacade.NAME + "." + _gid] = facadeContent;
            var bridgeContent = new ContentViewBridge();
            bridgeContent.service = serviceContent;
            facadeContent.setViewBridge(bridgeContent);
            framework_.getStaticPipe().RegisterFacade(facadeContent);

            // 注册数据层
            var modelDesigner = new DesignerModel(DesignerModel.NAME + "." + _gid, _gid);
            modelDesignerStaticMap_[DesignerModel.NAME + "." + _gid] = modelDesigner;
            framework_.getStaticPipe().RegisterModel(modelDesigner);
            // 注册视图层
            var viewDesigner = new DesignerView(DesignerView.NAME + "." + _gid, _gid);
            viewDesignerStaticMap_[DesignerView.NAME + "." + _gid] = viewDesigner;
            framework_.getStaticPipe().RegisterView(viewDesigner);
            // 注册控制层
            var controllerDesigner = new DesignerController(DesignerController.NAME + "." + _gid, _gid);
            controllerDesignerStaticMap_[DesignerController.NAME + "." + _gid] = controllerDesigner;
            framework_.getStaticPipe().RegisterController(controllerDesigner);
            // 注册服务层
            var serviceDesigner = new DesignerService(DesignerService.NAME + "." + _gid, _gid);
            serviceDesignerStaticMap_[DesignerService.NAME + "." + _gid] = serviceDesigner;
            framework_.getStaticPipe().RegisterService(serviceDesigner);
            serviceDesigner.InjectGrpcChannel(options_?.getChannel());
            // 注册UI装饰层
            var facadeDesigner = new DesignerFacade(DesignerFacade.NAME + "." + _gid, _gid);
            facadeDesignerStaticMap_[DesignerFacade.NAME + "." + _gid] = facadeDesigner;
            var bridgeDesigner = new DesignerViewBridge();
            bridgeDesigner.service = serviceDesigner;
            facadeDesigner.setViewBridge(bridgeDesigner);
            framework_.getStaticPipe().RegisterFacade(facadeDesigner);

            // 注册数据层
            var modelHealthy = new HealthyModel(HealthyModel.NAME + "." + _gid, _gid);
            modelHealthyStaticMap_[HealthyModel.NAME + "." + _gid] = modelHealthy;
            framework_.getStaticPipe().RegisterModel(modelHealthy);
            // 注册视图层
            var viewHealthy = new HealthyView(HealthyView.NAME + "." + _gid, _gid);
            viewHealthyStaticMap_[HealthyView.NAME + "." + _gid] = viewHealthy;
            framework_.getStaticPipe().RegisterView(viewHealthy);
            // 注册控制层
            var controllerHealthy = new HealthyController(HealthyController.NAME + "." + _gid, _gid);
            controllerHealthyStaticMap_[HealthyController.NAME + "." + _gid] = controllerHealthy;
            framework_.getStaticPipe().RegisterController(controllerHealthy);
            // 注册服务层
            var serviceHealthy = new HealthyService(HealthyService.NAME + "." + _gid, _gid);
            serviceHealthyStaticMap_[HealthyService.NAME + "." + _gid] = serviceHealthy;
            framework_.getStaticPipe().RegisterService(serviceHealthy);
            serviceHealthy.InjectGrpcChannel(options_?.getChannel());
            // 注册UI装饰层
            var facadeHealthy = new HealthyFacade(HealthyFacade.NAME + "." + _gid, _gid);
            facadeHealthyStaticMap_[HealthyFacade.NAME + "." + _gid] = facadeHealthy;
            var bridgeHealthy = new HealthyViewBridge();
            bridgeHealthy.service = serviceHealthy;
            facadeHealthy.setViewBridge(bridgeHealthy);
            framework_.getStaticPipe().RegisterFacade(facadeHealthy);

            return Error.OK;
        }

        /// <summary>
        /// 动态注册
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <param name="_logger">日志</param>
        /// <returns>错误</returns>
        protected Error dynamicRegister(string _gid, Logger _logger)
        {
            _logger.Trace("DynamicRegister");

            if (null == framework_)
            {
                return Error.NewNullErr("framework is null");
            }

            // 注册数据层
            var modelBundle = new BundleModel(BundleModel.NAME + "." + _gid, _gid);
            modelBundleDynamicMap_[BundleModel.NAME + "." + _gid] = modelBundle;
            framework_.getDynamicPipe().PushModel(modelBundle);
            // 注册视图层
            var viewBundle = new BundleView(BundleView.NAME + "." + _gid, _gid);
            viewBundleDynamicMap_[BundleView.NAME + "." + _gid] = viewBundle;
            framework_.getDynamicPipe().PushView(viewBundle);
            // 注册控制层
            var controllerBundle = new BundleController(BundleController.NAME + "." + _gid, _gid);
            controllerBundleDynamicMap_[BundleController.NAME + "." + _gid] = controllerBundle;
            framework_.getDynamicPipe().PushController(controllerBundle);
            // 注册服务层
            var serviceBundle = new BundleService(BundleService.NAME + "." + _gid, _gid);
            serviceBundleDynamicMap_[BundleService.NAME + "." + _gid] = serviceBundle;
            framework_.getDynamicPipe().PushService(serviceBundle);
            serviceBundle.InjectGrpcChannel(options_?.getChannel());
            // 注册UI装饰层
            var facadeBundle = new BundleFacade(BundleFacade.NAME + "." + _gid, _gid);
            facadeBundleDynamicMap_[BundleFacade.NAME + "." + _gid] = facadeBundle;
            var bridgeBundle = new BundleViewBridge();
            bridgeBundle.service = serviceBundle;
            facadeBundle.setViewBridge(bridgeBundle);
            framework_.getDynamicPipe().PushFacade(facadeBundle);

            // 注册数据层
            var modelContent = new ContentModel(ContentModel.NAME + "." + _gid, _gid);
            modelContentDynamicMap_[ContentModel.NAME + "." + _gid] = modelContent;
            framework_.getDynamicPipe().PushModel(modelContent);
            // 注册视图层
            var viewContent = new ContentView(ContentView.NAME + "." + _gid, _gid);
            viewContentDynamicMap_[ContentView.NAME + "." + _gid] = viewContent;
            framework_.getDynamicPipe().PushView(viewContent);
            // 注册控制层
            var controllerContent = new ContentController(ContentController.NAME + "." + _gid, _gid);
            controllerContentDynamicMap_[ContentController.NAME + "." + _gid] = controllerContent;
            framework_.getDynamicPipe().PushController(controllerContent);
            // 注册服务层
            var serviceContent = new ContentService(ContentService.NAME + "." + _gid, _gid);
            serviceContentDynamicMap_[ContentService.NAME + "." + _gid] = serviceContent;
            framework_.getDynamicPipe().PushService(serviceContent);
            serviceContent.InjectGrpcChannel(options_?.getChannel());
            // 注册UI装饰层
            var facadeContent = new ContentFacade(ContentFacade.NAME + "." + _gid, _gid);
            facadeContentDynamicMap_[ContentFacade.NAME + "." + _gid] = facadeContent;
            var bridgeContent = new ContentViewBridge();
            bridgeContent.service = serviceContent;
            facadeContent.setViewBridge(bridgeContent);
            framework_.getDynamicPipe().PushFacade(facadeContent);

            // 注册数据层
            var modelDesigner = new DesignerModel(DesignerModel.NAME + "." + _gid, _gid);
            modelDesignerDynamicMap_[DesignerModel.NAME + "." + _gid] = modelDesigner;
            framework_.getDynamicPipe().PushModel(modelDesigner);
            // 注册视图层
            var viewDesigner = new DesignerView(DesignerView.NAME + "." + _gid, _gid);
            viewDesignerDynamicMap_[DesignerView.NAME + "." + _gid] = viewDesigner;
            framework_.getDynamicPipe().PushView(viewDesigner);
            // 注册控制层
            var controllerDesigner = new DesignerController(DesignerController.NAME + "." + _gid, _gid);
            controllerDesignerDynamicMap_[DesignerController.NAME + "." + _gid] = controllerDesigner;
            framework_.getDynamicPipe().PushController(controllerDesigner);
            // 注册服务层
            var serviceDesigner = new DesignerService(DesignerService.NAME + "." + _gid, _gid);
            serviceDesignerDynamicMap_[DesignerService.NAME + "." + _gid] = serviceDesigner;
            framework_.getDynamicPipe().PushService(serviceDesigner);
            serviceDesigner.InjectGrpcChannel(options_?.getChannel());
            // 注册UI装饰层
            var facadeDesigner = new DesignerFacade(DesignerFacade.NAME + "." + _gid, _gid);
            facadeDesignerDynamicMap_[DesignerFacade.NAME + "." + _gid] = facadeDesigner;
            var bridgeDesigner = new DesignerViewBridge();
            bridgeDesigner.service = serviceDesigner;
            facadeDesigner.setViewBridge(bridgeDesigner);
            framework_.getDynamicPipe().PushFacade(facadeDesigner);

            // 注册数据层
            var modelHealthy = new HealthyModel(HealthyModel.NAME + "." + _gid, _gid);
            modelHealthyDynamicMap_[HealthyModel.NAME + "." + _gid] = modelHealthy;
            framework_.getDynamicPipe().PushModel(modelHealthy);
            // 注册视图层
            var viewHealthy = new HealthyView(HealthyView.NAME + "." + _gid, _gid);
            viewHealthyDynamicMap_[HealthyView.NAME + "." + _gid] = viewHealthy;
            framework_.getDynamicPipe().PushView(viewHealthy);
            // 注册控制层
            var controllerHealthy = new HealthyController(HealthyController.NAME + "." + _gid, _gid);
            controllerHealthyDynamicMap_[HealthyController.NAME + "." + _gid] = controllerHealthy;
            framework_.getDynamicPipe().PushController(controllerHealthy);
            // 注册服务层
            var serviceHealthy = new HealthyService(HealthyService.NAME + "." + _gid, _gid);
            serviceHealthyDynamicMap_[HealthyService.NAME + "." + _gid] = serviceHealthy;
            framework_.getDynamicPipe().PushService(serviceHealthy);
            serviceHealthy.InjectGrpcChannel(options_?.getChannel());
            // 注册UI装饰层
            var facadeHealthy = new HealthyFacade(HealthyFacade.NAME + "." + _gid, _gid);
            facadeHealthyDynamicMap_[HealthyFacade.NAME + "." + _gid] = facadeHealthy;
            var bridgeHealthy = new HealthyViewBridge();
            bridgeHealthy.service = serviceHealthy;
            facadeHealthy.setViewBridge(bridgeHealthy);
            framework_.getDynamicPipe().PushFacade(facadeHealthy);

            return Error.OK;
        }

        /// <summary>
        /// 静态注销
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <param name="_logger">日志</param>
        /// <returns>错误</returns>
        protected Error staticCancel(string _gid, Logger _logger)
        {
            _logger?.Trace("StaticCancel");

            if (null == framework_)
            {
                return Error.NewNullErr("framework is null");
            }

            // 注销服务层
            BundleService? serviceBundle;
            if(serviceBundleStaticMap_.TryGetValue(BundleService.NAME + "." + _gid, out serviceBundle))
            {
                framework_.getStaticPipe().CancelService(serviceBundle);
                serviceBundleStaticMap_.Remove(BundleService.NAME + "." +_gid);
            }
            // 注销控制层
            BundleController? controllerBundle;
            if(controllerBundleStaticMap_.TryGetValue(BundleController.NAME + "." + _gid, out controllerBundle))
            {
                framework_.getStaticPipe().CancelController(controllerBundle);
                controllerBundleStaticMap_.Remove(BundleController.NAME + "." +_gid);
            }
            // 注销视图层
            BundleView? viewBundle;
            if(viewBundleStaticMap_.TryGetValue(BundleView.NAME + "." + _gid, out viewBundle))
            {
                framework_.getStaticPipe().CancelView(viewBundle);
                viewBundleStaticMap_.Remove(BundleView.NAME + "." +_gid);
            }
            // 注销UI装饰层
            BundleFacade? facadeBundle;
            if(facadeBundleStaticMap_.TryGetValue(BundleFacade.NAME + "." + _gid, out facadeBundle))
            {
                framework_.getStaticPipe().CancelFacade(facadeBundle);
                facadeBundleStaticMap_.Remove(BundleFacade.NAME + "." +_gid);
            }
            // 注销数据层
            BundleModel? modelBundle;
            if(modelBundleStaticMap_.TryGetValue(BundleModel.NAME + "." + _gid, out modelBundle))
            {
                framework_.getStaticPipe().CancelModel(modelBundle);
                modelBundleStaticMap_.Remove(BundleModel.NAME + "." +_gid);
            }

            // 注销服务层
            ContentService? serviceContent;
            if(serviceContentStaticMap_.TryGetValue(ContentService.NAME + "." + _gid, out serviceContent))
            {
                framework_.getStaticPipe().CancelService(serviceContent);
                serviceContentStaticMap_.Remove(ContentService.NAME + "." +_gid);
            }
            // 注销控制层
            ContentController? controllerContent;
            if(controllerContentStaticMap_.TryGetValue(ContentController.NAME + "." + _gid, out controllerContent))
            {
                framework_.getStaticPipe().CancelController(controllerContent);
                controllerContentStaticMap_.Remove(ContentController.NAME + "." +_gid);
            }
            // 注销视图层
            ContentView? viewContent;
            if(viewContentStaticMap_.TryGetValue(ContentView.NAME + "." + _gid, out viewContent))
            {
                framework_.getStaticPipe().CancelView(viewContent);
                viewContentStaticMap_.Remove(ContentView.NAME + "." +_gid);
            }
            // 注销UI装饰层
            ContentFacade? facadeContent;
            if(facadeContentStaticMap_.TryGetValue(ContentFacade.NAME + "." + _gid, out facadeContent))
            {
                framework_.getStaticPipe().CancelFacade(facadeContent);
                facadeContentStaticMap_.Remove(ContentFacade.NAME + "." +_gid);
            }
            // 注销数据层
            ContentModel? modelContent;
            if(modelContentStaticMap_.TryGetValue(ContentModel.NAME + "." + _gid, out modelContent))
            {
                framework_.getStaticPipe().CancelModel(modelContent);
                modelContentStaticMap_.Remove(ContentModel.NAME + "." +_gid);
            }

            // 注销服务层
            DesignerService? serviceDesigner;
            if(serviceDesignerStaticMap_.TryGetValue(DesignerService.NAME + "." + _gid, out serviceDesigner))
            {
                framework_.getStaticPipe().CancelService(serviceDesigner);
                serviceDesignerStaticMap_.Remove(DesignerService.NAME + "." +_gid);
            }
            // 注销控制层
            DesignerController? controllerDesigner;
            if(controllerDesignerStaticMap_.TryGetValue(DesignerController.NAME + "." + _gid, out controllerDesigner))
            {
                framework_.getStaticPipe().CancelController(controllerDesigner);
                controllerDesignerStaticMap_.Remove(DesignerController.NAME + "." +_gid);
            }
            // 注销视图层
            DesignerView? viewDesigner;
            if(viewDesignerStaticMap_.TryGetValue(DesignerView.NAME + "." + _gid, out viewDesigner))
            {
                framework_.getStaticPipe().CancelView(viewDesigner);
                viewDesignerStaticMap_.Remove(DesignerView.NAME + "." +_gid);
            }
            // 注销UI装饰层
            DesignerFacade? facadeDesigner;
            if(facadeDesignerStaticMap_.TryGetValue(DesignerFacade.NAME + "." + _gid, out facadeDesigner))
            {
                framework_.getStaticPipe().CancelFacade(facadeDesigner);
                facadeDesignerStaticMap_.Remove(DesignerFacade.NAME + "." +_gid);
            }
            // 注销数据层
            DesignerModel? modelDesigner;
            if(modelDesignerStaticMap_.TryGetValue(DesignerModel.NAME + "." + _gid, out modelDesigner))
            {
                framework_.getStaticPipe().CancelModel(modelDesigner);
                modelDesignerStaticMap_.Remove(DesignerModel.NAME + "." +_gid);
            }

            // 注销服务层
            HealthyService? serviceHealthy;
            if(serviceHealthyStaticMap_.TryGetValue(HealthyService.NAME + "." + _gid, out serviceHealthy))
            {
                framework_.getStaticPipe().CancelService(serviceHealthy);
                serviceHealthyStaticMap_.Remove(HealthyService.NAME + "." +_gid);
            }
            // 注销控制层
            HealthyController? controllerHealthy;
            if(controllerHealthyStaticMap_.TryGetValue(HealthyController.NAME + "." + _gid, out controllerHealthy))
            {
                framework_.getStaticPipe().CancelController(controllerHealthy);
                controllerHealthyStaticMap_.Remove(HealthyController.NAME + "." +_gid);
            }
            // 注销视图层
            HealthyView? viewHealthy;
            if(viewHealthyStaticMap_.TryGetValue(HealthyView.NAME + "." + _gid, out viewHealthy))
            {
                framework_.getStaticPipe().CancelView(viewHealthy);
                viewHealthyStaticMap_.Remove(HealthyView.NAME + "." +_gid);
            }
            // 注销UI装饰层
            HealthyFacade? facadeHealthy;
            if(facadeHealthyStaticMap_.TryGetValue(HealthyFacade.NAME + "." + _gid, out facadeHealthy))
            {
                framework_.getStaticPipe().CancelFacade(facadeHealthy);
                facadeHealthyStaticMap_.Remove(HealthyFacade.NAME + "." +_gid);
            }
            // 注销数据层
            HealthyModel? modelHealthy;
            if(modelHealthyStaticMap_.TryGetValue(HealthyModel.NAME + "." + _gid, out modelHealthy))
            {
                framework_.getStaticPipe().CancelModel(modelHealthy);
                modelHealthyStaticMap_.Remove(HealthyModel.NAME + "." +_gid);
            }

            return Error.OK;
        }

        /// <summary>
        /// 动态注销
        /// </summary>
        /// <param name="_gid">直系的组的ID</param>
        /// <param name="_logger">日志</param>
        /// <returns>错误</returns>
        protected Error dynamicCancel(string _gid, Logger _logger)
        {
            _logger?.Trace("DynamicCancel");

            if (null == framework_)
            {
                return Error.NewNullErr("framework is null");
            }

            // 注销服务层
            BundleService? serviceBundle;
            if(serviceBundleDynamicMap_.TryGetValue(BundleService.NAME + "." + _gid, out serviceBundle))
            {
                framework_.getDynamicPipe().PopService(serviceBundle);
                serviceBundleDynamicMap_.Remove(BundleService.NAME + "." +_gid);
            }
            // 注销控制层
            BundleController? controllerBundle;
            if(controllerBundleDynamicMap_.TryGetValue(BundleController.NAME + "." + _gid, out controllerBundle))
            {
                framework_.getDynamicPipe().PopController(controllerBundle);
                controllerBundleDynamicMap_.Remove(BundleController.NAME + "." +_gid);
            }
            // 注销视图层
            BundleView? viewBundle;
            if(viewBundleDynamicMap_.TryGetValue(BundleView.NAME + "." + _gid, out viewBundle))
            {
                framework_.getDynamicPipe().PopView(viewBundle);
                viewBundleDynamicMap_.Remove(BundleView.NAME + "." +_gid);
            }
            // 注销UI装饰层
            BundleFacade? facadeBundle;
            if(facadeBundleDynamicMap_.TryGetValue(BundleFacade.NAME + "." + _gid, out facadeBundle))
            {
                framework_.getDynamicPipe().PopFacade(facadeBundle);
                facadeBundleDynamicMap_.Remove(BundleFacade.NAME + "." +_gid);
            }
            // 注销数据层
            BundleModel? modelBundle;
            if(modelBundleDynamicMap_.TryGetValue(BundleModel.NAME + "." + _gid, out modelBundle))
            {
                framework_.getDynamicPipe().PopModel(modelBundle);
                modelBundleDynamicMap_.Remove(BundleModel.NAME + "." +_gid);
            }

            // 注销服务层
            ContentService? serviceContent;
            if(serviceContentDynamicMap_.TryGetValue(ContentService.NAME + "." + _gid, out serviceContent))
            {
                framework_.getDynamicPipe().PopService(serviceContent);
                serviceContentDynamicMap_.Remove(ContentService.NAME + "." +_gid);
            }
            // 注销控制层
            ContentController? controllerContent;
            if(controllerContentDynamicMap_.TryGetValue(ContentController.NAME + "." + _gid, out controllerContent))
            {
                framework_.getDynamicPipe().PopController(controllerContent);
                controllerContentDynamicMap_.Remove(ContentController.NAME + "." +_gid);
            }
            // 注销视图层
            ContentView? viewContent;
            if(viewContentDynamicMap_.TryGetValue(ContentView.NAME + "." + _gid, out viewContent))
            {
                framework_.getDynamicPipe().PopView(viewContent);
                viewContentDynamicMap_.Remove(ContentView.NAME + "." +_gid);
            }
            // 注销UI装饰层
            ContentFacade? facadeContent;
            if(facadeContentDynamicMap_.TryGetValue(ContentFacade.NAME + "." + _gid, out facadeContent))
            {
                framework_.getDynamicPipe().PopFacade(facadeContent);
                facadeContentDynamicMap_.Remove(ContentFacade.NAME + "." +_gid);
            }
            // 注销数据层
            ContentModel? modelContent;
            if(modelContentDynamicMap_.TryGetValue(ContentModel.NAME + "." + _gid, out modelContent))
            {
                framework_.getDynamicPipe().PopModel(modelContent);
                modelContentDynamicMap_.Remove(ContentModel.NAME + "." +_gid);
            }

            // 注销服务层
            DesignerService? serviceDesigner;
            if(serviceDesignerDynamicMap_.TryGetValue(DesignerService.NAME + "." + _gid, out serviceDesigner))
            {
                framework_.getDynamicPipe().PopService(serviceDesigner);
                serviceDesignerDynamicMap_.Remove(DesignerService.NAME + "." +_gid);
            }
            // 注销控制层
            DesignerController? controllerDesigner;
            if(controllerDesignerDynamicMap_.TryGetValue(DesignerController.NAME + "." + _gid, out controllerDesigner))
            {
                framework_.getDynamicPipe().PopController(controllerDesigner);
                controllerDesignerDynamicMap_.Remove(DesignerController.NAME + "." +_gid);
            }
            // 注销视图层
            DesignerView? viewDesigner;
            if(viewDesignerDynamicMap_.TryGetValue(DesignerView.NAME + "." + _gid, out viewDesigner))
            {
                framework_.getDynamicPipe().PopView(viewDesigner);
                viewDesignerDynamicMap_.Remove(DesignerView.NAME + "." +_gid);
            }
            // 注销UI装饰层
            DesignerFacade? facadeDesigner;
            if(facadeDesignerDynamicMap_.TryGetValue(DesignerFacade.NAME + "." + _gid, out facadeDesigner))
            {
                framework_.getDynamicPipe().PopFacade(facadeDesigner);
                facadeDesignerDynamicMap_.Remove(DesignerFacade.NAME + "." +_gid);
            }
            // 注销数据层
            DesignerModel? modelDesigner;
            if(modelDesignerDynamicMap_.TryGetValue(DesignerModel.NAME + "." + _gid, out modelDesigner))
            {
                framework_.getDynamicPipe().PopModel(modelDesigner);
                modelDesignerDynamicMap_.Remove(DesignerModel.NAME + "." +_gid);
            }

            // 注销服务层
            HealthyService? serviceHealthy;
            if(serviceHealthyDynamicMap_.TryGetValue(HealthyService.NAME + "." + _gid, out serviceHealthy))
            {
                framework_.getDynamicPipe().PopService(serviceHealthy);
                serviceHealthyDynamicMap_.Remove(HealthyService.NAME + "." +_gid);
            }
            // 注销控制层
            HealthyController? controllerHealthy;
            if(controllerHealthyDynamicMap_.TryGetValue(HealthyController.NAME + "." + _gid, out controllerHealthy))
            {
                framework_.getDynamicPipe().PopController(controllerHealthy);
                controllerHealthyDynamicMap_.Remove(HealthyController.NAME + "." +_gid);
            }
            // 注销视图层
            HealthyView? viewHealthy;
            if(viewHealthyDynamicMap_.TryGetValue(HealthyView.NAME + "." + _gid, out viewHealthy))
            {
                framework_.getDynamicPipe().PopView(viewHealthy);
                viewHealthyDynamicMap_.Remove(HealthyView.NAME + "." +_gid);
            }
            // 注销UI装饰层
            HealthyFacade? facadeHealthy;
            if(facadeHealthyDynamicMap_.TryGetValue(HealthyFacade.NAME + "." + _gid, out facadeHealthy))
            {
                framework_.getDynamicPipe().PopFacade(facadeHealthy);
                facadeHealthyDynamicMap_.Remove(HealthyFacade.NAME + "." +_gid);
            }
            // 注销数据层
            HealthyModel? modelHealthy;
            if(modelHealthyDynamicMap_.TryGetValue(HealthyModel.NAME + "." + _gid, out modelHealthy))
            {
                framework_.getDynamicPipe().PopModel(modelHealthy);
                modelHealthyDynamicMap_.Remove(HealthyModel.NAME + "." +_gid);
            }

            return Error.OK;
        }

        /// <summary>
        /// MVCS框架
        /// </summary>
        protected Framework? framework_;
    }
}

