namespace WFHTracker_Server.Interface;

public interface IUserService
{
    Task<List<string>> GetUserPrivilegesAsync(string email);
}