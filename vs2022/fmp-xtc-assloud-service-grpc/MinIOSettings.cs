namespace XTC.FMP.MOD.Assloud.App.Service
{
    /// <summary>
    /// MinIO设置
    /// </summary>
    public class MinIOSettings
    {
        /// <summary>
        /// 外部的访问协议
        /// </summary>
        public string AddressScheme { get; set; } = null!;
        /// <summary>
        /// 外部的访问地址
        /// </summary>
        public string AddressUrl { get; set; } = null!;

        /// <summary>
        /// 连接地址
        /// </summary>
        public string Endpoint { get; set; } = null!;

        /// <summary>
        /// 存储桶
        /// </summary>
        public string Bucket { get; set; } = null!;

        public string AccessKey { get; set; } = null!;

        public string SecretKey { get; set; } = null!;
    }
}
