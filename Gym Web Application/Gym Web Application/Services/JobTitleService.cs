using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class JobTitleService
{
    private readonly AppDbContext _dbContext;

    public JobTitleService(DbContextOptions<AppDbContext> options)
    {
        _dbContext = AppDbContext.GetInstance(options);
    }

    public async Task<List<JobTitlesModel>> GetAllJobTitles()
    {
        return await _dbContext.JobTitles.ToListAsync();
    }

    public JobTitlesModel FindById(int id)
    {
        return _dbContext.JobTitles.FirstOrDefault(p => p.ID == id);
    }

}