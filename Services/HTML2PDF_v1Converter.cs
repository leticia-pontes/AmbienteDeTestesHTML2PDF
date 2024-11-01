using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;
using HTML2PDF_v1.Services;

namespace AmbienteDeTestesHTML2PDF.Services
{
    public class HTML2PDF_v1ConverterService
    {
        public async Task<MemoryStream> ConvertToPdf(IFormFile file)
        {
            var stopwatch = Stopwatch.StartNew();

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

                var converter = new HTML2PDF_v1Service();
                await converter.ConvertAsync(fileContent, string.Empty, memoryStream);

                memoryStream.Position = 0;
                return memoryStream;
            }
            finally
            {
                stopwatch.Stop();

                var conversionData = new
                {
                    FileName = file.FileName,
                    ConversionDate = DateTime.Now,
                    ExecutionTime = stopwatch.ElapsedMilliseconds,
                    WorkingSetMemory = Process.GetCurrentProcess().WorkingSet64
                };

                string json = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                string jsonFilePath = Path.Combine("Assets", "dados.json");
                await File.WriteAllTextAsync(jsonFilePath, json);
            }
        }
    }
}
