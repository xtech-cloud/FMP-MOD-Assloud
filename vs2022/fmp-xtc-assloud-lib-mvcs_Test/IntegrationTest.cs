
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
        public virtual void Alert(string _code, string _message)
        {
            throw new Exception(_message);
        }

        public virtual void RefreshMatch(IDTO _dto)
        {
            var dto = _dto as ContentListResponseDTO;
            Assert.NotEmpty(dto?.Value.Contents);
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
            fixture_.model.Publish(Subjects.MountDisk, data);
        }

        {
            var req = new ContentMatchRequest();
            req.Patterns.Add("tech.meex.��������/+");
            var dto = new ContentMatchRequestDTO(req);
            await bridgeContent.OnMatchSubmit(dto);
        }
    }
}
