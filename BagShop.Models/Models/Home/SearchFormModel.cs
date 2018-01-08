using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BagShop.Models.Models.Home
{
   public class SearchFormModel
    {
        public string SearchText { get; set; }

        [Display(Name = "Search In Bags")]
        public bool SearchInBags { get; set; } = true;
    }
}
