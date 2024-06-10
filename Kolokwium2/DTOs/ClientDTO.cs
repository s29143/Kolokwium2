using Kolokwium2.Models;

namespace Kolokwium2.DTOs;

public class ClientDTO
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public ICollection<SubscriptionDTO> Subscriptions { get; set; }
}