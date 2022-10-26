namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class ManifestSchema
    {
        public class Entry
        {
            public string file = "";
            public ulong size = 0;
            public string hash = "";
        }
        public List<Entry> entries = new List<Entry>();
    }
}
