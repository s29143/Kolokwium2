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
}