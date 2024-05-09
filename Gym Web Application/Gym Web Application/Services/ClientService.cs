using Gym_Web_Application.Models;

public class ClientService
{
    private readonly List<ClientModel> _clients;

    public ClientService()
    {
        _clients = new List<ClientModel>();
    }

    public void AddClient(ClientModel client)
    {
        _clients.Add(client);
    }

    public List<ClientModelDto> GetAllClients()
    {
        return _clients.Select(c => new ClientModelDto
        {
            ID = c.ID,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Age = c.Age,
            Gender = c.Gender,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber
        }).ToList();
    }

    public void EditClient(int clientId, ClientModel updatedClient)
    {
        var client = _clients.FirstOrDefault(c => c.ID == clientId);
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
        }
    }

    public void DeleteClient(int clientId)
    {
        var client = _clients.FirstOrDefault(c => c.ID == clientId);
        if (client != null)
        {
            _clients.Remove(client);
        }
    }

    public ClientModel FindById(int clientId)
    {
        return _clients.FirstOrDefault(c => c.ID == clientId);
    }

    public ClientModel FindByEmail(string email)
    {
        return _clients.FirstOrDefault(c => c.Email == email);
    }
}

