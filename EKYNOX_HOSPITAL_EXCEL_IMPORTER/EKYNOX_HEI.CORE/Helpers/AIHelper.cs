using Google.GenAI;
using Google.GenAI.Types;
using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace EKYNOX_HEI.CORE.Helpers
{
    public class AIHelper
    {
        private readonly string apiKey;

        public AIHelper(string _apiKey)
        {
            apiKey = _apiKey;
        }

        public async Task<ReturnData<string>> AIImageQuestion(string prompt, byte[] imageBytes, string imageMimeType)
        {
            var result = new ReturnData<string>();

            try
            {
                var googleAI = new Client(apiKey: apiKey);
                var aiModels = await googleAI.Models.ListAsync();
                var aiModelNames = await aiModels.Select(c => c.Name).ToListAsync();
                
                var errMessage = new StringBuilder();

                foreach (var modelName in aiModelNames)
                {
                    try
                    {
                        var response = await googleAI.Models.GenerateContentAsync
                            (
                            model: modelName, 
                            contents: new Content
                            {
                                Parts = new List<Part> 
                                {
                                    Part.FromBytes(imageBytes, imageMimeType),
                                    Part.FromText(prompt)
                                }
                            });

                        result.Data = response.Text;
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
