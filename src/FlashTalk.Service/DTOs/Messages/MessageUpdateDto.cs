namespace FlashTalk.Service.DTOs.Messages;

public class MessageUpdateDto
{
    public long Id { get; set; }
    public long From { get; set; }
    public long To { get; set; }
    public string Message { get; set; }
}
