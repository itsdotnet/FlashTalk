using FlashTalk.Domain.Commons;

namespace FlashTalk.Domain.Entities;

public class Message : Auditable
{
    public long From { get; set; }
    public long To { get; set; }
    public string Text { get; set; }
}
