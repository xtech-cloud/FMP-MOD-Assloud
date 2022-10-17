
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    public static class Utilities
    {
        public static ContentEntity DeepCloneContentEntity(ContentEntity _entity)
        {
            var entity = new ContentEntity();
            entity.Uuid = _entity.Uuid;
            entity.Name = _entity.Name;
            entity.BundleUuid = _entity.BundleUuid;
            entity.BundleName = _entity.BundleName;

            entity.Alias = _entity.Alias;
            foreach (var pair in _entity.AliasI18N)
            {
                entity.AliasI18N.Add(pair.Key, pair.Value);
            }

            entity.Title = _entity.Title;
            foreach (var pair in _entity.TitleI18N)
            {
                entity.TitleI18N.Add(pair.Key, pair.Value);
            }

            entity.Caption = _entity.Caption;
            foreach (var pair in _entity.CaptionI18N)
            {
                entity.CaptionI18N.Add(pair.Key, pair.Value);
            }

            entity.Topic = _entity.Topic;
            foreach (var pair in _entity.TopicI18N)
            {
                entity.TopicI18N.Add(pair.Key, pair.Value);
            }
            entity.Label = _entity.Label;
            foreach (var pair in _entity.LabelI18N)
            {
                entity.LabelI18N.Add(pair.Key, pair.Value);
            }

            entity.Description = _entity.Description;
            foreach (var pair in _entity.DescriptionI18N)
            {
                entity.DescriptionI18N.Add(pair.Key, pair.Value);
            }

            foreach (var pair in _entity.Kv)
            {
                entity.Kv.Add(pair.Key, pair.Value);
            }
            entity.Labels.AddRange(_entity.Labels);
            entity.Tags.AddRange(_entity.Tags);

            return entity;
        }

        public static string FormatSize(ulong _size)
        {
            if (_size < 1024L)
                return string.Format("{0}B", _size);
            if (_size < 1024L * 1024)
                return string.Format("{0}K", _size / 1024);
            if (_size < 1024L * 1024 * 1024)
                return string.Format("{0}M", _size / 1024 / 1024);
            if (_size < 1024L * 1024 * 1024 * 1024)
                return string.Format("{0}G", _size / 1024 / 1024 / 1024);
            return string.Format("{0}T", _size / 1024 / 1024 / 1024);
        }
    }
}
