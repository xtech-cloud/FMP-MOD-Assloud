using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class SingletonServices
    {
        private MongoClient mongoClient_;
        private IMongoDatabase mongoDatabase_;
        private BundleDAO daoBundle_;
        private ContentDAO daoContent_;
        private AggregateDAO daoAggregate_;
        private MinIOClient clientMinIO_;

        public SingletonServices(IOptions<DatabaseSettings> _databaseSettings, IOptions<MinIOSettings> _minioSettings)
        {
            mongoClient_ = new MongoClient(_databaseSettings.Value.ConnectionString);
            mongoDatabase_ = mongoClient_.GetDatabase(_databaseSettings.Value.DatabaseName);

            daoBundle_ = new BundleDAO(mongoDatabase_);
            daoContent_ = new ContentDAO(mongoDatabase_);
            daoAggregate_ = new AggregateDAO(mongoDatabase_);
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

        public AggregateDAO getAggregateDAO()
        {
            return daoAggregate_;
        }

        public MinIOClient getMinioClient()
        {
            return clientMinIO_;
        }
    }
}
