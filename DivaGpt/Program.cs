using DivaGpt.Data;
using DivaGpt.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<IChatService, ChatService>();
builder.Services.AddTransient<IChatFormatService, ChatFormatService>();
builder.Services.AddScoped<IOpenAiService, OpenAiService>();                // Scoped�Ƃ��邱�ƂŃ����[�h������Z�b�V�����؂��悤�ɂ���
builder.Services.Configure<OpenAiOption>(builder.Configuration.GetSection("OpenAiSettings"));
builder.Services.Configure<ChatOption>(builder.Configuration.GetSection("ChatSettings"));
builder.Services.AddHotKeys2(); // �L�[�{�[�h�V���[�g�J�b�g������

var app = builder.Build();

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
