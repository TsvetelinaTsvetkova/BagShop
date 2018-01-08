using System.Collections.Generic;

namespace BagShop.Areas.Admin.Models.Identity
{
    public class ListUserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
