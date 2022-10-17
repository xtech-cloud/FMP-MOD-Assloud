using Microsoft.AspNetCore.Components;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using XTC.FMP.MOD.Assloud.LIB.MVCS;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using AntDesign;
using AntDesign.TableModels;
using Newtonsoft.Json;

namespace XTC.FMP.MOD.Assloud.LIB.Razor
{
    public partial class ContentComponent
    {
        public class ContentUiBridge : IContentUiBridge
        {

            public ContentUiBridge(ContentComponent _razor)
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
                var dto = _dto as ContentRetrieveResponseDTO;
            }

            public void RefreshDelete(IDTO _dto, object? _context)
            {
                var dto = _dto as UuidResponseDTO;
                if (null == dto)
                    return;
                razor_.tableModel.RemoveAll((_item) =>
                {
                    return _item.entity?.Uuid?.Equals(dto.Value.Uuid) ?? false;
                });
                razor_.selectedModel = null;
            }

            public void RefreshList(IDTO _dto, object? _context)
            {
                var dto = _dto as ContentListResponseDTO;
                if (null == dto)
                    return;

                razor_.tableTotal = (int)dto.Value.Total;
                razor_.tableModel.Clear();
                foreach (var content in dto.Value.Contents)
                {
                    var item = new TableModel(content);
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

            public void RefreshMatch(IDTO _dto, object? _context)
            {
                throw new NotImplementedException();
            }

            public void RefreshPrepareUpload(IDTO _dto, object? _context)
            {
                var dto = _dto as PrepareUploadResponseDTO;
                if (null == dto)
                    return;

                Task.Run(async () => await razor_.upload(dto.Value.Filepath, dto.Value.Url));
            }

            public void RefreshFlushUpload(IDTO _dto, object? _context)
            {
                var dto = _dto as FlushUploadResponseDTO;
                var file = razor_.uploadFiles_.Find((_item) =>
                {
                    return _item.browserFile.Name.Equals(dto.Value.Filepath);
                });
                if (null == file)
                    return;
                file.percentage = 100;
                razor_.uploadFiles_.Remove(file);
                razor_.StateHasChanged();
                Task.Run(async () => await razor_.fetchAttachments());
            }

            public void RefreshFetchAttachments(IDTO _dto, object? _context)
            {
                var dto = _dto as ContentFetchAttachmentsResponseDTO;
                var item = razor_.tableModel.Find((_item) =>
                {
                    return _item.entity.Uuid?.Equals(dto?.Value.Uuid) ?? false;
                });
                if (null == item)
                    return;
                item._attachments = dto?.Value.Attachments.ToArray() ?? new FileSubEntity[0];
                razor_.StateHasChanged();
            }

            private ContentComponent razor_;
        }

        [Inject] NavigationManager? navigationMgr_ { get; set; } = null;
        private string bundle_uuid = "";
        private string bundle_name = "";

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            searchFormData[SearchField.Name.GetHashCode()] = new FormValue { Text = "Ãû³Æ", Value = "" };
            searchFormData[SearchField.Bundle.GetHashCode()] = new FormValue { Text = "°ü", Value = "" };
            //searchFormData[SearchField.Name.GetHashCode()] = new FormValue { Text = "Ãû³Æ", Value = "" };

            var query = new Uri(navigationMgr_?.Uri ?? "").Query;
            var vals = query.Replace("?", "").Split("&");
            foreach (var val in vals)
            {
                if (val.Contains("bundle_uuid="))
                {
                    bundle_uuid = val.Replace("bundle_uuid=", "");
                }
                else if (val.Contains("bundle_name="))
                {
                    bundle_name = val.Replace("bundle_name=", "");
                }
            }

            if (!string.IsNullOrEmpty(bundle_name))
            {
                searchFormData[SearchField.Bundle.GetHashCode()].Value = bundle_name;
            }

            await listAll();
        }

        private async Task fetchAttachments()
        {
            if (null == selectedModel)
                return;

            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new UuidRequest();
            req.Uuid = selectedModel?.entity.Uuid;
            var dto = new UuidRequestDTO(req);
            Error err = await bridge.OnFetchAttachmentsSubmit(dto, null);

            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }
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
            await fetchAttachments();
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
            Bundle,
            Name,
        }

        private async void onSearchSubmit(EditContext _context)
        {
            searchLoading = true;
            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new ContentSearchRequest();
            req.Offset = (tablePageIndex - 1) * tablePageSize;
            req.Count = tablePageSize;
            req.Name = searchFormData[SearchField.Name.GetHashCode()].Value ?? "";
            var dto = new ContentSearchRequestDTO(req);
            Error err = await bridge.OnSearchSubmit(dto, null);
            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }

        }

        private async void onSearchResetClick()
        {
            bundle_name = "";
            bundle_uuid = "";
            searchFormData[SearchField.Bundle.GetHashCode()].Value = "";
            searchForm?.Reset();
            await listAll();
        }
        #endregion

        #region Create Modal
        private class CreateModel
        {
            [Required]
            public string? Name { get; set; }

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
            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
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
            var req = new ContentCreateRequest();
            req.BundleUuid = bundle_uuid;
            req.Name = model.Name;
            var dto = new ContentCreateRequestDTO(req);
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
            public UpdateModel(ContentEntity _entity)
            {
                this.entity = _entity;
                _Labels = "";
                foreach (var label in _entity.Labels)
                    _Labels += label + ";";
                _Tags = "";
                foreach (var tag in _entity.Tags)
                    _Tags += tag + ";";
                foreach (var pair in _entity.Kv)
                {
                    _InputKeyValuePair.Add(new InputKeyValuePair()
                    {
                        Key = pair.Key,
                        Value = pair.Value
                    });
                }
            }

            public class InputKeyValuePair
            {
                public string Key { get; set; } = "";
                public string Value { get; set; } = "";
            }

            public ContentEntity entity { get; private set; }

            public List<InputKeyValuePair> _InputKeyValuePair { get; set; } = new List<InputKeyValuePair>();
            public string _Labels { get; set; }
            public string _Tags { get; set; }
        }

        private bool visibleUpdateModal = false;
        private bool updateLoading = false;
        private AntDesign.Internal.IForm? updateForm;
        private UpdateModel? updateModel = null;

        private void onUpdateClick(string? _uuid)
        {
            if (string.IsNullOrEmpty(_uuid))
                return;

            var content = tableModel.Find((x) =>
            {
                if (string.IsNullOrEmpty(x.entity?.Uuid))
                    return false;
                return x.entity.Uuid.Equals(_uuid);
            });
            if (null == content || null == content.entity)
                return;

            visibleUpdateModal = true;
            var entityClone = Utilities.DeepCloneContentEntity(content.entity);
            updateModel = new UpdateModel(entityClone);
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
            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
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
            if (null == model.entity)
            {
                logger_?.Error("model.entity is null");
                return;
            }
            var req = new ContentUpdateRequest();
            req.Uuid = model.entity.Uuid;
            req.Name = model.entity.Name;
            req.Alias = model.entity.Alias;
            req.Title = model.entity.Title;
            req.Caption = model.entity.Caption;
            req.Label = model.entity.Label;
            req.Topic = model.entity.Topic;
            req.Description = model.entity.Description;
            string[] tags = model._Tags?.Split(";") ?? new string[0];
            foreach (var tag in tags)
            {
                if (string.IsNullOrWhiteSpace(tag))
                    continue;
                req.Tags.Add(tag);
            }
            //req.Labels = "";
            foreach (var pair in model._InputKeyValuePair)
            {
                req.Kv.Add(pair.Key, pair.Value);
            }
            var dto = new ContentUpdateRequestDTO(req);
            Error err = await bridge.OnUpdateSubmit(dto, null);
            if (null != err)
            {
                logger_?.Error(err.getMessage());
            }
        }

        private void onAddKV()
        {
            if (null == updateModel)
                return;
            if (null == updateModel.entity)
                return;
            updateModel._InputKeyValuePair.Add(
                new UpdateModel.InputKeyValuePair
                {
                    Key = String.Format("{0}", updateModel.entity.Kv.Count + 1),
                    Value = ""
                }
            );
        }

        private void onDeleteKV(string _key)
        {
            if (null == updateModel)
                return;
            if (null == updateModel.entity)
                return;
            var found = updateModel._InputKeyValuePair.Find((_item) =>
            {
                return _item.Key == _key;
            });
            if (null == found)
                return;
            updateModel._InputKeyValuePair.Remove(found);
        }
        #endregion

        #region Table
        private class TableModel
        {
            public TableModel(ContentEntity _entity)
            {
                entity = _entity;
                _meta = JsonConvert.SerializeObject(_entity, Formatting.Indented);
            }

            public ContentEntity entity { get; private set; }
            public string _meta { get; private set; }

            public FileSubEntity[] _attachments { get; set; } = new FileSubEntity[0];
        }


        private List<TableModel> tableModel = new();
        private TableModel? selectedModel = null;
        private int tableTotal = 0;
        private int tablePageIndex = 1;
        private int tablePageSize = 50;

        private async Task listAll()
        {
            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new ContentListRequest();
            req.Offset = (tablePageIndex - 1) * tablePageSize;
            req.Count = tablePageSize;
            req.BundleUuid = bundle_uuid;
            var dto = new ContentListRequestDTO(req);
            Error err = await bridge.OnListSubmit(dto, null);
            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }
        }

        private async Task onConfirmDelete(string? _uuid)
        {
            if (string.IsNullOrEmpty(_uuid))
                return;

            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
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

        private void onCancelDelete()
        {
            //Nothing to do
        }

        private async void onPageIndexChanged(PaginationEventArgs args)
        {
            tablePageIndex = args.Page;
            await listAll();
        }
        #endregion

        #region Upload
        public class UploadFile
        {
            public UploadFile(IBrowserFile _file)
            {
                browserFile = _file;
            }
            public string contentUUID = "";
            public IBrowserFile browserFile { get; private set; }
            public string uploadUrl = "";
            public int percentage = 0;
        }

        private List<UploadFile> uploadFiles_ = new List<UploadFile>();

        private async Task onUploadFilesClick(InputFileChangeEventArgs _e)
        {
            uploadFiles_.Clear();
            if (null == selectedModel)
                return;
            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }

            int maxAllowedFiles = 100;
            foreach (var file in _e.GetMultipleFiles(maxAllowedFiles))
            {
                var req = new PrepareUploadRequest();
                req.Uuid = selectedModel?.entity.Uuid ?? "";
                req.Filepath = string.Format("{0}", file.Name);
                var dto = new PrepareUploadRequestDTO(req);
                Error err = await bridge.OnPrepareUploadSubmit(dto, null);
                if (!Error.IsOK(err))
                {
                    logger_?.Error(err.getMessage());
                }
                var uploadFile = new UploadFile(file);
                uploadFile.contentUUID = selectedModel?.entity.Uuid ?? "";
                uploadFiles_.Add(uploadFile);
            }
        }

        private async Task upload(string _filepath, string _url)
        {
            var uploadfile = uploadFiles_.Find((_item) =>
            {
                return _item.browserFile.Name.Equals(_filepath);
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

            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }
            var req = new FlushUploadRequest();
            req.Uuid = uploadfile.contentUUID;
            req.Filepath = uploadfile.browserFile.Name;
            var dto = new FlushUploadRequestDTO(req);
            Error err = await bridge.OnFlushUploadSubmit(dto, null);
            if (!Error.IsOK(err))
            {
                logger_?.Error(err.getMessage());
            }

        }


        #endregion
    }
}
