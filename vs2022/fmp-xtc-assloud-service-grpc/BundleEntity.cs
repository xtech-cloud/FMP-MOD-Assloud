using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class BundleEntity : Entity
    {
        public string Name { get; set; } = "";
        public string Summary { get; set; } = "";
        public string[] Labels { get; set; } = new string[0];
        public string[] Tags { get; set; } = new string[0];
        public AssetSubEntity[] Assets { get; set; } = new AssetSubEntity[0];
        public Dictionary<string, string> Summary_i18n { get; set; } = new Dictionary<string, string>();
    }
}
