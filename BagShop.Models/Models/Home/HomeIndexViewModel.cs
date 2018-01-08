using BagShop.Models.Bags;
using BagShop.Models.Models.Bags;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagShop.Models.Models.Home
{
   public class HomeIndexViewModel: SearchFormModel
    {
        public IEnumerable<BagListingServiceModel> Bags { get; set; }
    }
}
