using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

public class Discount
{
    [Key]
    public int IdDiscount { get; set; }

    public int Value { get; set; }
    public int IdSubscription { get; set; }
    [Required]
    public DateOnly DateFrom { get; set; }
    [Required]
    public DateOnly DateTo { get; set; }
    [ForeignKey(nameof(IdSubscription))] public virtual Subscription IdSubscriptionNavigation { get; set; } = null!;
}