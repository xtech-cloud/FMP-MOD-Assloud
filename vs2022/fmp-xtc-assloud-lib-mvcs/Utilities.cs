
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
            entity.ForeignBundleUuid = _entity.ForeignBundleUuid;

            entity.Alias = _entity.Alias;
            foreach (var pair in _entity.AliasI18NS)
            {
                entity.AliasI18NS.Add(pair.Key, pair.Value);
            }

            entity.Title = _entity.Title;
            foreach (var pair in _entity.TitleI18NS)
            {
                entity.TitleI18NS.Add(pair.Key, pair.Value);
            }

            entity.Caption = _entity.Caption;
            foreach (var pair in _entity.CaptionI18NS)
            {
                entity.CaptionI18NS.Add(pair.Key, pair.Value);
            }

            entity.Topic = _entity.Topic;
            foreach (var pair in _entity.TopicI18NS)
            {
                entity.TopicI18NS.Add(pair.Key, pair.Value);
            }
            entity.Label = _entity.Label;
            foreach (var pair in _entity.LabelI18NS)
            {
                entity.LabelI18NS.Add(pair.Key, pair.Value);
            }

            entity.Description = _entity.Description;
            foreach (var pair in _entity.DescriptionI18NS)
            {
                entity.DescriptionI18NS.Add(pair.Key, pair.Value);
            }

            foreach (var pair in _entity.KvS)
            {
                entity.KvS.Add(pair.Key, pair.Value);
            }
            entity.LabelS.AddRange(_entity.LabelS);
            entity.TagS.AddRange(_entity.TagS);

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
