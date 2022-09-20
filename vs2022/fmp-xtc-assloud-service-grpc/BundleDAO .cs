using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class BundleDAO : DAO<BundleEntity>
    {
        public BundleDAO(IOptions<DatabaseSettings> _settings) : base(_settings, "Bundle")
        {
        }

        public LIB.Proto.BundleEntity ToProtoEntity(BundleEntity _entity)
        {
            var entity = new LIB.Proto.BundleEntity();
            entity.Uuid = _entity.Uuid.ToString();
            entity.Name = _entity.Name;
            entity.Summary = _entity.Summary;
            foreach (var label in _entity.Labels)
            {
                entity.Labels.Add(label);
            }
            foreach (var tag in _entity.Tags)
            {
                entity.Tags.Add(tag);
            }
            foreach (var pair in _entity.Summary_i18n)
            {
                entity.SummaryI18N[pair.Key] = pair.Value;
            }
            return entity;
        }

        public virtual async Task<BundleEntity?> FindWithNameAsync(string _name) =>
            await collection_.Find(x => x.Name.Equals(_name)).FirstOrDefaultAsync();

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
                (string.IsNullOrWhiteSpace(_name) || (null != x.Name && x.Name.ToLower().Contains(_name.ToLower()))) &&
                (null == _labels || hasSubSet(x.Labels, _labels)) &&
                (null == _tags || hasSubSet(x.Tags, _tags))
            );

            var found = collection_.Find(filter);

            var total = await found.CountDocumentsAsync();
            var bundles = await found.Skip((int)_offset).Limit((int)_count).ToListAsync();

            return new KeyValuePair<long, List<BundleEntity>>(total, bundles);
        }
    }
}
