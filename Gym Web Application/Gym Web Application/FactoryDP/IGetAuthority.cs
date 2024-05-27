namespace Gym_Web_Application.Models;

public interface IGetAuthority<T>{
    public abstract Task<List<T>> getAll();
}