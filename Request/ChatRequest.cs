using System.Text.Json;

namespace iDeviceLogAnalyzer.AIChatProvider.Request
{
    public interface IRequestAI
    {
        public string Serialize(JsonSerializerOptions? options = null) =>
            this switch
            {
                DeepSeekRequest deepSeekRequest => JsonSerializer.Serialize(deepSeekRequest, options),
                ChatGPTRequest chatGptRequest => JsonSerializer.Serialize(chatGptRequest, options),
                _ => throw new InvalidOperationException("Tipo de solicitud no soportado para serialización.")
            };
    }

    public record DeepSeekRequest(
        string Model,
        List<Message> Messages,
        double Temperature,
        int MaxTokens
    ) : IRequestAI;

    public record ChatGPTRequest(
        string Model,
        List<Message> Messages,
        double Temperature,
        int MaxCompletionTokens
    ) : IRequestAI;
    public record NoIRequestChat : IRequestAI;
}
