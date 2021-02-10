using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Antoine.Berthier.Ocr.Console
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var images = new List<byte[]>();
            foreach (var path in args)
            {
                var imageBytes = await File.ReadAllBytesAsync(path);
                images.Add(imageBytes);
            }
            
            var ocrResults = await new Ocr().ReadAsync(images);
            
            foreach (var ocrResult in ocrResults)
            {
                System.Console.WriteLine($"Confidence :{ocrResult.Confidence}");
                System.Console.WriteLine($"Text :{ocrResult.Text}");
            } 
            
        }
    }
}