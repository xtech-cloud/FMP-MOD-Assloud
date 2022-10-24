using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class BundleEntity : Entity
    {
        public string name { get; set; } = "";
        public string summary { get; set; } = "";
        public string[] labelS { get; set; } = new string[0];
        public string[] tagS { get; set; } = new string[0];
        public FileSubEntity[] resourceS { get; set; } = new FileSubEntity[0];
        public Dictionary<string, string> summary_i18nS { get; set; } = new Dictionary<string, string>();
        public Guid[] foreign_content_uuidS { get; set; } = new Guid[0];
    }
}
