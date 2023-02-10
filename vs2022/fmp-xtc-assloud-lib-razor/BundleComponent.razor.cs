using Microsoft.AspNetCore.Components;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using XTC.FMP.MOD.Assloud.LIB.MVCS;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using AntDesign;
using static XTC.FMP.MOD.Assloud.LIB.Razor.BundleComponent;
using AntDesign.TableModels;
using System.Net.Http.Headers;
using System.Text;

namespace XTC.FMP.MOD.Assloud.LIB.Razor
{
    public partial class BundleComponent
    {
        public class BundleUiBridge : IBundleUiBridge
        {

            public BundleUiBridge(BundleComponent _razor)
            {
                razor_ = _razor;
            }

            public void Alert(string _code, string _message, object? _context)
            {
                if (null == razor_.messageService_)
                    return;
                Task.Run(async () =>
                {
                    await razor_.messageService_.Error(_message);
                    razor_.createLoading = false;
                    razor_.StateHasChanged();
                });
            }


            public void RefreshCreate(IDTO _dto, object? _context)
            {
                razor_.createLoading = false;
                razor_.visibleCreateModal = false;
                razor_.StateHasChanged();

                Task.Run(async () =>
                {
                    await razor_.listAll();
                });
            }

            public void RefreshUpdate(IDTO _dto, object? _context)
            {
                razor_.updateLoading = false;
                razor_.visibleUpdateModal = false;
                razor_.StateHasChanged();

                Task.Run(async () =>
                {
                    await razor_.listAll();
                });
            }


            public void RefreshRetrieve(IDTO _dto, object? _context)
            {
                var dto = _dto as BundleRetrieveResponseDTO;
            }

            public void RefreshDelete(IDTO _dto, object? _context)
            {
                var dto = _dto as UuidResponseDTO;
                if (null == dto)
                    return;
                razor_.tableModel.RemoveAll((_item) =>
                {
                    return _item.Uuid?.Equals(dto.Value.Uuid) ?? false;
                });
                razor_.selectedModel = null;
            }

            public void RefreshList(IDTO _dto, object? _context)
            {
                var dto = _dto as BundleListResponseDTO;
                if (null == dto)
                    return;

                razor_.tableTotal = (int)dto.Value.Total;
                razor_.tableModel.Clear();
                foreach (var bundle in dto.Value.BundleS)
                {
                    var item = new TableModel
                    {
                        Uuid = bundle.Uuid,
                        Name = bundle.Name,
                        Summary = bundle.Summary,
                    };
                    foreach (var label in bundle.Labels)
                    {
                        item.Labels.Add(label);
                    }
                    foreach (var tag in bundle.Tags)
                    {
                        item.Tags.Add(tag);
                    }
                    razor_.tableModel.Add(item);
                }
                razor_.selectedModel = null;
                razor_.StateHasChanged();
            }

            public void RefreshSearch(IDTO _dto, object? _context)
            {
                razor_.searchLoading = false;
                RefreshList(_dto, _context);
            }

            public void RefreshPrepareUploadResource(IDTO _dto, object? _context)
            {
                var dto = _dto as PrepareUploadResponseDTO;
                if (null == dto)
                    return;

                Task.Run(async () => await razor_.upload(dto.Value.Filepath, dto.Value.Url));
            }

            public void RefreshFlushUploadResource(IDTO _dto, object? _context)
            {
                var dto = _dto as FlushUploadResponseDTO;
                var file = razor_.uploadFiles_.Find((_item) =>
                {
                    return string.Format("{0}/{1}", razor_.uploadPathPrefix_, _item.browserFile.Name).Equals(dto.Value.Filepath);
                });
                if (null == file)
                    return;
                file.percentage = 100;
                razor_.uploadFiles_.Remove(file);
                razor_.StateHasChanged();
                Task.Run(async () => await razor_.fetchAssets());
            }

            public void RefreshFetchResources(IDTO _dto, object? _context)
            {
                var dto = _dto as BundleFetchResourcesResponseDTO;
                var item = razor_.tableModel.Find((_item) =>
                {
                    return _item.Uuid?.Equals(dto?.Value.Uuid) ?? false;
                });
                if (null == item)
                    return;
                item.ResourceS = dto?.Value.ResourceS.ToArray() ?? new FileSubEntity[0];
                razor_.StateHasChanged();
            }

            public void RefreshTidy(IDTO _dto, object? _context)
            {
            }

            public void RefreshDeleteResource(IDTO _dto, object? _context)
            {
            }

            private BundleComponent razor_;
        }

        [Inject] NavigationManager? navigationMgr_ { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            searchFormData[SearchField.Name.GetHashCode()] = new FormValue { Text = "名称", Value = "" };
            //searchFormData[SearchField.Name.GetHashCode()] = new FormValue { Text = "名称", Value = "" };
            await listAll();
        }

        private async Task onTableRowClick(RowData<TableModel> _data)
        {
            var item = _data.Data;
            if (null == item)
            {
                selectedModel = null;
                return;
            }

            selectedModel = item;
            await fetchAssets();
        }

        private async Task fetchAssets()
        {
            if (null == selectedModel)
                return;

            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new UuidRequest();
            req.Uuid = selectedModel?.Uuid;
            var dto = new UuidRequestDTO(req);
            Error err = await bridge.OnFetchResourcesSubmit(dto, null);

            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }
        }

        private void onBrowseClick(string? _uuid)
        {
            if (string.IsNullOrEmpty(_uuid))
                return;

            var bundle = tableModel.Find((x) =>
            {
                if (string.IsNullOrEmpty(x.Uuid))
                    return false;
                return x.Uuid.Equals(_uuid);
            });
            if (null == bundle)
                return;

            string uri = string.Format("/xtc/assloud/content?bundle_uuid={0}&bundle_name={1}", bundle.Uuid, bundle.Name);
            navigationMgr_?.NavigateTo(uri);
        }

        private void onTidyClick()
        {
            visibleCreateModal = true;
        }

        #region Search
        private class FormValue
        {
            public string? Text { get; set; }
            public string? Value { get; set; }
        }

        private bool searchLoading = false;
        private AntDesign.Internal.IForm? searchForm;
        private Dictionary<int, FormValue> searchFormData = new();
        private bool searchExpand = false;

        private enum SearchField
        {
            Name,
        }

        private async void onSearchSubmit(EditContext _context)
        {
            searchLoading = true;
            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new BundleSearchRequest();
            req.Offset = (tablePageIndex - 1) * tablePageSize;
            req.Count = tablePageSize;
            req.Name = searchFormData[SearchField.Name.GetHashCode()].Value ?? "";
            var dto = new BundleSearchRequestDTO(req);
            Error err = await bridge.OnSearchSubmit(dto, null);
            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }

        }

        private async void onSearchResetClick()
        {
            searchForm?.Reset();
            await listAll();
        }
        #endregion

        #region Create Modal
        private class CreateModel
        {
            [Required]
            public string? Name { get; set; }

            public string? Summary { get; set; }
        }

        private bool visibleCreateModal = false;
        private bool createLoading = false;
        private AntDesign.Internal.IForm? createForm;
        private CreateModel createModel = new();

        private void onCreateClick()
        {
            visibleCreateModal = true;
        }

        private void onCreateModalOk()
        {
            createForm?.Submit();
        }

        private void onCreateModalCancel()
        {
            visibleCreateModal = false;
        }

        private async void onCreateSubmit(EditContext _context)
        {
            createLoading = true;
            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var model = _context.Model as CreateModel;
            if (null == model)
            {
                logger_?.Error("model is null");
                return;
            }
            var req = new BundleCreateRequest();
            req.Name = model.Name;
            req.Summary = model.Summary ?? "";
            var dto = new BundleCreateRequestDTO(req);
            Error err = await bridge.OnCreateSubmit(dto, null);
            if (null != err)
            {
                logger_?.Error(err.getMessage());
            }
        }


        #endregion

        #region Update Modal
        private class UpdateModel
        {
            public string? Uuid { get; set; }

            [Required]
            public string? Name { get; set; }

            public string? Summary { get; set; }

            public string? Labels { get; set; }
            public string? Tags { get; set; }
        }

        private bool visibleUpdateModal = false;
        private bool updateLoading = false;
        private AntDesign.Internal.IForm? updateForm;
        private UpdateModel updateModel = new();

        private void onUpdateClick(string? _uuid)
        {
            if (string.IsNullOrEmpty(_uuid))
                return;

            var bundle = tableModel.Find((x) =>
            {
                if (string.IsNullOrEmpty(x.Uuid))
                    return false;
                return x.Uuid.Equals(_uuid);
            });
            if (null == bundle)
                return;

            visibleUpdateModal = true;
            updateModel.Uuid = _uuid;
            updateModel.Name = bundle.Name ?? "";
            updateModel.Summary = bundle.Summary ?? "";
            updateModel.Labels = "";
            foreach (var label in bundle.Labels)
                updateModel.Labels += label + ";";
            updateModel.Tags = "";
            foreach (var tag in bundle.Tags)
                updateModel.Tags += tag + ";";
        }

        private async void onTidyClick(string? _uuid)
        {
            if (string.IsNullOrEmpty(_uuid))
                return;

            var bundle = tableModel.Find((x) =>
            {
                if (string.IsNullOrEmpty(x.Uuid))
                    return false;
                return x.Uuid.Equals(_uuid);
            });
            if (null == bundle)
                return;

            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new UuidRequest();
            req.Uuid = bundle.Uuid;
            var dto = new UuidRequestDTO(req);
            Error err = await bridge.OnTidySubmit(dto, null);
            if (null != err)
            {
                logger_?.Error(err.getMessage());
            }
        }

        private void onUpdateModalOk()
        {
            updateForm?.Submit();
        }

        private void onUpdateModalCancel()
        {
            visibleUpdateModal = false;
        }

        private async void onUpdateSubmit(EditContext _context)
        {
            updateLoading = true;
            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var model = _context.Model as UpdateModel;
            if (null == model)
            {
                logger_?.Error("model is null");
                return;
            }
            var req = new BundleUpdateRequest();
            req.Uuid = model.Uuid;
            req.Name = model.Name;
            req.Summary = model.Summary ?? "";
            string[] tags = model.Tags?.Split(";") ?? new string[0];
            foreach (var tag in tags)
            {
                if (string.IsNullOrWhiteSpace(tag))
                    continue;
                req.TagS.Add(tag);
            }
            //req.Labels = "";
            var dto = new BundleUpdateRequestDTO(req);
            Error err = await bridge.OnUpdateSubmit(dto, null);
            if (null != err)
            {
                logger_?.Error(err.getMessage());
            }
        }


        #endregion

        #region Table
        private class TableModel
        {
            public string? Uuid { get; set; }

            [DisplayName("名称")]
            public string? Name { get; set; }
            [DisplayName("简介 ")]
            public string? Summary { get; set; }
            [DisplayName("预设标签")]
            public List<string> Labels { get; set; } = new List<string>();
            [DisplayName("自定义标签")]
            public List<string> Tags { get; set; } = new List<string>();

            public FileSubEntity[] ResourceS { get; set; } = new FileSubEntity[0];
        }


        private List<TableModel> tableModel = new();
        private TableModel? selectedModel = null;
        private int tableTotal = 0;
        private int tablePageIndex = 1;
        private int tablePageSize = 50;

        private async Task listAll()
        {
            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new BundleListRequest();
            req.Offset = (tablePageIndex - 1) * tablePageSize;
            req.Count = tablePageSize;
            var dto = new BundleListRequestDTO(req);
            Error err = await bridge.OnListSubmit(dto, null);
            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }
        }

        private async Task onConfirmDeleteBundle(string? _uuid)
        {
            if (string.IsNullOrEmpty(_uuid))
                return;

            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new UuidRequest();
            req.Uuid = _uuid;
            var dto = new UuidRequestDTO(req);
            Error err = await bridge.OnDeleteSubmit(dto, null);
            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }
        }

        private void onCancelDeleteBundle()
        {
            //Nothing to do
        }

        private async void onPageIndexChanged(PaginationEventArgs args)
        {
            tablePageIndex = args.Page;
            await listAll();
        }

        private async Task onConfirmDeleteResource(string? _uuid)
        {
            if (string.IsNullOrEmpty(_uuid))
                return;

            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new UuidRequest();
            req.Uuid = _uuid;
            var dto = new UuidRequestDTO(req);
            Error err = await bridge.OnDeleteSubmit(dto, null);
            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }
        }

        private void onCancelDeleteResource()
        {
            //Nothing to do
        }
        #endregion

        #region Upload
        public class UploadFile
        {
            public UploadFile(IBrowserFile _file)
            {
                browserFile = _file;
            }
            public string bundleUUID = "";
            public IBrowserFile browserFile { get; private set; }
            public string uploadUrl = "";
            public int percentage = 0;
        }
        private string? uploadPathPrefix_;

        private List<UploadFile> uploadFiles_ = new List<UploadFile>();

        private async Task onUploadFilesClick(InputFileChangeEventArgs _e)
        {
            uploadFiles_.Clear();
            if (null == selectedModel)
                return;
            if (string.IsNullOrEmpty(uploadPathPrefix_))
                return;

            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }

            int maxAllowedFiles = 100;
            foreach (var file in _e.GetMultipleFiles(maxAllowedFiles))
            {
                var req = new PrepareUploadRequest();
                req.Uuid = selectedModel?.Uuid ?? "";
                req.Filepath = string.Format("{0}/{1}", uploadPathPrefix_, file.Name);
                var dto = new PrepareUploadRequestDTO(req);
                Error err = await bridge.OnPrepareUploadResourceSubmit(dto, null);
                if (!Error.IsOK(err))
                {
                    logger_?.Error(err.getMessage());
                }
                var uploadFile = new UploadFile(file);
                uploadFile.bundleUUID = selectedModel?.Uuid ?? "";
                uploadFiles_.Add(uploadFile);
            }
        }

        private async Task upload(string _filepath, string _url)
        {
            var uploadfile = uploadFiles_.Find((_item) =>
            {
                return string.Format("{0}/{1}", uploadPathPrefix_, _item.browserFile.Name).Equals(_filepath);
            });
            if (null == uploadfile)
                return;
            uploadfile.uploadUrl = _url;

            var httpClient = new HttpClient();
            bool success = false;
            try
            {
                long maxFileSize = long.MaxValue;
                var fileContent = new StreamContent(uploadfile.browserFile.OpenReadStream(maxFileSize));
                var response = await httpClient.PutAsync(new Uri(uploadfile.uploadUrl), fileContent);
                success = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                logger_?.Error(ex.Message);
            }

            if (!success)
                return;

            var bridge = (getFacade()?.getViewBridge() as IBundleViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new FlushUploadRequest();
            req.Uuid = uploadfile.bundleUUID;
            req.Filepath = string.Format("{0}/{1}", uploadPathPrefix_, uploadfile.browserFile.Name);
            var dto = new FlushUploadRequestDTO(req);
            Error err = await bridge.OnFlushUploadResourceSubmit(dto, null);
            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }

        }


        #endregion
    }
}
