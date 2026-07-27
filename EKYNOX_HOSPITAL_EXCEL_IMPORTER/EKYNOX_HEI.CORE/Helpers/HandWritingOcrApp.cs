using Azure;
using Azure.AI.Vision.ImageAnalysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace EKYNOX_HEI.CORE.Helpers
{
    public class ParticipantDto
    {
        [JsonPropertyName("class_no")]
        public int ClassNo { get; set; }

        [JsonPropertyName("name_surname")]
        public string NameSurname { get; set; } = string.Empty;
    }

    public class ResponseDto
    {
        [JsonPropertyName("participants")]
        public List<ParticipantDto> Participants { get; set; } = new List<ParticipantDto>();
    }

    public class HandWritingOcrApp
    {
        private string endpoint { get; set; }
        private string apiKey { get; set; }

        public HandWritingOcrApp(string _endpoint, string _apiKey)
        {
            endpoint = _endpoint;
            apiKey = _apiKey;
        }

        public async Task<ReturnData<ResponseDto>> OcrProcess(byte[] imageBytes) 
        {
            var result = new ReturnData<ResponseDto>();

            try
            {
                var client = new ImageAnalysisClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

                ImageAnalysisResult response = await client.AnalyzeAsync(BinaryData.FromBytes(imageBytes), VisualFeatures.Read);

                var detectedLines = new List<string>();
                if (response.Read != null)
                {
                    foreach (var block in response.Read.Blocks)
                    {
                        foreach (var line in block.Lines)
                        {
                            detectedLines.Add(line.Text);
                        }
                    }
                }

                result.Data = ParseAndFormatParticipants(detectedLines);
            }
            catch (Exception ex)
            {
                result.Status = Enums.StatusEnum.Error;
                result.Message = ex.Message;
            }

            return result;            
        }

        private static ResponseDto ParseAndFormatParticipants(List<string> lines)
        {
            var response = new ResponseDto();
            var trCulture = new CultureInfo("tr-TR"); // Türkçe karakter dönüşümü için (ı->I, i->İ)

            int classCounter = 1;

            foreach (var line in lines)
            {
                string cleanLine = line.Trim();

                // İsteğe bağlı: Başlıkları veya tablo harici alanları es geçmek için filtre koyabilirsiniz.
                // Burada her satırı büyük harfe çevirip ekliyoruz:
                string upperName = cleanLine.ToUpper(trCulture);

                response.Participants.Add(new ParticipantDto
                {
                    ClassNo = classCounter++,
                    NameSurname = upperName
                });
            }

            return response;
        }
    }
}
