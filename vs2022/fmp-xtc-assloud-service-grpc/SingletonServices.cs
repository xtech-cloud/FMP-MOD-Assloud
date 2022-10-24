using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    /// <summary>
    /// 单例服务
    /// </summary>
    public class SingletonServices
    {
        private MongoClient mongoClient_;
        private IMongoDatabase mongoDatabase_;
        private BundleDAO daoBundle_;
        private ContentDAO daoContent_;
        private MinIOClient clientMinIO_;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>
        /// 参数为自动注入，支持多个参数，DatabaseSettings的注入点在Program.cs中，自定义设置可在MyProgram.PreBuild中注入
        /// </remarks>
        /// <param name="_databaseSettings"></param>
        /// <param name="_minioSettings"></param>
        public SingletonServices(IOptions<DatabaseSettings> _databaseSettings, IOptions<MinIOSettings> _minioSettings)
        {
            mongoClient_ = new MongoClient(_databaseSettings.Value.ConnectionString);
            mongoDatabase_ = mongoClient_.GetDatabase(_databaseSettings.Value.DatabaseName);

            daoBundle_ = new BundleDAO(mongoDatabase_);
            daoContent_ = new ContentDAO(mongoDatabase_);
            clientMinIO_ = new MinIOClient(_minioSettings);
        }

        public BundleDAO getBundleDAO()
        {
            return daoBundle_;
        }

        public ContentDAO getContentDAO()
        {
            return daoContent_;
        }

        public MinIOClient getMinioClient()
        {
            return clientMinIO_;
        }
    }
}
