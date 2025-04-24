using iDeviceLogAnalyzer.AIChatProvider.Request;
using Message = iDeviceLogAnalyzer.AIChatProvider.Request.Message;

namespace iDeviceLogAnalyzer.AIChatProvider
{
    public record NoChatProvider : IChatProvider
    {
        public Providers? Name => null;
        public string? EndPoint => null;
        public string? Model => null;
        public string? ApiKey => null;

        public IRequestAI GetIRequest(List<Message> messages)
        {
            return new NoIRequestChat();
        }
    }
}
