using AutoMapper.QueryableExtensions;
using BagShop.Data;
using BagShop.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly BagShopDbContext db;

        public AdminUserService(BagShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserListingServiceModel> All()
            => this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>();
    }
}
