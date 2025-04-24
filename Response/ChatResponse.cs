namespace iDeviceLogAnalyzer.AIChatProvider.Response
{
    public interface IChatResponse;
    public record ChatResponse(string Id, List<Choice> Choices, Usage Usage): IChatResponse;
    public record NoChatResponse:IChatResponse;
}
