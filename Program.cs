using AIChatProvider.Response;

var IAChatService = new ChatService(
    Providers.ChatGPT, 
    "APIkey"
);
var message = "Hello, ChatGPT!. ¿Que dia es Hoy?";
var response = await IAChatService.GenerateSinglePromptResponseAsync(message, CancellationToken.None);

if (response is ChatResponse responseAI)
{
    var jsonResponse = responseAI.Choices.First().Message.Content;
    Console.WriteLine(jsonResponse);
}
