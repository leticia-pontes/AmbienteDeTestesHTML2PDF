using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using HtmlToPdfConverter.Services;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;

namespace HtmlPreviewApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> ConvertToPdf(IFormFile file)
        {
            // Inicia a medi��o do tempo de execu��o
            var stopwatch = Stopwatch.StartNew();

            // Verifica se um arquivo foi enviado
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Por favor, selecione um arquivo.";
                return View("Index");
            }

            // Define licen�a do QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            long memoryUsed = 0; // Vari�vel para armazenar o consumo de mem�ria

            try
            {
                // L� o conte�do do arquivo enviado (txt ou html)
                string fileContent;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                // Instancia o servi�o da biblioteca
                var converter = new HtmlToPdfConverterService();

                // Gera o PDF em mem�ria
                using (var memoryStream = new MemoryStream())
                {
                    // Realiza a convers�o do conte�do HTML para PDF
                    await converter.ConvertAsync(fileContent, string.Empty, memoryStream); // Usa o m�todo da biblioteca
                    memoryStream.Position = 0; // Reinicia a posi��o do stream para leitura

                    // Retorna o PDF gerado como arquivo de download
                    return File(memoryStream.ToArray(), "application/pdf", "output.pdf");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ocorreu um erro: {ex.Message}";
                return View("Index");
            }
            finally
            {
                stopwatch.Stop(); // Para o temporizador

                // Coleta dados de consumo de mem�ria
                memoryUsed = Process.GetCurrentProcess().PrivateMemorySize64;

                // Cria um objeto com os dados que ser�o salvos no JSON
                var conversionData = new
                {
                    FileName = file.FileName,
                    ConversionDate = DateTime.Now,
                    ExecutionTime = stopwatch.ElapsedMilliseconds, // Tempo em milissegundos
                    MemoryUsage = memoryUsed // Consumo de mem�ria em bytes
                };

                // Gera um JSON a partir do objeto de convers�o
                string json = JsonConvert.SerializeObject(conversionData, Formatting.Indented);

                // Escreve o JSON em um arquivo
                string jsonFilePath = Path.Combine("Assets", "data.json");
                await System.IO.File.WriteAllTextAsync(jsonFilePath, json);
            }
        }

        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}
