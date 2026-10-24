using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Interfaces.Repos
{
    public interface IActivityLogRepo
    {
        public Task PostActivityLog(ActivityLog activityLog);
        public Task<List<ActivityLog>> GetActivityLogsByUserId(string userId);
        public Task<List<ActivityLog>> GetActivityLogsByIp(string ipAddress);
        public Task<List<ActivityLog>> GetActivityLogsByClient(string client);
        public Task<List<ActivityLog>> GetAllActivityLogs();
    }
}
