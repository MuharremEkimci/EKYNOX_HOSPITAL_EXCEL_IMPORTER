using Google.GenAI;
using Google.GenAI.Types;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EKYNOX_HEI.CORE.Helpers
{
    public class AIHelper
    {
        private readonly string apiKey;

        public AIHelper(string _apiKey)
        {
            apiKey = _apiKey;
        }

        [Display(Name = "Method", Description = "Gemini AI Methodu")]
        public async Task<ReturnData<string>> GeminiAIQuestion(string prompt, dynamic data, bool blImageUsing = false, bool blAnswerJsonTrigger = false)
        {
            var result = new ReturnData<string>();

            try
            {
                var googleAI = new Client(apiKey: apiKey);
                var aiModels = await googleAI.Models.ListAsync();
                var aiModelNames = await aiModels.Select(c => c.Name).ToListAsync();

                var cfg = new GenerateContentConfig
                {
                    ResponseMimeType = "application/json", // Markdown (```json) ve ekstra metinleri API seviyesinde engeller
                    Temperature = 0.1f                     // Halüsinasyon ve saçmalamayı önlemek için düşük sıcaklık
                };

                var errMessage = new StringBuilder();

                foreach (var modelName in data.aiModelNames)
                {
                    try
                    {
                        var parts = new List<Part>();

                        if (blImageUsing)
                        {
                            parts = new List<Part>
                                {
                                    Part.FromBytes(data.imageBytes, data.imageMimeType),
                                    Part.FromText(prompt)
                                };
                        }
                        else
                        {
                            parts = new List<Part>
                                {
                                    Part.FromText(prompt)
                                };
                        }

                        var response = await googleAI.Models.GenerateContentAsync
                            (
                            model: modelName,
                            contents: new Content
                            {
                                Parts = parts
                            },
                            config: blAnswerJsonTrigger ? cfg : null
                            );

                        result.Data = response.Text;
                        break;
                    }
                    catch (Exception ex)
                    {
                       errMessage.AppendLine($"Model: {modelName} - Hata: {ex.Message}");
                       continue;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = Enums.StatusEnum.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
