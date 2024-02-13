using OnlineShop_WebApp.Models;

namespace OnlineShop_WebApp.Areas.Admin.Models
{
    public class EditRightsViewModel
    {
        public string UserName { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public List<RoleViewModel> AllRoles { get; set; }
    }
}
