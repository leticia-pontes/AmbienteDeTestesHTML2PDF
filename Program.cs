using AmbienteDeTestesHTML2PDF.Components;
using AmbienteDeTestesHTML2PDF.Services;
using HtmlToPdfConverter.Services;
using System.IO;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<HtmlToPdfConverterService>(); // Biblioteca
builder.Services.AddSingleton<HTML2PDFConversionService>(); // Serviço do Programa

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