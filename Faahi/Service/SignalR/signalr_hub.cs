using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Faahi.Service.SignalR
{
    [Authorize]
    public class signalr_hub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            var companyId = Context.User?.FindFirst("company_id")?.Value;

            if (!string.IsNullOrEmpty(companyId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, companyId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var companyId = Context.User?.FindFirst("company_id")?.Value;

            if (!string.IsNullOrEmpty(companyId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, companyId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}

