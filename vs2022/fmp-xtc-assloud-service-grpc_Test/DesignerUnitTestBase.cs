
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!
//*************************************************************************************

public abstract class DesignerUnitTestBase : IClassFixture<TestFixture>
{
    /// <summary>
    /// 测试上下文
    /// </summary>
    protected TestFixture fixture_ { get; set; }

    public DesignerUnitTestBase(TestFixture _testFixture)
    {
        fixture_ = _testFixture;
    }


    [Fact]
    public abstract Task ReadStyleSheetTest();

    [Fact]
    public abstract Task WriteStyleTest();

    [Fact]
    public abstract Task ReadInstancesTest();

    [Fact]
    public abstract Task WriteInstancesTest();

}
