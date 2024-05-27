using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class MembershipsService
{
    private MembershipAuthoritiesFactory _membershipAuthoritiesFactory;

    public MembershipsService(MembershipAuthoritiesFactory membershipAuthoritiesFactory)
    {
        _membershipAuthoritiesFactory = membershipAuthoritiesFactory;
    }

    public async Task addMembership(MembershipModel membership)
    {
        await _membershipAuthoritiesFactory.AddMembershipAsync(membership);
    }
    public async Task activateMembership(int clientID, PackageModel package)
    {
        await _membershipAuthoritiesFactory.activateMembership(clientID, package);
    }
    public async Task<List<MembershipModel>> GetAllMemberships()
    {
        return await _membershipAuthoritiesFactory.getAll();
    }
    public async Task updateMembership(MembershipModel membership)
    {
        await _membershipAuthoritiesFactory.updateMembership(membership);
    }
    public async Task DeleteMembership(int membershipID)
    {
        await _membershipAuthoritiesFactory.DeleteMembership(membershipID);
    }
    public async Task<MembershipModel> GetMembershipById(int membershipID)
    {
        return await _membershipAuthoritiesFactory.GetMembershipById(membershipID);
    }
    public async Task<MembershipModel> FindByClientId(int clientID)
    {
        return await _membershipAuthoritiesFactory.GetMembershipById(clientID);
    }
    public async Task<bool> hasActiveMembership(int clientID)
    {
        return await _membershipAuthoritiesFactory.hasActiveMembership(clientID);
    }
    public async Task FreezeMembership(int MembershipId, DateTime FreezeEndDate)
    {
        await _membershipAuthoritiesFactory.FreezeMembership(MembershipId, FreezeEndDate);
    }
    public async Task UnfreezeMembership(int MembershipId)
    {
        await _membershipAuthoritiesFactory.UnfreezeMembership(MembershipId);
    }


}
