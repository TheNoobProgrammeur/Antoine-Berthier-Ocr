using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit; 

namespace Antoine.Berthier.Ocr.Tests
{
    public class OcrUnitTest
    {
        [Fact]
        public async Task ImagesShouldBeReadCorrectly()
        {
            var executingPath = GetExecutingPath();
            var images = new List<byte[]>();
            foreach (var imagePath in
                Directory.EnumerateFiles(Path.Combine(executingPath, "images")))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                images.Add(imageBytes);
            }

            var ocrResults = await new Ocr().ReadAsync(images);

            Assert.Equal(ocrResults[0].Text,
                @"Dans de nombreuses technologies, il existe des certifications. Le monde Microsoft en propose de nombreuses pour ");
            Assert.Equal(ocrResults[0].Confidence, 0.949999988079071);
            Assert.Equal(ocrResults[1].Text,@"ARRÉTONS DE PARLER DE DÉVELOPPEUR .NET ! " );
            Assert.Equal(ocrResults[1].Confidence, 0.8999999761581421);
            Assert.Equal(ocrResults[2].Text, @"Ë développeur .Net, ou plus largement ur les technologies Microsoft, n'est pas un profil rare. La popularité de C# ");
            Assert.Equal(ocrResults[2].Confidence, 0.8899999856948853);
        }
        private static string GetExecutingPath()
        {
            var executingAssemblyPath =
                Assembly.GetExecutingAssembly().Location;
            var executingPath =
                Path.GetDirectoryName(executingAssemblyPath);
            return executingPath;
        }
    }
}