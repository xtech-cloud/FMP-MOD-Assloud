
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.33.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Bundle服务模拟类
    /// </summary>
    public class BundleServiceMock
    {


        public System.Func<BundleCreateRequest, Task<UuidResponse>>? CallCreateDelegate { get; set; } = null;

        public System.Func<BundleUpdateRequest, Task<UuidResponse>>? CallUpdateDelegate { get; set; } = null;

        public System.Func<UuidRequest, Task<BundleRetrieveResponse>>? CallRetrieveDelegate { get; set; } = null;

        public System.Func<UuidRequest, Task<UuidResponse>>? CallDeleteDelegate { get; set; } = null;

        public System.Func<BundleListRequest, Task<BundleListResponse>>? CallListDelegate { get; set; } = null;

        public System.Func<BundleSearchRequest, Task<BundleListResponse>>? CallSearchDelegate { get; set; } = null;

    }
}
