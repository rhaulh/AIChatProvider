using iDeviceLogAnalyzer.AIChatProvider.Request;
using Message = iDeviceLogAnalyzer.AIChatProvider.Request.Message;

namespace iDeviceLogAnalyzer.AIChatProvider
{
    public class ChatProvider : IChatProvider
    {
        public Providers? Name { get; set; }
        public string Model { get; set; }
        public string EndPoint { get; set; }
        public string? ApiKey { get; set; }
        private ChatProvider(Providers chatProvider, string endPoint, string model, string apiKey)
        {
            EndPoint = endPoint;
            Model = model;
            Name = chatProvider;
            ApiKey = apiKey;
        }
        public static IChatProvider Create(Providers chatProvider,string apiKey)
        {
            return chatProvider switch
            {
                Providers.ChatGPT => new ChatProvider(chatProvider, "https://api.openai.com/v1/", "gpt-4o-mini", apiKey),
                Providers.DeepSeek => new ChatProvider(chatProvider, "https://api.deepseek.com/v1/", "deepseek-chat", apiKey),
                _ => new NoChatProvider()
            };
        }
        public IRequestAI GetIRequest(List<Message> messages)
        {
            return Model switch
            {
                "gpt-4o-mini" => new ChatGPTRequest(
                    Model: Model,
                    Messages: messages,
                    Temperature: 1.0,
                    MaxCompletionTokens: 1000),
                "deepseek-chat" => new DeepSeekRequest(
                    Model: Model,
                    Messages: messages,
                    Temperature: 1.0,
                    MaxTokens: 1000),
                _ => new NoIRequestChat()
            };
        }
    }
}
