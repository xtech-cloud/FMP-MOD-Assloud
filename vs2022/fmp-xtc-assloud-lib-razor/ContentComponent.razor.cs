
using Microsoft.AspNetCore.Components;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;
using XTC.FMP.MOD.Assloud.LIB.Bridge;
using XTC.FMP.MOD.Assloud.LIB.MVCS;

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

            public void Alert(string _code, string _message, SynchronizationContext? _context)
            {
                if (null == razor_.messageService_)
                    return;
                Task.Run(async () =>
                {
                    await razor_.messageService_.Error(_message);
                }); 
            }


            public void RefreshMatch(IDTO _dto, SynchronizationContext? _context)
            {
                var dto = _dto as ContentListResponseDTO;
                razor_.__debugMatch = dto?.Value.ToString();
            }


            private ContentComponent razor_;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task __debugClick()
        {
            var bridge = (getFacade()?.getViewBridge() as IContentViewBridge);
            if (null == bridge)
            {
                logger_?.Error("bridge is null");
                return;
            }

            var reqMatch = new ContentMatchRequest();
            var dtoMatch = new ContentMatchRequestDTO(reqMatch);
            logger_?.Trace("invoke OnMatchSubmit");
            await bridge.OnMatchSubmit(dtoMatch, null);

        }


        private string? __debugMatch;

    }
}
