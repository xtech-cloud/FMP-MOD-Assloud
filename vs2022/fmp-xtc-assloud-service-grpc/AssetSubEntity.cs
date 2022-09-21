using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class AssetSubEntity
    {
        public string Path { get; set; } = "";
        public string Hash { get; set; } = "";
        public ulong Size { get; set; } = 0;
    }
}
