using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class ContentEntity : Entity
    {
        public string name { get; set; } = "";
        public Dictionary<string, string> kvS { get; set; } = new Dictionary<string, string>();
        public string alias { get; set; } = "";
        public string title { get; set; } = "";
        public string caption { get; set; } = "";
        public string label { get; set; } = "";
        public string topic { get; set; } = "";
        public string description { get; set; } = "";
        public Dictionary<string, string> alias_i18nS { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> title_i18nS { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> caption_i18nS { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> label_i18nS { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> topic_i18nS { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> description_i18nS { get; set; } = new Dictionary<string, string>();
        public string[] labelS { get; set; } = new string[0];
        public string[] tagS { get; set; } = new string[0];

        public Guid? foreign_bundle_uuid { get; set; } = null;

        public FileSubEntity[] AttachmentS { get; set; } = new FileSubEntity[0];
    }

    public class ExtraContentEntity : ContentEntity
    {
        public string? extra_bundle_name { get; set; } = null;
    }
}
