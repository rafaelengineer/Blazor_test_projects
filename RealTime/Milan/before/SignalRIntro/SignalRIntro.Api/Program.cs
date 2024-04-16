using Microsoft.AspNetCore.SignalR;
using SignalRIntro.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR ();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("broadcast");

app.UseHttpsRedirection();

app.MapHub<ChatHub>("chat-hub");

app.Run();
