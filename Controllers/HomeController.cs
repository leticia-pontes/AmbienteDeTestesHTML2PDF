using System.IO;
using System.Threading.Tasks;
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
            // Verifica se um arquivo foi enviado
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Por favor, selecione um arquivo.";
                return View("Index");
            }

            // Define licença do QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            try
            {
                // Lê o conteúdo do arquivo enviado (txt ou html)
                string fileContent;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                // Instancia o serviço da biblioteca
                var converter = new HtmlToPdfConverterService();

                // Gera o PDF em memória
                using (var memoryStream = new MemoryStream())
                {
                    // Realiza a conversão do conteúdo HTML para PDF
                    await converter.ConvertAsync(fileContent, string.Empty, memoryStream); // Usa o método da biblioteca
                    memoryStream.Position = 0; // Reinicia a posição do stream para leitura

                    // Retorna o PDF gerado como arquivo de download
                    return File(memoryStream.ToArray(), "application/pdf", "output.pdf");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ocorreu um erro: {ex.Message}";
                return View("Index");
            }
        }

        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}
