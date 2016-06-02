using System.ComponentModel.DataAnnotations;

namespace Msmq.OnlineShop.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "ID")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(20)]
        [MinLength(2)]
        [Display(Name = "Product name")]
        public string ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
