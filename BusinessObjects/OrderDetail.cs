using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects
{
    public class OrderDetail
    {
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("FlowerBouquet")]
        public int FlowerBouquetID { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Discount { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
        [JsonIgnore]
        public virtual FlowerBouquet FlowerBouquet { get; set; }
    }
}
