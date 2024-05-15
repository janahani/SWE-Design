using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

public class ClientAuthoritiesFactory : AuthorityModel
{
    private readonly AppDbContext _dbContext;

    public ClientAuthoritiesFactory(DbContextOptions<AppDbContext> options)
    {
        _dbContext = AppDbContext.GetInstance(options);
    }
    public async Task addClient(ClientModel clientRequest)
    {
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
        return await _dbContext.Clients.ToListAsync();
    }
    public async Task deleteClient(int clientId)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ID == clientId);
        if (client != null)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }
    }

     public async Task<ClientModel> getClientById(int clientId)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ID == clientId);

        if (client == null)
        {
            return null;
        }

        var clientModel = new ClientModel
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber
        };

        return clientModel;
    }
}