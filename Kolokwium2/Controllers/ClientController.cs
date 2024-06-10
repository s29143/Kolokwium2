using Kolokwium2.DTOs;
using Kolokwium2.Exceptions;
using Kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [Route("/api/[controller]/{idClient}")]
    [HttpGet]
    public async Task<IActionResult> GetClient([FromRoute] int idClient)
    {
        try
        {
            var client = await _clientService.GetById(idClient);
            return Ok(client);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    [Route("/api/[controller]/{idClient}/subscription/{idSubscription}")]
    [HttpPost]
    public async Task<IActionResult> PayForSubscription([FromRoute] int idClient,
        [FromRoute] int idSubscription, [FromBody] PaymentDTO dto)
    {
        try
        {
            var paymentSuccessful = await _clientService.PayForSubscription(idClient, idSubscription, dto);
            return Ok(paymentSuccessful);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }
}