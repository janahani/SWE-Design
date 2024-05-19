using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

public class ClientAuthoritiesFactory : AuthorityModel
{
    private readonly DbContextOptions<AppDbContext> _options;

    public ClientAuthoritiesFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }
    public async Task addClient(ClientModel clientRequest)
    {
        using var _dbContext = new AppDbContext(_options);
        var client = new ClientModel()
        {
            FirstName = clientRequest.FirstName,
            LastName = clientRequest.LastName,
            Age = clientRequest.Age,
            Gender = clientRequest.Gender,
            Email = clientRequest.Email,
            PhoneNumber = clientRequest.PhoneNumber,
            CreatedAt = DateTime.Now
        };
        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync();

    }
    public async Task editClient(ClientModel updatedClient)
    {
        using var _dbContext = new AppDbContext(_options);
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ID == updatedClient.ID);
        if (client != null)
        {
            client.FirstName = updatedClient.FirstName;
            client.LastName = updatedClient.LastName;
            client.Email = updatedClient.Email;
            client.PhoneNumber = updatedClient.PhoneNumber;

            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task<List<ClientModel>> getClients()
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Clients.ToListAsync();
    }
    public async Task deleteClient(int clientId)
    {
        using var _dbContext = new AppDbContext(_options);
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ID == clientId);
        if (client != null)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<ClientModel> getClientById(int clientId)
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Clients.FirstOrDefaultAsync(c => c.ID == clientId);
    }
}