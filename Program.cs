using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LifeCenter.Data;
using LifeCenter.Helpers;
using LifeCenter.Repositories;
using LifeCenter.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<IPersonalRepository, PersonalRepository>();
builder.Services.AddScoped<IPersonalService, PersonalService>();

var app = builder.Build();
// ensure database and tables exist
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Init();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

// https://github.dev/cornflourblue/dotnet-7-dapper-mysql-crud-api/blob/master/