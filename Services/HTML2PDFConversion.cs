using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;
using HtmlToPdfConverter.Services;
//using htmltopdf;

namespace AmbienteDeTestesHTML2PDF.Services
{
    public class HTML2PDFConversionService
    {
        public async Task<MemoryStream> ConvertToPdf(IFormFile file, string library)
        {
            var stopwatch = Stopwatch.StartNew();
            long memoryUsed = 0;

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Por favor, selecione um arquivo.");
            }

            QuestPDF.Settings.License = LicenseType.Community;

            try
            {
                string fileContent;

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                var memoryStream = new MemoryStream();

                if (library == "lib1")
                {
                    var converter = new HtmlToPdfConverterService();
                    await converter.ConvertAsync(fileContent, string.Empty, memoryStream);
                }

                memoryStream.Position = 0;
                return memoryStream;
            }
            finally
            {
                stopwatch.Stop();
                memoryUsed = Process.GetCurrentProcess().PrivateMemorySize64;

                var conversionData = new
                {
                    FileName = file.FileName,
                    ConversionDate = DateTime.Now,
                    ExecutionTime = stopwatch.ElapsedMilliseconds,
                    MemoryUsage = memoryUsed
                };

                string json = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                string jsonFilePath = Path.Combine("Assets", "dados.json");
                await File.WriteAllTextAsync(jsonFilePath, json);
            }
        }
    }
}
