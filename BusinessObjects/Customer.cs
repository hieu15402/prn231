using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace BusinessObjects
{
    public class Customer : IdentityUser
    {
        public string? CustomerName { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? Birthday { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
