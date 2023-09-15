using FlashTalk.Domain.Commons;

namespace FlashTalk.Domain.Entities;

public class Massage : Auditable
{
    public long From { get; set; }
    public long To { get; set; }
    public string Text { get; set; }
}
