using System.Security.Cryptography;
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

    public async Task<List<EmployeeModel>> GetAllCoaches()
    {
        return await _dbContext.Employees
            .Where(e => e.JobTitleID == 4)
            .ToListAsync();
    }


    public async Task AddEmployee(EmployeeModel addEmployeeRequest)
    {
        byte[] salt = GenerateSalt();

        string hashedPassword = HashPassword(addEmployeeRequest.Password, salt);

        var employee = new EmployeeModel()
        {
            Name = addEmployeeRequest.Name,
            Email = addEmployeeRequest.Email,
            PhoneNumber = addEmployeeRequest.PhoneNumber,
            Salary = addEmployeeRequest.Salary,
            Address = addEmployeeRequest.Address,
            JobTitleID = addEmployeeRequest.JobTitleID,
            Password = hashedPassword,
        };

        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private string HashPassword(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20); 
            return Convert.ToBase64String(hash);
        }
    }

    public async Task<EmployeeModel> FindById(int id)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(p => p.ID == id);
    }
}
