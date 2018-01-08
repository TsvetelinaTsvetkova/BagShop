using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BagShop.Models.Models.Bags
{
   public class EditBagModel
    {
        [Required]
        public int BagId { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }
    }
}
