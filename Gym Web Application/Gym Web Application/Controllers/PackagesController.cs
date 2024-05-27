using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym_Web_Application.Controllers
{
    public class PackagesController : Controller
    {
        private readonly ILogger<PackagesController> _logger;
        private readonly PackageService _packageService;
        private readonly ClientService _clientService;
        private int? _jobTitleId;

        public PackagesController(ILogger<PackagesController> logger, PackageService packageService, ClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
            _packageService = packageService;
        }

        private void SetJobTitleId()
        {
            if (HttpContext.Session.GetString("EmployeeJobTitleID") != null)
            {
                _jobTitleId = Convert.ToInt32(HttpContext.Session.GetString("EmployeeJobTitleID"));
            }
            else
            {
                _jobTitleId = null;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewPackages()
        {
            if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }

            SetJobTitleId(); // Set jobTitleId value
            var packages = await _packageService.GetAllPackages();
            return View(packages);
        }

        [HttpGet]
        public async Task<IActionResult> ViewActivatedPackages(int clientID)
        {
            if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }

            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewPackages");
            }

            var client = await _clientService.GetClientById(clientID);
            _logger.LogInformation("output this rn {client}", client);
            var packages = await _packageService.GetActivatedPackages();
            ViewBag.Client = client;
            ViewBag.Packages = packages;
            return View();
        }

        [HttpGet]
        public IActionResult AddPackages()
        {
            if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }

            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewPackages");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPackages(PackageModel addPackageRequest)
        {
            if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }

            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewPackages");
            }

            await _packageService.AddPackages(addPackageRequest);
            return RedirectToAction("ViewPackages");
        }

        [HttpPost]
        public async Task<IActionResult> Activation(string activationStatus, int packageId)
        {
            if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }

            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewPackages");
            }

            if (activationStatus == "Activated")
            {
                await _packageService.DeactivatePackage(packageId);
            }
            else if (activationStatus == "Not Activated")
            {
                await _packageService.ActivatePackage(packageId);
            }

            return RedirectToAction("ViewPackages");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}