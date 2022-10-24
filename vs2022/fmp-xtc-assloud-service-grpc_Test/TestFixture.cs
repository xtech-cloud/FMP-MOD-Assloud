
using XTC.FMP.MOD.Assloud.App.Service;

/// <summary>
/// 测试上下文，用于共享测试资源
/// </summary>
public class TestFixture : TestFixtureBase
{
    private SingletonServices singletonServices_;

    public TestFixture()
        : base()
    {
        singletonServices_ = new SingletonServices(new DatabaseOptions(), new MinIOOptions());
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    protected override void newBundleService()
    {
        serviceBundle_ = new BundleService(singletonServices_);
    }

    protected override void newContentService()
    {
        serviceContent_ = new ContentService(singletonServices_);
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
