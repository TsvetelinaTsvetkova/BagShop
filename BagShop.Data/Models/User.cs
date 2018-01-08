using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BagShop.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public List<Bag> Bags { get; set; } = new List<Bag>();

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
