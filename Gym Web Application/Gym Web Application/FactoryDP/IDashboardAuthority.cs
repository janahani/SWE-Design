namespace Gym_Web_Application.Models;

public interface IDashboardAuthority{
    public abstract Task<List<ClientModel>> getTop3RecentClients();
}