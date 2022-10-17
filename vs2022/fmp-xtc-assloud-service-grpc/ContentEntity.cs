using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class ContentEntity : Entity
    {
        public string Name { get; set; } = "";
        public Dictionary<string, string> KV { get; set; } = new Dictionary<string, string>();
        public string Alias { get; set; } = "";
        public string Title { get; set; } = "";
        public string Caption { get; set; } = "";
        public string Label { get; set; } = "";
        public string Topic { get; set; } = "";
        public string Description { get; set; } = "";
        public Dictionary<string, string> Alias_I18N { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Title_I18N { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Caption_I18N { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Label_I18N { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Topic_I18N { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Description_I18N { get; set; } = new Dictionary<string, string>();
        public string[] Labels { get; set; } = new string[0];
        public string[] Tags { get; set; } = new string[0];

        public string Bundle_Uuid { get; set; } = "";
        public string Bundle_Name { get; set; } = "";

        public FileSubEntity[] Attachments { get; set; } = new FileSubEntity[0];
    }
}
