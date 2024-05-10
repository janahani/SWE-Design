using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class ClientService
{
    private readonly AppDbContext _dbContext;

    public ClientService(DbContextOptions<AppDbContext> options)
    {
        _dbContext = AppDbContext.GetInstance(options);
    }

    public void AddClient(ClientModel client)
    {
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
    }

    public List<ClientModel> GetAllClients()
    {
        return _dbContext.Clients
            .Select(c => new ClientModel
            {
                ID = c.ID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Age = c.Age,
                Gender = c.Gender,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            })
            .ToList();
    }

    public void EditClient(int clientId, ClientModel updatedClient)
    {
        var client = _dbContext.Clients.FirstOrDefault(c => c.ID == clientId);
        if (client != null)
        {
            client.FirstName = updatedClient.FirstName;
            client.LastName = updatedClient.LastName;
            client.Age = updatedClient.Age;
            client.Gender = updatedClient.Gender;
            client.Email = updatedClient.Email;
            client.Password = updatedClient.Password;
            client.PhoneNumber = updatedClient.PhoneNumber;
            client.CreatedAt = updatedClient.CreatedAt;

            _dbContext.SaveChanges();
        }
    }

    public void DeleteClient(int clientId)
    {
        var client = _dbContext.Clients.FirstOrDefault(c => c.ID == clientId);
        if (client != null)
        {
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }
    }

    public ClientModel FindById(int clientId)
    {
        return _dbContext.Clients.FirstOrDefault(c => c.ID == clientId);
    }

    public ClientModel FindByEmail(string email)
    {
        return _dbContext.Clients.FirstOrDefault(c => c.Email == email);
    }
}
