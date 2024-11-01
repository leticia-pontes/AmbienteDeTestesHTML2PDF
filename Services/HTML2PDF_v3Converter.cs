using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using HTML2PDF_v3;

namespace AmbienteDeTestesHTML2PDF.Services
{
    public class HTML2PDF_v3ConverterService
    {
        public async Task<MemoryStream> ConvertToPdf(IFormFile file)
        {
            var stopwatch = Stopwatch.StartNew();

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Por favor, selecione um arquivo.");
            }

            try
            {
                string htmlContent;

                // Lendo o conteúdo do arquivo HTML
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    htmlContent = await reader.ReadToEndAsync();
                }

                // Preparando o MemoryStream para armazenar o PDF
                var memoryStream = new MemoryStream();

                // Usando o PDFGenerator para gerar o PDF a partir do conteúdo HTML
                var pdfGenerator = new PDFGenerator();
                pdfGenerator.GeneratePDF(htmlContent, memoryStream);

                memoryStream.Position = 0; // Resetando a posição para o início do stream
                return memoryStream;
            }
            finally
            {
                stopwatch.Stop();

                // Dados de conversão para o JSON
                var conversionData = new
                {
                    FileName = file.FileName,
                    ConversionDate = DateTime.Now,
                    ExecutionTime = stopwatch.ElapsedMilliseconds,
                    WorkingSetMemory = Process.GetCurrentProcess().WorkingSet64
                };

                // Escrevendo os dados de conversão em um arquivo JSON
                string jsonFilePath = Path.Combine("Assets", "dados.json");
                string json = JsonConvert.SerializeObject(conversionData, Formatting.Indented);
                await File.WriteAllTextAsync(jsonFilePath, json);
            }
        }
    }
}
