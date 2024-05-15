using Gym_Web_Application.Models;

public class ClientService
{
    private ClientAuthoritiesFactory _clientAuthoritiesFactory;

    public ClientService(ClientAuthoritiesFactory clientAuthoritiesFactory)
    {
        _clientAuthoritiesFactory= clientAuthoritiesFactory;
    }

    public async Task AddClientAsync(ClientModel clientRequest)
    {
        await _clientAuthoritiesFactory.addClient(clientRequest);
    }

    public async Task<List<ClientModel>> GetAllClients()
    {
        return await _clientAuthoritiesFactory.getClients();
    }

    public async Task EditClient(ClientModel updatedClient)
    {
        await _clientAuthoritiesFactory.editClient(updatedClient);
    }

    public async Task DeleteClient(int clientId)
    {
        await _clientAuthoritiesFactory.deleteClient(clientId);
    }

    public async Task<ClientModel> GetClientById(int clientId)
    {
        return await _clientAuthoritiesFactory.getClientById(clientId);
    }

}
