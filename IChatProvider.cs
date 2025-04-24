using iDeviceLogAnalyzer.AIChatProvider.Request;
using Message = iDeviceLogAnalyzer.AIChatProvider.Request.Message;

namespace iDeviceLogAnalyzer.AIChatProvider
{
    public interface IChatProvider
    {
        Providers? Name { get; }
        string? EndPoint { get; }
        string? Model { get; }
        string? ApiKey { get; }
        IRequestAI GetIRequest(List<Message> messages);
    }
}
