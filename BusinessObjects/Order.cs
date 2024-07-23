using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public string Freight { get; set; }
        [ForeignKey("CustomerId")]
        public string CustomerID { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
