
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.36.0.  DO NOT EDIT!
//*************************************************************************************

using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Content服务模拟类
    /// </summary>
    public class ContentServiceMock
    {


        public System.Func<ContentCreateRequest, Task<UuidResponse>>? CallCreateDelegate { get; set; } = null;

        public System.Func<ContentUpdateRequest, Task<UuidResponse>>? CallUpdateDelegate { get; set; } = null;

        public System.Func<UuidRequest, Task<ContentRetrieveResponse>>? CallRetrieveDelegate { get; set; } = null;

        public System.Func<UuidRequest, Task<UuidResponse>>? CallDeleteDelegate { get; set; } = null;

        public System.Func<ContentListRequest, Task<ContentListResponse>>? CallListDelegate { get; set; } = null;

        public System.Func<ContentSearchRequest, Task<ContentListResponse>>? CallSearchDelegate { get; set; } = null;

        public System.Func<ContentMatchRequest, Task<ContentListResponse>>? CallMatchDelegate { get; set; } = null;

    }
}
