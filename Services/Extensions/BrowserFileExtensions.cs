using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

public static class BrowserFileExtensions
{
    public static async Task<IFormFile?> ToFormFileAsync(this IBrowserFile browserFile)
    {
        if (browserFile == null)
        {
            Console.WriteLine("O arquivo do navegador é nulo.");
            return null; // Permanece retornando null para manter a lógica original
        }

        var memoryStream = new MemoryStream();
        try
        {
            await browserFile.OpenReadStream(maxAllowedSize: 1024 * 1024 * 10).CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            Console.WriteLine($"Arquivo {browserFile.Name} lido com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao abrir o arquivo: {ex.Message}");
            return null; // Permanece retornando null para manter a lógica original
        }

        return new FormFile(memoryStream, 0, memoryStream.Length, browserFile.Name, browserFile.Name);
    }
}
