namespace FlashTalk.Service.DTOs.Messages;

public class MessageCreationDto
{
    public long From { get; set; }
    public long To { get; set; }
    public string Text { get; set; }
}
