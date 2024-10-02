using System.ComponentModel.DataAnnotations;

namespace OrderServiceApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required.")]
        public int? Quantity  { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; } // Сделаем это поле nullable
    }
}
