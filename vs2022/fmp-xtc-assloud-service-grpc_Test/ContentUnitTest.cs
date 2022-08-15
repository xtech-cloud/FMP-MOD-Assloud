
using XTC.FMP.MOD.Assloud.LIB.Proto;

public class ContentTest : ContentUnitTestBase
{
    public ContentTest(TestFixture _testFixture)
        : base(_testFixture)
    {
    }


    public override async Task MatchTest()
    {
        var request = new ContentMatchRequest();
        var response = await fixture_.getServiceContent().Match(request, fixture_.context);
        Assert.Equal(0, response.Status.Code);
    }

}
