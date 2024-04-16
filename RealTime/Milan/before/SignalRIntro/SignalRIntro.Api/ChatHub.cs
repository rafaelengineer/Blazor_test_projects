using System;
using Microsoft.AspNetCore.SignalR;


namespace SignalRIntro.Api;

public sealed class ChatHub : Hub<IChatClient>
{
	public override async Task OnConnectedAsync()
	{
		await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
		// return base.OnConnectedAsync();
	}
	public async Task SendMessage(string message)
	{
		await Clients.All.ReceiveMessage($"{Context.ConnectionId}: {message}");
	}
}
