
namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    /// <summary>
    /// Content服务层
    /// </summary>
    public class ContentService : ContentServiceBase
    {
        /// <summary>
        /// 完整名称
        /// </summary>
        public const string NAME = "XTC.FMP.MOD.Assloud.LIB.MVCS.ContentService";

        /// <summary>
        /// 带uid参数的构造函数
        /// </summary>
        /// <param name="_uid">实例化后的唯一识别码</param>
        /// <param name="_gid">直系的组的ID</param>
        public ContentService(string _uid, string _gid) : base(_uid, _gid) 
        {
        }
    }
}
