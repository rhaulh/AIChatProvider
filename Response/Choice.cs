using Message = iDeviceLogAnalyzer.AIChatProvider.Request.Message;

namespace iDeviceLogAnalyzer.AIChatProvider.Response
{
    public record Choice(Message Message, string FinishReason);
}
