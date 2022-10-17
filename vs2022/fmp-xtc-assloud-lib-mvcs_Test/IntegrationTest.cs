
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using XTC.FMP.MOD.Assloud.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;

public class IntegrationTest : IntegrationTestBase
{
    public class TestView
    {

    }

    public class ContentUiBridge : IContentUiBridge
    {
        public void Alert(string _code, string _message, object? _context)
        {
            throw new Exception(_message);
        }

        public void RefreshCreate(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }

        public void RefreshDelete(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }

        public void RefreshFetchAttachments(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }

        public void RefreshFlushUpload(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }

        public void RefreshList(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }

        public void RefreshMatch(IDTO _dto, object? _context)
        {
            var dto = _dto as ContentListResponseDTO;
            Assert.NotEmpty(dto?.Value.Contents);
        }

        public void RefreshPrepareUpload(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }

        public void RefreshRetrieve(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }

        public void RefreshSearch(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }

        public void RefreshUpdate(IDTO _dto, object? _context)
        {
            throw new NotImplementedException();
        }
    }

    public IntegrationTest(TestFixture _testFixture) : base(_testFixture)
    {
    }

    public override async Task Test()
    {
        var facadeContent = fixture_.entry.getStaticContentFacade("test");
        facadeContent?.setUiBridge(new ContentUiBridge());
        var bridgeContent = facadeContent?.getViewBridge() as IContentViewBridge;

        {
            var data = new Dictionary<string, object>();
            string dir = "../../../../../unity2021/vendor/assloud";
            dir = Path.GetFullPath(dir);
            data["dir"] = dir;
            data["gid"] = "test";
            fixture_.model.Publish(Subjects.MountDisk, data);
        }

        {
            SynchronizationContext? context = SynchronizationContext.Current;
            var req = new ContentMatchRequest();
            req.Patterns.Add("tech.meex.∞Ò—˘»ÀŒÔ/+");
            var dto = new ContentMatchRequestDTO(req);
            await bridgeContent.OnMatchSubmit(dto, context);
        }
    }
}
