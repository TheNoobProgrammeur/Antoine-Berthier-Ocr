using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Tesseract;

namespace Antoine.Berthier.Ocr
{
    public class OcrResult
    {
        public string Text { get; set; }
        public float Confidence { get; set; }
    }

    public class Ocr
    {
        
        public async Task<OcrResult[]> ReadAsync(List<byte[]> images)
        {
            OcrResult[] ocrResults = new OcrResult[images.Count];
            int index = 0;
            var ocrtext = string.Empty;
            float conf;
            var engine = new TesseractEngine(@"D:\RaiderProject\Antoine.Berthier\Antoine.Berthier.Ocr\tessdata", "fra", EngineMode.Default);
            foreach (byte[] image in images)
            {

                using var pix = Pix.LoadFromMemory(image); 
                using (var page = engine.Process(pix)) {
                    ocrtext = page.GetText();
                    conf = page.GetMeanConfidence();
                    OcrResult ocrResultCurant = new OcrResult();
                    ocrResultCurant.Text = ocrtext.Replace('\n', ' ');
                    ocrResultCurant.Confidence = conf;
                    ocrResults[index] = ocrResultCurant;
                }

                index += 1;

            }
            
            return ocrResults;
        }
    }
    
    
}