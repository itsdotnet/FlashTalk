using FlashTalk.Domain.Commons;

namespace FlashTalk.Domain.Entities;

public class User : Auditable
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
