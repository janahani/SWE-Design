using System.Security.Cryptography;
using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;



public class EmployeeAuthoritiesFactory : AuthorityModel{

    private readonly DbContextOptions<AppDbContext> _options;

    public EmployeeAuthoritiesFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public async Task<List<EmployeeModel>> GetAllEmployees()
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Employees.ToListAsync();
    }



    public async Task<List<EmployeeModel>> GetAllCoaches()
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Employees
            .Where(e => e.JobTitleID == 4)
            .ToListAsync();
    }

    public async Task AddEmployee(EmployeeModel addEmployeeRequest)
    {
        using var _dbContext = new AppDbContext(_options);
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
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Employees.FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task EditEmployee(EmployeeModel editEmployeeRequest)
    {
        using var _dbContext = new AppDbContext(_options);
        var employee =  await _dbContext.Employees.FirstOrDefaultAsync(e => e.ID == editEmployeeRequest.ID);
        byte[] salt = GenerateSalt();
        string hashedPassword = HashPassword(editEmployeeRequest.Password, salt);

        if(employee!=null)
        {
            employee.Name = editEmployeeRequest.Name;
            employee.Email = editEmployeeRequest.Email;
            employee.PhoneNumber = editEmployeeRequest.PhoneNumber;
            employee.Salary = editEmployeeRequest.Salary;
            employee.Address = editEmployeeRequest.Address;
            employee.JobTitleID = editEmployeeRequest.JobTitleID;
            employee.Password = hashedPassword;
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteEmployee(int id)
    {
        using var _dbContext = new AppDbContext(_options);
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.ID == id);

        if(employee!=null)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
        
    }

}