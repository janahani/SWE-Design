using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class ScheduledUnfreezeService
{
    private readonly AppDbContext _dbContext;

    public ScheduledUnfreezeService(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task AddScheduledUnfreezeAsync(int membershipID,int FreezeCount ,DateTime FreezeEndDate)
    {
        var ScheduledUnfreeze = new ScheduledUnfreezeModel()
        {
            MembershipID = membershipID,
            FreezeStartDate = DateTime.Now.Date,
            FreezeEndDate = FreezeEndDate,
            FreezeCount = FreezeCount
        };
        await _dbContext.ScheduledUnfreeze.AddAsync(ScheduledUnfreeze);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ScheduledUnfreezeModel>> GetAllScheduledUnfreezes()
    {
        return await _dbContext.ScheduledUnfreeze.ToListAsync();
    }

    public async Task DeleteScheduledUnfreeze(int ScheduledUnfreezeId)
    {
        var ScheduledUnfreeze = await _dbContext.ScheduledUnfreeze.FirstOrDefaultAsync(mem => mem.ID == ScheduledUnfreezeId);
        if (ScheduledUnfreeze != null)
        {
            _dbContext.ScheduledUnfreeze.Remove(ScheduledUnfreeze);
            await _dbContext.SaveChangesAsync();
        }

    }

    public async Task<ScheduledUnfreezeModel> FindByMembershipId(int membershipId)
    {
        return await _dbContext.ScheduledUnfreeze.FirstOrDefaultAsync(mem => mem.MembershipID == membershipId);
    }
}
