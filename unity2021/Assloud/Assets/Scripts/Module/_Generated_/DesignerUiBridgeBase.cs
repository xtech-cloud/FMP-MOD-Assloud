
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.12.0.  DO NOT EDIT!
//*************************************************************************************

using System;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Bridge;

namespace XTC.FMP.MOD.Assloud.LIB.Unity
{

    public class DesignerUiBridgeBase : IDesignerUiBridge
    {
        public LibMVCS.Logger logger { get; set; }

        public virtual void Alert(string _code, string _message)
        {
            throw new NotImplementedException();
        }


        public virtual void RefreshReadStyleSheet(IDTO _dto)
        {
            throw new NotImplementedException();
        }

        public virtual void RefreshWriteStyle(IDTO _dto)
        {
            throw new NotImplementedException();
        }

        public virtual void RefreshReadInstances(IDTO _dto)
        {
            throw new NotImplementedException();
        }

        public virtual void RefreshWriteInstances(IDTO _dto)
        {
            throw new NotImplementedException();
        }

    }
}
