
using XTC.FMP.MOD.Assloud.App.Service;

/// <summary>
/// 测试上下文，用于共享测试资源
/// </summary>
public class TestFixture : TestFixtureBase
{
    public TestFixture()
        : base()
    {
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    protected override void newBundleService()
    {
        serviceBundle_ = new BundleService(new BundleDAO(new DatabaseOptions()), new ContentDAO(new DatabaseOptions()), new MinIOClient(new MinIOOptions()));
    }

    protected override void newContentService()
    {
        serviceContent_ = new ContentService(new ContentDAO(new DatabaseOptions()), new BundleDAO(new DatabaseOptions()));
    }

    protected override void newDesignerService()
    {
        throw new NotImplementedException();
        //serviceDesigner_ = new DesignerService(new DesignerDAO(new DatabaseOptions()));
    }

    protected override void newHealthyService()
    {
        throw new NotImplementedException();
        //serviceHealthy_ = new HealthyService(new HealthyDAO(new DatabaseOptions()));
    }

}
