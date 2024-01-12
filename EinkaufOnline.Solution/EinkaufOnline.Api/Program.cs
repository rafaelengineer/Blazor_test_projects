using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using EinkaufOnline.Api.Data;
using EinkaufOnline.Api.Repositories.Contracts;
using EinkaufOnline.Api.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//***IMPORTANT INSTRUCTION HERE - MUST CONFIGURE CONNECTION BEFORE RUNNING MIGRATIONS
builder.Services.AddDbContextPool<EinkaufOnlineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EinkaufOnlineConnection"))
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();//von tutorial file
    app.UseSwaggerUI();//Von Tutorial file
    app.UseExceptionHandler("/Error");//Alt
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5290", "http://localhost:5290")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);

//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



