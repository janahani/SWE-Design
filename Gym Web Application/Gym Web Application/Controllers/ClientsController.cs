using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class ClientsController : Controller
{
    private readonly ILogger<ClientsController> _logger;
    private readonly ClientService _clientService;
    private readonly MembershipService _membershipService;

    public ClientsController(ILogger<ClientsController> logger, ClientService clientService, MembershipService membershipService)
    {
        _logger = logger;
        _clientService = clientService;
        _membershipService = membershipService;
    }

    [HttpGet]
    public async Task<IActionResult> ViewClients()
    {
        if (HttpContext.Session.GetString("EmployeeEmail") == null)
        {
            return RedirectToAction("Login");
        }
        var clients = await _clientService.GetAllClients();
        List<bool> hasActiveMembership = new List<bool>();
        foreach (var client in clients)
        {
            if (await _membershipService.hasActiveMembership(client.ID))
            {
                hasActiveMembership.Add(true);
            }
            else
            {
                hasActiveMembership.Add(false);

            }
        }
        ViewBag.clients = clients;
        ViewBag.hasActiveMembership = hasActiveMembership;
        return View();
    }

    [HttpGet]
    public IActionResult AddClients()
    {
        if (HttpContext.Session.GetString("EmployeeEmail") == null)
        {
            return RedirectToAction("Login");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddClients(ClientModel clientRequest)
    {
        if (HttpContext.Session.GetString("EmployeeEmail") == null)
        {
            return RedirectToAction("Login");
        }
        await _clientService.AddClientAsync(clientRequest);
        return RedirectToAction("AddClients");
    }

    [HttpGet]
    public async Task<IActionResult> EditClients(int id)
    {
        if (HttpContext.Session.GetString("EmployeeEmail") == null)
        {
            return RedirectToAction("Login");
        }
        var client = await _clientService.GetClientById(id);
        return View(client);
    }

    [HttpPost]
    public async Task<IActionResult> EditClients(ClientModel updatedClient)
    {
        if (HttpContext.Session.GetString("EmployeeEmail") == null)
        {
            return RedirectToAction("Login");
        }
        await _clientService.EditClient(updatedClient);
        return RedirectToAction("ViewClients");
    }

    [HttpPost]
    public async Task<ActionResult> DeleteClients(int id)
    {
        if (HttpContext.Session.GetString("EmployeeEmail") == null)
        {
            return RedirectToAction("Login");
        }
        await _clientService.DeleteClient(id);
        return RedirectToAction("ViewClients");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}