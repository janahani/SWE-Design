using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class MembershipService
{
    private MembershipAuthoritiesFactory membershipAuthoritiesFactory;

    public MembershipService(MembershipAuthoritiesFactory membershipAuthoritiesFactory)
    {
        membershipAuthoritiesFactory = membershipAuthoritiesFactory;
    }

    public async Task addMembership(MembershipModel membership)
    {
        await membershipAuthoritiesFactory.AddMembershipAsync(membership);
    }
    public async Task activateMembership(int clientID, PackageModel package)
    {
        await membershipAuthoritiesFactory.activateMembership(clientID, package);
    }
    public async Task<List<MembershipModel>> GetAllMemberships()
    {
        return await membershipAuthoritiesFactory.GetAllMemberships();
    }
    public async Task updateMembership(MembershipModel membership)
    {
        await membershipAuthoritiesFactory.updateMembership(membership);
    }
    public async Task DeleteMembership(int membershipID)
    {
        await membershipAuthoritiesFactory.DeleteMembership(membershipID);
    }
    public async Task<MembershipModel> GetMembershipById(int membershipID)
    {
        return await membershipAuthoritiesFactory.GetMembershipById(membershipID);
    }
    public async Task<MembershipModel> FindByClientId(int clientID)
    {
        return await membershipAuthoritiesFactory.GetMembershipById(clientID);
    }
    public async Task<bool> hasActiveMembership(int clientID)
    {
        return await membershipAuthoritiesFactory.hasActiveMembership(clientID);
    }
    public async Task FreezeMembership(int MembershipId, DateTime FreezeEndDate)
    {
        await membershipAuthoritiesFactory.FreezeMembership(MembershipId, FreezeEndDate);
    }
    public async Task UnfreezeMembership(int MembershipId)
    {
        await membershipAuthoritiesFactory.UnfreezeMembership(MembershipId);
    }


}
