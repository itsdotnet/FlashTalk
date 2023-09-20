namespace FlashTalk.Web.Interfaces;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}