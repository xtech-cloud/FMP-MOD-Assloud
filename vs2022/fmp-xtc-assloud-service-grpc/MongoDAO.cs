
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!
//*************************************************************************************

using MongoDB.Driver;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    /// <summary>
    /// 泛型数据访问对象
    /// </summary>
    /// <typeparam name="T">Entity的派生类</typeparam>
    /// <example>
    /// public class YourDAO : DAO<YourEntity>
    /// {
    ///     public YourDAO(IMongoDatabase _mongoDatabase) 
    ///         : base(_mongoDatabase, "TableName")
    ///     {
    ///     }
    ///}
    /// </example>
    public class MongoDAO<T> where T : Entity
    {
        protected readonly IMongoDatabase mongoDatabase_;
        protected readonly IMongoCollection<T> collection_;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_mongoDatabase">mongo数据库</param>
        /// <param name="_collectionName">数据集合的名称</param>
        public MongoDAO(IMongoDatabase _mongoDatabase, string _collectionName)
        {
            mongoDatabase_ = _mongoDatabase;
            collection_ = mongoDatabase_.GetCollection<T>(_collectionName);
        }

        /// <summary>
        /// 异步创建实体
        /// </summary>
        /// <param name="_entity">实体的实例</param>
        /// <returns></returns>
        public virtual async Task CreateAsync(T _entity) =>
           await collection_.InsertOneAsync(_entity);

        /// <summary>
        /// 异步计算数量
        /// </summary>
        /// <returns></returns>
        public virtual async Task<long> CountAsync() =>
            await collection_.CountDocumentsAsync(_=>true);

        /// <summary>
        /// 异步列举实体
        /// </summary>
        /// <param name="_offset">偏移量</param>
        /// <param name="_count">查询量</param>
        /// <returns></returns>
        public virtual async Task<List<T>> ListAsync(int _offset, int _count) =>
            await collection_.Find(_ => true).Skip(_offset).Limit(_count).ToListAsync();

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="_uuid">实体的uuid</param>
        /// <returns></returns>
        public virtual async Task<T?> GetAsync(string _uuid) =>
            await collection_.Find(x => x.Uuid.Equals(Guid.Parse(_uuid))).FirstOrDefaultAsync();

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="_uuid">实体的uuid</param>
        /// <param name="_entity">实体的实例</param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(string _uuid, T _entity) =>
            await collection_.ReplaceOneAsync(x => x.Uuid.Equals(Guid.Parse(_uuid)), _entity);

        /// <summary>
        /// 异步移除实体
        /// </summary>
        /// <param name="_uuid">实体的uuid</param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(string _uuid) =>
            await collection_.DeleteOneAsync(x => x.Uuid.Equals(Guid.Parse(_uuid)));
    }
}
