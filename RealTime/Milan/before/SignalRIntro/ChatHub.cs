using System;
using Microsoft.AspNetCore.SignalR;


namespace SignalRIntro.Api;

public sealed class Chathub : Hub
{
	public override Task OnConnectedAsync()
	{
		return base.OnConnectedAsync();
	}
}
