using System.ComponentModel.DataAnnotations;

namespace Kolokwium2.Models;

public class Subscription
{
    [Key]
    public int IdSubscription { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    public int RenewalPeriod { get; set; }
    [Required]
    public DateOnly EndTime { get; set; }
    public double Price { get; set; }
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

}