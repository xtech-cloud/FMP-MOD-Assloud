
using System;
using LibMVCS = XTC.FMP.LIB.MVCS;

namespace XTC.FMP.MOD.Assloud.LIB.Unity
{
    /// <summary>
    /// 虚拟数据
    /// </summary>
    public class DummyModel : LibMVCS.Model
    {
        public const string NAME = "XTC.FMP.MOD.Assloud.LIB.Unity.DummyModel";

        public class DummyStatus : LibMVCS.Model.Status
        {
            public const string NAME = "XTC.FMP.MOD.Assloud.LIB.Unity.DummyStatus";
        }

        public DummyModel(string _uid) : base(_uid)
        {
        }

        protected override void preSetup()
        {
            LibMVCS.Error err;
            status_ = spawnStatus<DummyStatus>(DummyStatus.NAME, out err);
            if(!LibMVCS.Error.IsOK(err))
            {
                getLogger().Error(err.getMessage());
            }
        }

        protected override void postDismantle()
        {
            LibMVCS.Error err;
            killStatus(DummyStatus.NAME, out err);
            if(!LibMVCS.Error.IsOK(err))
            {
                getLogger().Error(err.getMessage());
            }
        }
    }
}

