using System.ComponentModel.DataAnnotations.Schema;
using XTC.FMP.MOD.Assloud.LIB.Proto;

public class ContentTest : ContentUnitTestBase
{
    public ContentTest(TestFixture _testFixture)
        : base(_testFixture)
    {
    }


    public override async Task CreateTest()
    {
        string uuid = "";
        {
            var request = new ContentCreateRequest();
            request.BundleUuid = Guid.NewGuid().ToString();
            request.Name = "cloud.xtech.test.create";
            var response = await fixture_.getServiceContent().Create(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            uuid = response.Uuid;
            // 重复创建
            request.Name = "cloud.xtech.test.create";
            response = await fixture_.getServiceContent().Create(request, fixture_.context);
            Assert.Equal(1, response.Status.Code);
        }
        {
            var request = new UuidRequest();
            request.Uuid = uuid;
            var response = await fixture_.getServiceContent().Delete(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
        }
    }

    public override async Task UpdateTest()
    {
        string uuid = "";
        {
            var request = new ContentCreateRequest();
            request.BundleUuid = Guid.NewGuid().ToString();
            request.Name = "cloud.xtech.test.update";
            var response = await fixture_.getServiceContent().Create(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            uuid = response.Uuid;
        }
        {
            var request = new ContentUpdateRequest();
            request.Uuid = uuid;
            request.Name = "name";
            request.Tags.Add("tag-1");
            request.Tags.Add("tag-2");
            request.Labels.Add("label-1");
            request.Labels.Add("label-2");
            var response = await fixture_.getServiceContent().Update(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);

            // 获取更新后的数据
            var request2 = new UuidRequest();
            request2.Uuid = uuid;
            var response2 = await fixture_.getServiceContent().Retrieve(request2, fixture_.context);
            Assert.Equal(0, response2.Status.Code);
            Assert.Equal(uuid, response2.Content.Uuid);
            Assert.Equal(request.Name, response2.Content.Name);
            Assert.Equal(2, response2.Content.Tags.Count);
            Assert.Equal(2, response2.Content.Labels.Count);

            // 不存在
            request.Uuid = Guid.NewGuid().ToString();
            response = await fixture_.getServiceContent().Update(request, fixture_.context);
            Assert.Equal(1, response.Status.Code);
        }
        {
            var request = new UuidRequest();
            request.Uuid = uuid;
            var response = await fixture_.getServiceContent().Delete(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
        }
    }

    public override async Task RetrieveTest()
    {
        string uuid = "";
        {
            var request = new ContentCreateRequest();
            request.BundleUuid = Guid.NewGuid().ToString();
            request.Name = "cloud.xtech.test.retrieve";
            var response = await fixture_.getServiceContent().Create(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);

            uuid = response.Uuid;
        }
        {
            var request = new UuidRequest();
            request.Uuid = uuid;
            var response = await fixture_.getServiceContent().Retrieve(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);

            // 不存在的
            request.Uuid = Guid.NewGuid().ToString();
            response = await fixture_.getServiceContent().Retrieve(request, fixture_.context);
            Assert.Equal(1, response.Status.Code);
        }
    }

    public override async Task DeleteTest()
    {
        string uuid = "";
        {
            var request = new ContentCreateRequest();
            request.BundleUuid = Guid.NewGuid().ToString();
            request.Name = "cloud.xtech.test.delete";
            var response = await fixture_.getServiceContent().Create(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            uuid = response.Uuid;
        }
        {
            var request = new UuidRequest();
            request.Uuid = uuid;
            var response = await fixture_.getServiceContent().Delete(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);

            // 不存在的
            request.Uuid = Guid.NewGuid().ToString();
            response = await fixture_.getServiceContent().Delete(request, fixture_.context);
            Assert.Equal(1, response.Status.Code);
        }
    }

    public override async Task ListTest()
    {
        List<string> uuids = new List<string>();
        {
            for (int i = 0; i < 10; i++)
            {
                var request = new ContentCreateRequest();
                request.BundleUuid = Guid.NewGuid().ToString();
                request.Name = "cloud.xtech.test.list#" + i.ToString();
                var response = await fixture_.getServiceContent().Create(request, fixture_.context);
                uuids.Add(response.Uuid);
                Assert.Equal(0, response.Status.Code);
            }
        }
        {
            var request = new ContentListRequest();
            request.Count = 5;
            var response = await fixture_.getServiceContent().List(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(10, response.Total);
            Assert.Equal(5, response.Contents.Count);
            request.Offset = 8;
            request.Count = 5;
            response = await fixture_.getServiceContent().List(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(10, response.Total);
            Assert.Equal(2, response.Contents.Count);
            Assert.Contains("#8", response.Contents[0].Name);
        }
        {
            foreach (var uuid in uuids)
            {
                var request = new UuidRequest();
                request.Uuid = uuid;
                var response = await fixture_.getServiceContent().Delete(request, fixture_.context);
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
                var request = new ContentCreateRequest();
                request.BundleUuid = Guid.NewGuid().ToString();
                request.Name = "cloud.xtech.test.search#" + i.ToString();
                var response = await fixture_.getServiceContent().Create(request, fixture_.context);
                uuids.Add(response.Uuid);
                Assert.Equal(0, response.Status.Code);
                var request2 = new ContentUpdateRequest();
                request2.Uuid = response.Uuid;
                request2.Labels.Add("label." + (i % 3 + 1).ToString());
                request2.Tags.Add("tag." + (i % 3 + 1).ToString());
                var response2 = await fixture_.getServiceContent().Update(request2, fixture_.context);
                Assert.Equal(0, response2.Status.Code);
            }
        }

        {
            var request = new ContentSearchRequest();
            request.Count = 5;
            var response = await fixture_.getServiceContent().Search(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(0, response.Total);

            request.Name = "cloud.";
            request.Offset = 0;
            request.Count = 50;
            response = await fixture_.getServiceContent().Search(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(10, response.Total);
            Assert.Equal(10, response.Contents.Count);

            request.Offset = 5;
            request.Count = 2;
            response = await fixture_.getServiceContent().Search(request, fixture_.context);
            Assert.Equal(0, response.Status.Code);
            Assert.Equal(10, response.Total);
            Assert.Equal(2, response.Contents.Count);
            Assert.Contains("#6", response.Contents[0].Name);
        }

        {
            foreach (var uuid in uuids)
            {
                var request = new UuidRequest();
                request.Uuid = uuid;
                var response = await fixture_.getServiceContent().Delete(request, fixture_.context);
                Assert.Equal(0, response.Status.Code);
            }
        }
    }

    public override Task MatchTest()
    {
        throw new NotImplementedException();
    }

    public override Task PrepareUploadTest()
    {
        throw new NotImplementedException();
    }

    public override Task FlushUploadTest()
    {
        throw new NotImplementedException();
    }

    public override Task FetchAttachmentsTest()
    {
        throw new NotImplementedException();
    }
}
