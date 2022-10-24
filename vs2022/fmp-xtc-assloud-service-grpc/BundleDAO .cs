using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class BundleDAO : MongoDAO<BundleEntity>
    {
        public BundleDAO(IMongoDatabase _mongoDatabase) : base(_mongoDatabase, "Bundle")
        {
        }

        public LIB.Proto.BundleEntity ToProtoEntity(BundleEntity _entity)
        {
            var entity = new LIB.Proto.BundleEntity();
            entity.Uuid = _entity.Uuid.ToString();
            entity.Name = _entity.name;
            entity.Summary = _entity.summary;
            foreach (var label in _entity.labelS)
            {
                entity.Labels.Add(label);
            }
            foreach (var tag in _entity.tagS)
            {
                entity.Tags.Add(tag);
            }
            foreach (var pair in _entity.summary_i18nS)
            {
                entity.SummaryI18NS[pair.Key] = pair.Value;
            }
            return entity;
        }

        public virtual async Task<BundleEntity?> FindWithNameAsync(string _name) =>
            await collection_.Find(x => x.name.Equals(_name)).FirstOrDefaultAsync();

        /// <summary>
        /// 异步搜索实体
        /// </summary>
        /// <param name="_offset">偏移量</param>
        /// <param name="_count">查询量</param>
        /// <returns></returns>
        public virtual async Task<KeyValuePair<long, List<BundleEntity>>> SearchAsync(int _offset, int _count, string? _name, string[]? _labels, string[]? _tags)
        {
            // _name, _labels, _tags 三个条件至少需要满足一个
            if (string.IsNullOrWhiteSpace(_name) && (null == _labels || _labels.Length == 0) && (null == _tags || _tags.Length == 0))
                return new KeyValuePair<long, List<BundleEntity>>(0, new List<BundleEntity>());

            System.Func<string[], string[], bool> hasSubSet = (_val, _sub) =>
            {
                foreach (var sub in _sub)
                {
                    foreach (var val in _val)
                    {
                        if (!val.Equals(sub))
                            return false;
                    }
                }
                return true;
            };

            var filter = Builders<BundleEntity>.Filter.Where(x =>
                (string.IsNullOrWhiteSpace(_name) || (null != x.name && x.name.ToLower().Contains(_name.ToLower()))) &&
                (null == _labels || hasSubSet(x.labelS, _labels)) &&
                (null == _tags || hasSubSet(x.tagS, _tags))
            );

            var found = collection_.Find(filter);

            var total = await found.CountDocumentsAsync();
            var bundles = await found.Skip((int)_offset).Limit((int)_count).ToListAsync();

            return new KeyValuePair<long, List<BundleEntity>>(total, bundles);
        }

        public async Task PutBucketEntityToMinIO(BundleEntity _entity, MinIOClient _minioClient)
        {
            string filepath = String.Format("{0}/meta.json", _entity.Uuid.ToString());
            //将meta存入对象存储引擎中
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_entity));
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                await _minioClient.PutObject(filepath, stream);
            }
        }

        public async Task RemoveBucketEntityFromMinIO(BundleEntity _entity, MinIOClient _minioClient)
        {
            string filepath = String.Format("{0}/meta.json", _entity.Uuid.ToString());
            await _minioClient.RemoveObject(filepath);
        }
    }
}
