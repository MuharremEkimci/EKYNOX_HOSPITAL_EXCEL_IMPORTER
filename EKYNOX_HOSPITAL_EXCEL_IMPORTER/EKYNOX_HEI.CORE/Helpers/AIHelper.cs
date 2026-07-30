using Microsoft.Extensions.AI;
using Azure.AI.OpenAI;
using EKYNOX_HEI.CORE.Models.AISetting;
using Google.GenAI;
using Google.GenAI.Types;
using System.ClientModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using OpenAI;
using OpenAI.Chat;

namespace EKYNOX_HEI.CORE.Helpers
{
    public class AiRequestData
    {
        public List<AISettingListModel>? AiModelNames { get; set; }
        public byte[] ImageBytes { get; set; }
        public string? ImageMimeType { get; set; }
        public string? ApiKey { get; set; }
        public string? Prompt { get; set; }
        public string? Endpoint { get; set; }
    }

    public class AIHelper
    {
        [Display(Name = "Method", Description = "Gemini AI Methodu")]
        public async Task<ReturnData<string>> GeminiAIQuestion(AiRequestData data, bool blImageUsing = false, bool blAnswerJsonTrigger = false)
        {
            var result = new ReturnData<string>();

            try
            {
                var googleAI = new Client(apiKey: data.ApiKey);
                var cfg = new GenerateContentConfig
                {
                    ResponseMimeType = "application/json", // Markdown (```json) ve ekstra metinleri API seviyesinde engeller
                    Temperature = 0.1f                     // Halüsinasyon ve saçmalamayı önlemek için düşük sıcaklık
                };

                var errMessage = new StringBuilder();

                foreach (var modelName in data.AiModelNames)
                {
                    try
                    {
                        var parts = new List<Part>();

                        if (blImageUsing)
                        {
                            parts = new List<Part>
                                {
                                    Part.FromBytes(data.ImageBytes, data.ImageMimeType),
                                    Part.FromText(data.Prompt)
                                };
                        }
                        else
                        {
                            parts = new List<Part>
                                {
                                    Part.FromText(data.Prompt)
                                };
                        }

                        var response = await googleAI.Models.GenerateContentAsync
                            (
                            model: modelName.AiModelName,
                            contents: new Content
                            {
                                Parts = parts
                            },
                            config: blAnswerJsonTrigger ? cfg : null
                            );

                        result.Data = response.Text;
                        result.Status = Enums.StatusEnum.Success;
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

        [Display(Name = "Method", Description = "Azure AI Methodu")]
        public async Task<ReturnData<string>> AzureAIQuestion(AiRequestData data, bool blImageUsing = false, bool blAnswerJsonTrigger = false)
        {
            var result = new ReturnData<string>();

            try
            {
                var azureAi = new AzureOpenAIClient(new Uri(data.Endpoint),new System.ClientModel.ApiKeyCredential(data.ApiKey));
                var options = new ChatOptions
                {
                    Temperature = 0.1f
                };

                var errMessage = new StringBuilder();

                foreach (var modelName in data.AiModelNames)
                {
                    try
                    {
                        IChatClient aiClient = azureAi.GetChatClient(modelName.AiModelName).AsIChatClient();
                        var chatMessages = new List<Microsoft.Extensions.AI.ChatMessage>();

                        if (blImageUsing)
                        {
                            var imageContent = new DataContent(
                                                        BinaryData.FromBytes(data.ImageBytes),
                                                        data.ImageMimeType
                                                   );

                            if (blAnswerJsonTrigger)
                            {
                                chatMessages = new List<Microsoft.Extensions.AI.ChatMessage>
                                               {
                                                   new(ChatRole.System,
                                                       "Sen bir OCR asistanısın. Sadece geçerli JSON döndürürsün."),

                                                   new(ChatRole.User,
                                                   [
                                                       new TextContent(data.Prompt),
                                                       imageContent
                                                   ])
                                               };
                            }
                            else
                            {
                                chatMessages = new List<Microsoft.Extensions.AI.ChatMessage>
                                                {
                                                    new(ChatRole.User,
                                                    [
                                                        new TextContent(data.Prompt),
                                                        imageContent
                                                    ])
                                                };
                            }
                        }
                        else
                        {
                            chatMessages = new List<Microsoft.Extensions.AI.ChatMessage>
                                           {
                                               new(ChatRole.User, data.Prompt)
                                           };
                        }

                        if (blAnswerJsonTrigger)
                        {
                            options.ResponseFormat = Microsoft.Extensions.AI.ChatResponseFormat.Json;
                        }


                        ChatResponse response = await aiClient.GetResponseAsync(chatMessages, options);
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

        [Display(Name = "Method", Description = "Groq AI Methodu")]
        public async Task<ReturnData<string>> GroqAIQuestion(AiRequestData data, bool blImageUsing = false, bool blAnswerJsonTrigger = false)
        {
            var result = new ReturnData<string>();

            try
            {
                var azureAi = new OpenAIClient(new ApiKeyCredential(data.ApiKey), new OpenAIClientOptions { Endpoint = new Uri(data.Endpoint) });
                var cfg = new ChatCompletionOptions
                {
                    Temperature = 0,
                    MaxOutputTokenCount = 4096
                };

                var errMessage = new StringBuilder();

                foreach (var modelName in data.AiModelNames)
                {
                    try
                    {
                        ChatClient chatClient = azureAi.GetChatClient(modelName.AiModelName);
                        var textPart = ChatMessageContentPart.CreateTextPart(data.Prompt);
                        var chatMessages = new List<OpenAI.Chat.ChatMessage>();


                        if (blImageUsing)
                        {
                            var imagePart = ChatMessageContentPart.CreateImagePart(BinaryData.FromBytes(data.ImageBytes), data.ImageMimeType, ChatImageDetailLevel.High);

                            if (blAnswerJsonTrigger)
                            {
                                chatMessages = new List<OpenAI.Chat.ChatMessage>
                                {
                                    new SystemChatMessage("""
                                                            You are an OCR extraction engine.
                                                            
                                                            RULES:
                                                            - Output ONLY valid JSON.
                                                            - Do NOT output <think>.
                                                            - Do NOT output explanations.
                                                            - Do NOT use markdown.
                                                            - Do NOT add text before or after JSON.
                                                            
                                                            Return exactly this schema:
                                                            
                                                            {
                                                              "participants": [
                                                                {
                                                                  "class_no": 1,
                                                                  "name": "string",
                                                                  "surname": "string"
                                                                }
                                                              ]
                                                            }
                                                            """),
                                    new UserChatMessage(textPart, imagePart)
                                };
                            }
                            else
                            {
                                chatMessages = new List<OpenAI.Chat.ChatMessage>
                                {
                                    new UserChatMessage(textPart, imagePart)
                                };
                            }
                        }
                        else
                        {
                            chatMessages = new List<OpenAI.Chat.ChatMessage>
                                {
                                    new UserChatMessage(textPart)
                                };
                        }

                        ChatCompletion completion = await chatClient.CompleteChatAsync(chatMessages, options: blAnswerJsonTrigger ? cfg : null);
                        string rawContent = completion.Content[0].Text;
                        result.Data = rawContent;
                        break;
                    }
                    catch (Exception ex)
                    {
                        errMessage.AppendLine($"Model: {modelName.AiModelName} - Hata: {ex.Message}");
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
