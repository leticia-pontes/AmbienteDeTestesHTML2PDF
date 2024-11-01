using AmbienteDeTestesHTML2PDF.Components;
using AmbienteDeTestesHTML2PDF.Services;
using HTML2PDF_v1.Services;
using System.IO;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Version 1
builder.Services.AddSingleton<HTML2PDF_v1Service>(); // Biblioteca
builder.Services.AddSingleton<HTML2PDF_v1ConverterService>(); // Serviço do Programa
// Version 3
builder.Services.AddSingleton<HTML2PDF_v3ConverterService>(); // Serviço do Programa

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();