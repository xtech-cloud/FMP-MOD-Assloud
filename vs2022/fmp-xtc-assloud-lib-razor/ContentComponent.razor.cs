using Microsoft.AspNetCore.Components;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using XTC.FMP.MOD.Assloud.LIB.MVCS;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using AntDesign;
using static XTC.FMP.MOD.Assloud.LIB.Razor.ContentComponent;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using XTC.FMP.MOD.Assloud.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;
using System;
using AntDesign.TableModels;
using System.Net.WebSockets;

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
                    return _item.Uuid?.Equals(dto.Value.Uuid) ?? false;
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
                    var item = new TableModel
                    {
                        Uuid = content.Uuid,
                        Name = content.Name,
                        Alias = content.Alias,
                        Title = content.Title,
                        Caption = content.Caption,
                        Label = content.Label,
                        Topic = content.Topic,
                        Description = content.Description,
                        Bundle = content.BundleName,
                    };
                    foreach (var label in content.Labels)
                    {
                        item.Labels.Add(label);
                    }
                    foreach (var tag in content.Tags)
                    {
                        item.Tags.Add(tag);
                    }
                    foreach (var pair in content.Kv)
                    {
                        item.KV.Add(pair.Key, pair.Value);
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

            public void RefreshMatch(IDTO _dto, object? _context)
            {
                throw new NotImplementedException();
            }

            private ContentComponent razor_;
        }

        [Inject] NavigationManager? navigationMgr_ { get; set; } = null;
        private string bundle_uuid = "";
        private string bundle_name = "";

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            searchFormData[SearchField.Name.GetHashCode()] = new FormValue { Text = "名称", Value = "" };
            searchFormData[SearchField.Bundle.GetHashCode()] = new FormValue { Text = "包", Value = "" };
            //searchFormData[SearchField.Name.GetHashCode()] = new FormValue { Text = "名称", Value = "" };

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

        private void onTableRowClick(RowData<TableModel> _data)
        {
            var item = _data.Data;
            if (null == item)
            {
                selectedModel = null;
                return;
            }

            selectedModel = item;
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
            public class Pair
            {
                public string Key { get; set; } = "";
                public string Value { get; set; } = "";
            }

            public string? Uuid { get; set; }

            [Required]
            public string? Name { get; set; }

            [Required]
            public string? Alias { get; set; }

            public List<Pair> KV { get; set; } = new List<Pair>();

            [Required]
            public string? Title { get; set; }
            public string? Caption { get; set; }
            public string? Label { get; set; }
            public string? Topic { get; set; }
            public string? Description { get; set; }

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

            var content = tableModel.Find((x) =>
            {
                if (string.IsNullOrEmpty(x.Uuid))
                    return false;
                return x.Uuid.Equals(_uuid);
            });
            if (null == content)
                return;

            visibleUpdateModal = true;
            updateModel.Uuid = _uuid;
            updateModel.Name = content.Name ?? "";
            updateModel.Alias = content.Alias ?? "";
            updateModel.Title = content.Title ?? "";
            updateModel.Caption = content.Caption ?? "";
            updateModel.Label = content.Label ?? "";
            updateModel.Topic = content.Topic ?? "";
            updateModel.Description = content.Description ?? "";
            updateModel.KV.Clear();
            foreach (var pair in content.KV)
            {
                updateModel.KV.Add(new UpdateModel.Pair
                {
                    Key = pair.Key,
                    Value = pair.Value,
                });
            }
            updateModel.Labels = "";
            foreach (var label in content.Labels)
                updateModel.Labels += label + ";";
            updateModel.Tags = "";
            foreach (var tag in content.Tags)
                updateModel.Tags += tag + ";";
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
            var req = new ContentUpdateRequest();
            req.Uuid = model.Uuid;
            req.Name = model.Name;
            req.Alias = model.Alias;
            req.Title = model.Title;
            req.Caption = model.Caption;
            req.Label = model.Label;
            req.Topic = model.Topic;
            req.Description = model.Description;
            string[] tags = model.Tags?.Split(";") ?? new string[0];
            foreach (var tag in tags)
            {
                if (string.IsNullOrWhiteSpace(tag))
                    continue;
                req.Tags.Add(tag);
            }
            //req.Labels = "";
            foreach (var pair in model.KV)
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

            updateModel.KV.Add(new UpdateModel.Pair
            {
                Key = string.Format("{0}", updateModel.KV.Count + 1),
                Value = "",
            });
        }
        #endregion

        #region Table
        private class TableModel
        {
            public string? Uuid { get; set; }

            [DisplayName("名称")]
            public string? Name { get; set; }
            [DisplayName("别名")]
            public string? Alias { get; set; }
            [DisplayName("主标题")]
            public string? Title { get; set; }
            [DisplayName("副标题")]
            public string? Caption { get; set; }
            [DisplayName("标签")]
            public string? Label { get; set; }
            [DisplayName("标语")]
            public string? Topic { get; set; }
            [DisplayName("描述")]
            public string? Description { get; set; }
            [DisplayName("键值")]
            public Dictionary<string, string> KV { get; set; } = new Dictionary<string, string>();
            [DisplayName("预设标签")]
            public List<string> Labels { get; set; } = new List<string>();
            [DisplayName("自定义标签")]
            public List<string> Tags { get; set; } = new List<string>();
            [DisplayName("包")]
            public string? Bundle { get; set; }
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
    }
}
