using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BagShop.Models.Bags
{
    public class AddBagViewModel
    {
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Title { get; set; }

        public string Color { get; set; }

        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0,100)]
        public int Quantity { get; set; }
    }
}
