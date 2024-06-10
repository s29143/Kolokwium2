using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

public class Payment
{
    [Key]
    public int IdPayment { get; set; }

    public DateOnly Date { get; set; }
    public int IdClient { get; set; }
    public int IdSubscription { get; set; }
    [ForeignKey(nameof(IdClient))] public virtual Client IdClientNavigation { get; set; } = null!;
    [ForeignKey(nameof(IdSubscription))] public virtual Subscription IdSubscriptionNavigation { get; set; } = null!;
}