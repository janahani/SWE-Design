using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class ClientsController : Controller
{
    private readonly ILogger<ClientsController> _logger;
    private readonly ClientService _clientService;

    public ClientsController(ILogger<ClientsController> logger,ClientService clientService)
    {
        _logger = logger;
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> ViewClients()
    {
        var clients = await _clientService.GetAllClients();
        return View(clients);
    }

    [HttpGet]
    public IActionResult AddClients()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddClients(ClientModel clientRequest)
    {
        await _clientService.AddClientAsync(clientRequest);
        return RedirectToAction ("AddClients");
    }

    [HttpGet]
    public async Task<IActionResult> EditClients(int id)
    {
        var client = await _clientService.GetClientById(id);
        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> EditClients(ClientModel updatedClient)
    {
        await _clientService.EditClient(updatedClient);
        return RedirectToAction("ViewClients");
    }

    [HttpPost]
    public async Task<ActionResult> DeleteClients(int id)
    {
        await _clientService.DeleteClient(id);
        return RedirectToAction("ViewClients");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}