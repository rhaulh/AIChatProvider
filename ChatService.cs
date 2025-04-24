using iDeviceLogAnalyzer.AIChatProvider.Exceptions;
using iDeviceLogAnalyzer.AIChatProvider.Request;
using iDeviceLogAnalyzer.AIChatProvider.Response;
using System.Net;
using System.Text;
using System.Text.Json;
using Message = iDeviceLogAnalyzer.AIChatProvider.Request.Message;

namespace iDeviceLogAnalyzer.AIChatProvider
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private readonly IChatProvider _AIChatProvider;
        public ChatService(Providers chatProvider, string chatApiKey)
        {
            _AIChatProvider = ChatProvider.Create(chatProvider, chatApiKey);
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_AIChatProvider.EndPoint!)
            };
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_AIChatProvider.ApiKey}");
        }
        public async Task<IChatResponse?> GenerateSinglePromptResponseAsync(string prompt, CancellationToken cancellationToken)
        {
            if (_AIChatProvider is ChatProvider)
            {
                List<Message> messages = [new("user", prompt)];
                var request = _AIChatProvider.GetIRequest(messages);
                return await GenerateResponseAsync(request, cancellationToken);
            }
            return new NoChatResponse();
        }
        public async Task<IChatResponse?> GenerateMessagesResponseAsync(List<Message> messages, CancellationToken cancellationToken)
        {
            if (_AIChatProvider is ChatProvider)
            {
                var request = _AIChatProvider.GetIRequest(messages);
                return await GenerateResponseAsync(request, cancellationToken);
            }
            return new NoChatResponse();
        }
        private async Task<ChatResponse> GenerateResponseAsync(IRequestAI request, CancellationToken cancellationToken)
        {
            try
            {
                var _jsonOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    WriteIndented = true
                };
                var content = new StringContent(request.Serialize(_jsonOptions), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("chat/completions", content, cancellationToken);
                var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = JsonSerializer.Deserialize<ApiError>(responseJson, _jsonOptions)?.Message ?? "Error Desconocido";
                    throw new ChatException($"API Error: {errorMessage}", response.StatusCode);
                }
                return JsonSerializer.Deserialize<ChatResponse>(responseJson, _jsonOptions)
                    ?? throw new ChatException("No se pudo analizar la respuesta.", response.StatusCode);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("La solicitud fue cancelada.");
                throw;
            }
            catch (Exception ex)
            {
                throw new ChatException("La solicitud falló", HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
