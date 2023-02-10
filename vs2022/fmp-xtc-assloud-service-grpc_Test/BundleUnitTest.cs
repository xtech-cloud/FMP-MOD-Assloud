
using System.ComponentModel.DataAnnotations.Schema;
using XTC.FMP.MOD.Assloud.LIB.Proto;

public class BundleTest : BundleUnitTestBase
{
    public BundleTest(TestFixture _testFixture)
        : base(_testFixture)
    {
    }

    public override async Task CreateTest()
    {
        string uuid = "";
        {
            var request = new BundleCreateRequest();
            request.Name = "cloud.xtech.test.create";
            var response = await fixture_.getServiceBundle().Create(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            uuid = response.Uuid;
            // 重复创建
            request.Name = "cloud.xtech.test.create";
            response = await fixture_.getServiceBundle().Create(request, fixture_.context);
            Assert.Equal(1, response.Status.Code);
        }
        {
            var request = new UuidRequest();
            request.Uuid = uuid;
            var response = await fixture_.getServiceBundle().Delete(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
        }
    }

    public override async Task UpdateTest()
    {
        string uuid = "";
        {
            var request = new BundleCreateRequest();
            request.Name = "cloud.xtech.test.update";
            var response = await fixture_.getServiceBundle().Create(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            uuid = response.Uuid;
        }
        {
            var request = new BundleUpdateRequest();
            request.Uuid = uuid;
            request.Name = "name";
            request.Summary = "summary";
            request.TagS.Add("tag-1");
            request.TagS.Add("tag-2");
            request.LabelS.Add("label-1");
            request.LabelS.Add("label-2");
            request.SummaryI18NS.Add("en_US", "summary_en_US");
            var response = await fixture_.getServiceBundle().Update(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);

            // 获取更新后的数据
            var request2 = new UuidRequest();
            request2.Uuid = uuid;
            var response2 = await fixture_.getServiceBundle().Retrieve(request2, fixture_.context);
            Assert.Equal(0, response2.Status.Code);
            Assert.Equal(uuid, response2.Bundle.Uuid);
            Assert.Equal(request.Name, response2.Bundle.Name);
            Assert.Equal(request.Summary, response2.Bundle.Summary);
            Assert.Equal(2, response2.Bundle.Tags.Count);
            Assert.Equal(2, response2.Bundle.Labels.Count);
            Assert.Equal("summary_en_US", response2.Bundle.SummaryI18NS["en_US"]);

            // 不存在
            request.Uuid = Guid.NewGuid().ToString();
            response = await fixture_.getServiceBundle().Update(request, fixture_.context);
            Assert.Equal(1, response.Status.Code);
        }
        {
            var request = new UuidRequest();
            request.Uuid = uuid;
            var response = await fixture_.getServiceBundle().Delete(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
        }
    }

    public override async Task RetrieveTest()
    {
        string uuid = "";
        {
            var request = new BundleCreateRequest();
            request.Name = "cloud.xtech.test.retrieve";
            var response = await fixture_.getServiceBundle().Create(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);

            uuid = response.Uuid;
        }
        {
            var request = new UuidRequest();
            request.Uuid = uuid;
            var response = await fixture_.getServiceBundle().Retrieve(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);

            // 不存在的
            request.Uuid = Guid.NewGuid().ToString();
            response = await fixture_.getServiceBundle().Retrieve(request, fixture_.context);
            Assert.Equal(1, response.Status.Code);
        }
    }

    public override async Task DeleteTest()
    {
        string uuid = "";
        {
            var request = new BundleCreateRequest();
            request.Name = "cloud.xtech.test.delete";
            var response = await fixture_.getServiceBundle().Create(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            uuid = response.Uuid;
        }
        {
            var request = new UuidRequest();
            request.Uuid = uuid;
            var response = await fixture_.getServiceBundle().Delete(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);

            // 不存在的
            request.Uuid = Guid.NewGuid().ToString();
            response = await fixture_.getServiceBundle().Delete(request, fixture_.context);
            Assert.Equal(1, response.Status.Code);
        }
    }

    public override async Task ListTest()
    {
        List<string> uuids = new List<string>();
        {
            for (int i = 0; i < 10; i++)
            {
                var request = new BundleCreateRequest();
                request.Name = "cloud.xtech.test.list#" + i.ToString();
                var response = await fixture_.getServiceBundle().Create(request, fixture_.context);
                uuids.Add(response.Uuid);
                Assert.Equal(0, response.Status.Code);
            }
        }
        {
            var request = new BundleListRequest();
            request.Count = 5;
            var response = await fixture_.getServiceBundle().List(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(10, response.Total);
            Assert.Equal(5, response.BundleS.Count);
            request.Offset = 8;
            request.Count = 5;
            response = await fixture_.getServiceBundle().List(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(10, response.Total);
            Assert.Equal(2, response.BundleS.Count);
            Assert.Contains("#8", response.BundleS[0].Name);
        }
        {
            foreach (var uuid in uuids)
            {
                var request = new UuidRequest();
                request.Uuid = uuid;
                var response = await fixture_.getServiceBundle().Delete(request, fixture_.context);
                Assert.Equal(0, response.Status.Code);
            }
        }
    }

    public override async Task SearchTest()
    {
        List<string> uuids = new List<string>();
        {
            for (int i = 0; i < 10; i++)
            {
                var request = new BundleCreateRequest();
                request.Name = "cloud.xtech.test.list#" + i.ToString();
                var response = await fixture_.getServiceBundle().Create(request, fixture_.context);
                uuids.Add(response.Uuid);
                Assert.Equal(0, response.Status.Code);
                var request2 = new BundleUpdateRequest();
                request2.Uuid = response.Uuid;
                request2.LabelS.Add("label." + (i % 3 + 1).ToString());
                request2.TagS.Add("tag." + (i % 3 + 1).ToString());
                var response2 = await fixture_.getServiceBundle().Update(request2, fixture_.context);
                Assert.Equal(0, response2.Status.Code);
            }
        }

        {
            var request = new BundleSearchRequest();
            request.Count = 5;
            var response = await fixture_.getServiceBundle().Search(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(0, response.Total);

            request.Name = "cloud.";
            request.Offset = 0;
            request.Count = 50;
            response = await fixture_.getServiceBundle().Search(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(10, response.Total);
            Assert.Equal(10, response.BundleS.Count);

            request.Offset = 5;
            request.Count = 2;
            response = await fixture_.getServiceBundle().Search(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(10, response.Total);
            Assert.Equal(2, response.BundleS.Count);
            Assert.Contains("#6", response.BundleS[0].Name);
        }

        {
            foreach (var uuid in uuids)
            {
                var request = new UuidRequest();
                request.Uuid = uuid;
                var response = await fixture_.getServiceBundle().Delete(request, fixture_.context);
                Assert.Equal(0, response.Status.Code);
            }
        }
    }

    public override Task PrepareUploadResourceTest()
    {
        throw new NotImplementedException();
    }

    public override Task FlushUploadResourceTest()
    {
        throw new NotImplementedException();
    }

    public override Task FetchResourcesTest()
    {
        throw new NotImplementedException();
    }

    public override Task TidyTest()
    {
        throw new NotImplementedException();
    }

    public override Task DeleteResourceTest()
    {
        throw new NotImplementedException();
    }
}
