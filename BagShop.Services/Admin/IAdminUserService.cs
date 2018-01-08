using BagShop.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Services.Admin
{
    public interface IAdminUserService
    {
        IEnumerable<AdminUserListingServiceModel>All();
    }
}
