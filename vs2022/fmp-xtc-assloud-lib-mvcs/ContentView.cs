using System.IO;
using System.Collections.Generic;
using XTC.FMP.LIB.MVCS;
using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using System.Threading;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Content视图层
    /// </summary>
    public class ContentView : ContentViewBase
    {
        /// <summary>
        /// 完整名称
        /// </summary>
        public const string NAME = "XTC.FMP.MOD.Assloud.LIB.MVCS.ContentView";

        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public ContentView(string _uid, string _gid) : base(_uid, _gid)
        {
        }

        protected override void setup()
        {
            base.setup();
            addSubscriber(Subjects.MountDisk, handleMountDisk);
        }

        private void handleMountDisk(Model.Status? _status, object _data)
        {
            string gid = "";
            string dir = "";
            try
            {
                Dictionary<string, object>? data = _data as Dictionary<string, object>;
                if (null != data)
                {
                    dir = (string)data["dir"];
                    gid = (string)data["gid"];
                }
            }
            catch (System.Exception ex)
            {
                getLogger()?.Exception(ex);
                return;
            }

            if (!gid.Equals(gid_))
                return;

            SynchronizationContext context = SynchronizationContext.Current;
            MockService.Instance.logger = getLogger();
            Error err = MockService.Instance.MountDisk(dir);
            if (!Error.IsOK(err))
            {
                var bridge = getFacade()?.getUiBridge() as IContentUiBridge;
                bridge?.Alert(err.getCode().ToString(), err.getMessage(), context);
            }
            // 挂在本地磁盘数据口，网络服务使用mock形式
            var service = getService();
            if (null == service)
                return;
            service.mock.CallMatchDelegate = MockService.Instance.CallMatch;
            getModel()?.Publish(Subjects.OnMountDisk, _data);
        }
    }
}


