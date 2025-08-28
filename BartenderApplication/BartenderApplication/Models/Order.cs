using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderApplication.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public int CocktailId { get; set; }

        [ForeignKey("CocktailId")]
        public Cocktail Cocktail { get; set; }

        [Range(1, 20)]
        public int Quantity { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
