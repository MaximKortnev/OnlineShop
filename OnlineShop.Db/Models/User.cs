using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public string NickName { get; set; } = "User";
        public string? AvatarImagePath { get; set; } = "~\\wwwroot\\images\\users";
    }
}
