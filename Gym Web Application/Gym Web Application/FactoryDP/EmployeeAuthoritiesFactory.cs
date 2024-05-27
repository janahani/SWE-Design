using System.Security.Cryptography;
using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

public class EmployeeAuthoritiesFactory : AuthorityModel,IGetAuthority<EmployeeModel>
{

    private readonly DbContextOptions<AppDbContext> _options;

    public EmployeeAuthoritiesFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public async Task<List<EmployeeModel>> getAll()
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



    public async Task<bool> ValidateEmployeeLogin(string email, string password)
        {
            using var _dbContext = new AppDbContext(_options);
            var employee = await GetEmployeeByEmail(email);
            bool passwordCorrect = VerifyPassword(password, employee.Password);
            
            if (employee != null && VerifyPassword(password, employee.Password))
            {
                return true;
            }
            return false;
        }

    public async Task AddEmployee(EmployeeModel addEmployeeRequest)
    {
        using var _dbContext = new AppDbContext(_options);
        byte[] salt = GenerateSalt();
        string hashedPassword = HashPassword(addEmployeeRequest.Password, salt);
        addEmployeeRequest.Password = hashedPassword;

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
        byte[] hashBytes = new byte[36]; // 16 bytes for the salt + 20 bytes for the hash
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        return Convert.ToBase64String(hashBytes);
    }
}

private bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(storedPassword);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
        }
        return true;
    }
    




    public async Task<EmployeeModel> FindById(int id)
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Employees.FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task<EmployeeModel> GetEmployeeByEmail(string email)
    {
        
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);

    }

    public async Task EditEmployee(EmployeeModel editEmployeeRequest)
    {
        using var _dbContext = new AppDbContext(_options);
        var employee =  await _dbContext.Employees.FirstOrDefaultAsync(e => e.ID == editEmployeeRequest.ID);
       byte[] salt = GenerateSalt();
        string hashedPassword = HashPassword(editEmployeeRequest.Password, salt);
        editEmployeeRequest.Password = hashedPassword;

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