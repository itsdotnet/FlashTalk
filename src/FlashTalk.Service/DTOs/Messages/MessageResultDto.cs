namespace FlashTalk.Service.DTOs.Messages;

public class MessageResultDto
{
    public long Id { get; set; }
    public long From { get; set; }
    public long To { get; set; }
    public string Text { get; set; }
}