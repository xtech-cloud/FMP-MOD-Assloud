using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class FileSubEntity
    {
        /// <summary>
        /// 相对路径
        /// </summary>
        public string path { get; set; } = "";

        /// <summary>
        /// 哈希值
        /// </summary>
        public string hash { get; set; } = "";

        /// <summary>
        /// 大小
        /// </summary>
        public ulong Size { get; set; } = 0;

        /// <summary>
        /// 外部直接访问路径
        /// </summary>
        /// <remarks>
        /// 空值时为直接存储的文件，返回URL给客户端时实时生成
        /// 非空值时为外部存储的文件
        /// </remarks>
        public string Url{ get; set; } = "";
    }
}
