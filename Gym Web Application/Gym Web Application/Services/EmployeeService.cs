using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class EmployeeService
{
    private readonly AppDbContext _dbContext;

    public EmployeeService(DbContextOptions<AppDbContext> options)
    {
        _dbContext = AppDbContext.GetInstance(options);
    }

    public async Task<List<EmployeeModel>> GetAllEmployees()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public async Task AddEmployees(EmployeeModel addEmployeeRequest)
    {
        var employee = new EmployeeModel()
        {
            Name = addEmployeeRequest.Name,
            Email = addEmployeeRequest.Email,
            PhoneNumber = addEmployeeRequest.PhoneNumber,
            Salary = addEmployeeRequest.Salary,
            Address = addEmployeeRequest.Address,
            JobTitleID = addEmployeeRequest.JobTitleID,
            Password = addEmployeeRequest.Name
        };

        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
    }


    public EmployeeModel FindById(int id)
    {
        return _dbContext.Employees.FirstOrDefault(p => p.ID == id);
    }
}
