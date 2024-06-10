using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

public class Sale
{
    [Key]
    public int IdSale { get; set; }
    public int IdClient { get; set; }
    public int IdSubscription { get; set; }
    [Required]
    public DateOnly CreatedAt { get; set; }
    [ForeignKey(nameof(IdClient))] public virtual Client IdClientNavigation { get; set; } = null!;
    [ForeignKey(nameof(IdSubscription))] public virtual Subscription IdSubscriptionNavigation { get; set; } = null!;

}