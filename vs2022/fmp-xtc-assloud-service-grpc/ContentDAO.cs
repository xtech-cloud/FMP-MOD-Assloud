using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class ContentDAO : DAO<ContentEntity>
    {
        public ContentDAO(IOptions<DatabaseSettings> _settings) : base(_settings, "Content")
        {
        }

        public LIB.Proto.ContentEntity ToProtoEntity(ContentEntity _entity)
        {
            var entity = new LIB.Proto.ContentEntity();
            entity.Uuid = _entity.Uuid.ToString();
            entity.BundleUuid = _entity.Bundle_Uuid;
            entity.BundleName = _entity.Bundle_Name;
            entity.Name = _entity.Name;
            foreach (var pair in _entity.KV)
                entity.Kv[pair.Key] = pair.Value;
            entity.Alias = _entity.Alias;
            entity.Title = _entity.Title;
            entity.Caption = _entity.Caption;
            entity.Label = _entity.Label;
            entity.Topic = _entity.Topic;
            entity.Description = _entity.Description;
            foreach (var pair in _entity.Alias_I18N)
                entity.AliasI18N[pair.Key] = pair.Value;
            foreach (var pair in _entity.Title_I18N)
                entity.TitleI18N[pair.Key] = pair.Value;
            foreach (var pair in _entity.Caption_I18N)
                entity.CaptionI18N[pair.Key] = pair.Value;
            foreach (var pair in _entity.Label_I18N)
                entity.LabelI18N[pair.Key] = pair.Value;
            foreach (var pair in _entity.Topic_I18N)
                entity.TopicI18N[pair.Key] = pair.Value;
            foreach (var pair in _entity.Description_I18N)
                entity.DescriptionI18N[pair.Key] = pair.Value;
            foreach (var label in _entity.Labels)
                entity.Labels.Add(label);
            foreach (var tag in _entity.Tags)
                entity.Tags.Add(tag);
            return entity;
        }


        public virtual async Task<ContentEntity?> FindWithNameAsync(string _name) =>
            await collection_.Find(x => x.Name.Equals(_name)).FirstOrDefaultAsync();

        public virtual async Task<long> CountAsync(string _bundleUUID) =>
            await collection_.CountDocumentsAsync(x => x.Bundle_Uuid.Equals(_bundleUUID));

        public virtual async Task<List<ContentEntity>> ListAsync(int _offset, int _count, string _bundleUUID) =>
            await collection_.Find(x => x.Bundle_Uuid.Equals(_bundleUUID)).Skip(_offset).Limit(_count).ToListAsync();



        /// <summary>
        /// 异步搜索实体
        /// </summary>
        /// <param name="_offset">偏移量</param>
        /// <param name="_count">查询量</param>
        /// <returns></returns>
        public virtual async Task<KeyValuePair<long, List<ContentEntity>>> SearchAsync(int _offset, int _count, string? _name, string[]? _labels, string[]? _tags)
        {
            // _name, _labels, _tags 三个条件至少需要满足一个
            if (string.IsNullOrWhiteSpace(_name) && (null == _labels || _labels.Length == 0) && (null == _tags || _tags.Length == 0))
                return new KeyValuePair<long, List<ContentEntity>>(0, new List<ContentEntity>());

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

            var filter = Builders<ContentEntity>.Filter.Where(x =>
                (string.IsNullOrWhiteSpace(_name) || (null != x.Name && x.Name.ToLower().Contains(_name.ToLower()))) &&
                (null == _labels || hasSubSet(x.Labels, _labels)) &&
                (null == _tags || hasSubSet(x.Tags, _tags))
            );

            var found = collection_.Find(filter);

            var total = await found.CountDocumentsAsync();
            var bundles = await found.Skip((int)_offset).Limit((int)_count).ToListAsync();

            return new KeyValuePair<long, List<ContentEntity>>(total, bundles);
        }
    }
}
