using FlashTalk.Web.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FlashTalk.Web.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined!");
    }

    public async Task SendMessage(string message)
    {
        await Clients.All.ReceiveMessage($"{Context.ConnectionId}: {message}");
    }
     
}