
using XTC.FMP.MOD.Assloud.App.Service;

public static class MyProgram
{
    public static void PreBuild(WebApplicationBuilder? _builder)
    {
        _builder?.Services.AddSingleton<BundleDAO>();
    }

    public static void PreRun(WebApplication? _app)
    {
    }
}
