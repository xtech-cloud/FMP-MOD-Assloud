
using System;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using XTC.FMP.MOD.Assloud.LIB.MVCS;

namespace XTC.FMP.MOD.Assloud.LIB.Unity
{
    /// <summary>
    /// 虚拟视图，用于处理消息订阅
    /// </summary>
    public class DummyView : LibMVCS.View
    {
        public const string NAME = "XTC.FMP.MOD.Assloud.LIB.Unity.DummyView";

        public DummyView(string _uid) : base(_uid)
        {
        }

        protected override void setup()
        {
            addSubscriber("/Bootloader/Step/Execute", handleBootloaderStepExecute);
        }

        private void handleBootloaderStepExecute(LibMVCS.Model.Status _status, object _data)
        {
            /*
            var signal = new LibMVCS.Signal(model_);
            signal.Connect((_status, _data) =>
            {
                getLogger().Info($"receive signal: {_data}");
            });
            signal.Emit("test");
            */
        }
    }
}

