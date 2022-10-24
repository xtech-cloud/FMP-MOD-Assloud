using MongoDB.Bson;
using MongoDB.Driver;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class AggregateDAO
    {
        private IMongoDatabase db_;
        public AggregateDAO(IMongoDatabase _mongoDatabase)
        {
            db_ = _mongoDatabase;
        }

    }
}
