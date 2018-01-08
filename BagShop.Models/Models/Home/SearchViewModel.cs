using BagShop.Models.Bags;
using BagShop.Models.Models.Bags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BagShop.Models.Models.Home
{
   public class SearchViewModel
    {

        public IEnumerable<BagListingServiceModel> Bags { get; set; }
  
        public string SearchText { get; set; }
    }
}
