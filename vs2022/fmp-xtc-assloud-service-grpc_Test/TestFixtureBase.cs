
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.53.0.  DO NOT EDIT!
//*************************************************************************************

using XTC.FMP.MOD.Assloud.App.Service;

public abstract class TestFixtureBase : IDisposable
{
    public TestServerCallContext context { get; set; }

    public TestFixtureBase()
    {
        context = TestServerCallContext.Create();
    }

    public virtual void Dispose()
    {

        var options = new DatabaseOptions();
        var mongoClient = new MongoDB.Driver.MongoClient(options.Value.ConnectionString);
        mongoClient.DropDatabase(options.Value.DatabaseName);

    }


    protected BundleService? serviceBundle_ { get; set; }

    public BundleService getServiceBundle()
    {
        if(null == serviceBundle_)
        {
            newBundleService();
        }
        return serviceBundle_!;
    }

    /// <summary>
    /// 实例化服务
    /// </summary>
    /// <example>
    /// serviceBundle_ = new BundleService(new BundleDAO(new DatabaseOptions()));
    /// </example>
    protected abstract void newBundleService();

    protected ContentService? serviceContent_ { get; set; }

    public ContentService getServiceContent()
    {
        if(null == serviceContent_)
        {
            newContentService();
        }
        return serviceContent_!;
    }

    /// <summary>
    /// 实例化服务
    /// </summary>
    /// <example>
    /// serviceContent_ = new ContentService(new ContentDAO(new DatabaseOptions()));
    /// </example>
    protected abstract void newContentService();

    protected DesignerService? serviceDesigner_ { get; set; }

    public DesignerService getServiceDesigner()
    {
        if(null == serviceDesigner_)
        {
            newDesignerService();
        }
        return serviceDesigner_!;
    }

    /// <summary>
    /// 实例化服务
    /// </summary>
    /// <example>
    /// serviceDesigner_ = new DesignerService(new DesignerDAO(new DatabaseOptions()));
    /// </example>
    protected abstract void newDesignerService();

    protected HealthyService? serviceHealthy_ { get; set; }

    public HealthyService getServiceHealthy()
    {
        if(null == serviceHealthy_)
        {
            newHealthyService();
        }
        return serviceHealthy_!;
    }

    /// <summary>
    /// 实例化服务
    /// </summary>
    /// <example>
    /// serviceHealthy_ = new HealthyService(new HealthyDAO(new DatabaseOptions()));
    /// </example>
    protected abstract void newHealthyService();

}

