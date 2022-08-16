
using System;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using System.Threading;

namespace XTC.FMP.MOD.Assloud.LIB.Unity
{
    public class ContentUiBridge : ContentUiBridgeBase
    {
        public override void Alert(string _code, string _message, SynchronizationContext _context)
        {
            logger.Error(_message);
        }
    }
}
