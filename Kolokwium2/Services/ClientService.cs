using Kolokwium2.Contexts;
using Kolokwium2.DTOs;
using Kolokwium2.Exceptions;
using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Services;

public interface IClientService
{
    Task<ClientDTO> GetById(int idClient);
    Task<int> PayForSubscription(int idClient, int idSubscription, PaymentDTO paymentDto);
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
            .Include(c => c.Sales)
            .Select(c => new ClientDTO()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Subscriptions = c.Sales.Select(sale => new SubscriptionDTO()
                {
                    IdSubscription = sale.IdSubscriptionNavigation.IdSubscription,
                    Name = sale.IdSubscriptionNavigation.Name,
                    TotalPaidAmount = sale.IdSubscriptionNavigation.Price * _context.Payments.Count(p => p.IdSubscription == sale.IdSubscription)
                }).ToList()
            }).FirstOrDefaultAsync();
        if (client is null)
        {
            throw new NotFoundException();
        }

        return client;
    }

    public async Task<int> PayForSubscription(int idClient, int idSubscription, PaymentDTO paymentDto)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == idClient);
        if (client is null)
        {
            throw new NotFoundException("Client doesn't exists given id=" + idClient);
        }
        var subscription = await _context.Subscriptions.FirstOrDefaultAsync(c => c.IdSubscription == idSubscription);
        if (subscription is null)
        {
            throw new NotFoundException("Subscription doesn't exists given id=" + idSubscription);
        }

        if (subscription.EndTime < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ConflictException("Subscription is not active");
        }

        var payment = await _context.Payments.FirstOrDefaultAsync(p => p.IdClient == idClient &&
                                                                 p.IdSubscription == idSubscription &&
                                                                 p.Date == paymentDto.Date);
        if (payment is not null)
        {
            throw new ConflictException("Payment already exists");
        }

        var discount = await _context.Discounts.Where(d => d.IdSubscription == idSubscription)
            .OrderByDescending(d => d.Value).FirstOrDefaultAsync();

        double price = 1;
        if (discount is not null)
        {
            price = (double)discount.Value / 100;
        }

        if (paymentDto.Price < subscription.Price - subscription.Price * price)
        {
            throw new ConflictException("Too few money");
        }

        payment = new Payment()
        {
            IdClient = idClient,
            IdSubscription = idSubscription,
            Date = DateOnly.FromDateTime(DateTime.Now)
        };
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();

        return payment.IdPayment;
    }
}