namespace OnlineShop_WebApp.Models
{
    public class UserEditAvatarViewModel
    {
        public string Login { get; set; }

        public string AvatarImagePath { get; set; }

        public IFormFile? UploadNewAvatar { get; set; }
    }
}
