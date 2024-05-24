using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class EmployeeService
{
    private EmployeeAuthoritiesFactory _employeeAuthoritiesFactory;

    public EmployeeService(EmployeeAuthoritiesFactory employeeAuthoritiesFactory)
    {
        _employeeAuthoritiesFactory = employeeAuthoritiesFactory;
    }

    public async Task<List<EmployeeModel>> GetAllEmployees()
    {
        return await _employeeAuthoritiesFactory.GetAllEmployees();
    }

    public async Task<List<EmployeeModel>> GetAllCoaches()
    {
        return await _employeeAuthoritiesFactory.GetAllCoaches();
    }

    public async Task<EmployeeModel> GetEmployeeByEmail(string email)
    {
        return await _employeeAuthoritiesFactory.GetEmployeeByEmail(email);
    }

    public async Task AddEmployee(EmployeeModel addEmployeeRequest)
    {
        await _employeeAuthoritiesFactory.AddEmployee(addEmployeeRequest);
    }

    public async Task<EmployeeModel> FindById(int id)
    {
        return await _employeeAuthoritiesFactory.FindById(id);
    }

    public async Task EditEmployee(EmployeeModel editEmployeeRequest)
    {
        await _employeeAuthoritiesFactory.EditEmployee(editEmployeeRequest);
    }

    public async Task DeleteEmployee(int id)
    {
        await _employeeAuthoritiesFactory.DeleteEmployee(id);
    }

     public async Task<bool> ValidateEmployeeLogin(string email, string password)
        {
            bool login = await _employeeAuthoritiesFactory.ValidateEmployeeLogin( email, password);
            return login;
        }

 
}
