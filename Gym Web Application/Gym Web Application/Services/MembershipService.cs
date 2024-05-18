using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class MembershipService
{
    private readonly AppDbContext _dbContext;

    public MembershipService(DbContextOptions<AppDbContext> options)
    {
        _dbContext = AppDbContext.GetInstance(options);
    }

    public async Task AddMembershipAsync(MembershipModel MembershipRequest)
    {
        var Membership = new MembershipModel()
        {
            ClientID = MembershipRequest.ClientID,
            PackageID = MembershipRequest.PackageID,
            StartDate = MembershipRequest.StartDate,
            EndDate = MembershipRequest.EndDate,
            VisitsCount = MembershipRequest.VisitsCount,
            InvitationsCount = MembershipRequest.InvitationsCount,
            InbodySessionsCount = MembershipRequest.InbodySessionsCount,
            PrivateTrainingSessionsCount = MembershipRequest.PrivateTrainingSessionsCount,
            FreezeCount = MembershipRequest.FreezeCount,
            Freezed = MembershipRequest.Freezed,
            IsActivated = MembershipRequest.IsActivated
        };
        await _dbContext.Memberships.AddAsync(Membership);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<MembershipModel>> GetAllMemberships()
    {
        return await _dbContext.Memberships.ToListAsync();
    }

    public async Task updateMembership(MembershipModel updatedMembership)
    {
        var Membership = await _dbContext.Memberships.FirstOrDefaultAsync(membership => membership.ID == updatedMembership.ID);
        if (Membership != null)
        {
            Membership.VisitsCount = updatedMembership.VisitsCount;
            Membership.InvitationsCount = updatedMembership.InvitationsCount;
            Membership.InbodySessionsCount = updatedMembership.InbodySessionsCount;
            Membership.PrivateTrainingSessionsCount = updatedMembership.PrivateTrainingSessionsCount;
            Membership.FreezeCount = updatedMembership.FreezeCount;
            Membership.EndDate = updatedMembership.EndDate;
            Membership.Freezed = updatedMembership.Freezed;
            Membership.IsActivated = updatedMembership.IsActivated;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteMembership(int MembershipId)
    {
        var Membership = await _dbContext.Memberships.FirstOrDefaultAsync(mem => mem.ID == MembershipId);
        if (Membership != null)
        {
            _dbContext.Memberships.Remove(Membership);
            await _dbContext.SaveChangesAsync();
        }

    }

    public async Task<MembershipModel> GetMembershipById(int MembershipId)
    {
        var Membership = await _dbContext.Memberships.FirstOrDefaultAsync(mem => mem.ID == MembershipId);

        if (Membership == null)
        {
            return null;
        }

        var MembershipModel = new MembershipModel
        {
            ClientID = Membership.ClientID,
            PackageID = Membership.PackageID,
            StartDate = Membership.StartDate,
            EndDate = Membership.EndDate,
            VisitsCount = Membership.VisitsCount,
            InvitationsCount = Membership.InvitationsCount,
            InbodySessionsCount = Membership.InbodySessionsCount,
            PrivateTrainingSessionsCount = Membership.PrivateTrainingSessionsCount,
            FreezeCount = Membership.FreezeCount,
            Freezed = Membership.Freezed,
            IsActivated = Membership.IsActivated
        };

        return MembershipModel;
    }
    public async Task activateMembership (int clientId, PackageModel package)
    {

        var Membership = new MembershipModel()
        {
            ClientID = clientId,
            PackageID = package.ID,
            StartDate = DateTime.Now.Date,
            EndDate = DateTime.Now.Date.AddDays(package.NumOfMonths),
            VisitsCount = package.VisitsLimit,
            InvitationsCount = package.NumOfInvitations,
            InbodySessionsCount = package.NumOfInbodySessions,
            PrivateTrainingSessionsCount = package.NumOfPrivateTrainingSessions,
            FreezeCount = package.FreezeLimit,
            Freezed = "Not Freezed",
            IsActivated = "Activated"
        };
        await _dbContext.Memberships.AddAsync(Membership);
        await _dbContext.SaveChangesAsync();
    }
    public MembershipModel FindByClientId(int id)
    {
        return _dbContext.Memberships.FirstOrDefault(mem => mem.ClientID == id);
    }
    public int CalculateFreezeDuration(DateTime currentDate, DateTime freezeEndDate)
    {
        int freezeDuration = (int)(freezeEndDate - currentDate).TotalDays;
        return freezeDuration;
    }
    public async Task FreezeMembership(int MembershipId, DateTime FreezeEndDate)
    {
        var Membership = await _dbContext.Memberships.FirstOrDefaultAsync(mem => mem.ID == MembershipId);
        if (Membership != null)
        {
            if (FreezeEndDate > DateTime.Now.Date && FreezeEndDate <= DateTime.Now.Date.AddDays(Membership.FreezeCount))
            {
                int freezeDuration = CalculateFreezeDuration(DateTime.Now.Date, FreezeEndDate.Date);
                DateTime membershipEndDate = Membership.EndDate.AddDays(freezeDuration);
                int newFreezeCount = Membership.FreezeCount - freezeDuration;
                Membership.FreezeCount = newFreezeCount;
                Membership.EndDate = membershipEndDate;
                Membership.Freezed = "Freezed";
                await updateMembership(Membership);
                ScheduledUnfreezeService scheduledUnfreezeService = new ScheduledUnfreezeService(_dbContext);
                await scheduledUnfreezeService.AddScheduledUnfreezeAsync(MembershipId, newFreezeCount, FreezeEndDate);
            }
        }
    }
}
