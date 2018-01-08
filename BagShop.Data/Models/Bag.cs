using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BagShop.Data.Models
{
   public class Bag
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Title { get; set; }

        public string Color { get; set; }

        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        [Range(0,100)]
        [Required]
        public int Quantity { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
