using Kolokwium2.Contexts;
using Kolokwium2.DTOs;
using Kolokwium2.Exceptions;
using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Services;

public interface IClientService
{
    Task<ClientDTO> GetById(int idClient);
}

public class ClientService : IClientService
{
    private readonly ApbdContext _context;

    public ClientService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<ClientDTO> GetById(int idClient)
    {
        var client = await _context.Clients.Where(c => c.IdClient == idClient)
            .Include(c => c.Payments)
            .Select(c => new ClientDTO()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Subscriptions = new List<SubscriptionDTO>()
            }).FirstOrDefaultAsync();
        if (client is null)
        {
            throw new NotFoundException();
        }

        return client;
    }
}