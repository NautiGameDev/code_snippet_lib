using CodeSnippetLib.Server.DTOs.ActivityLogs;
using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Helpers.Mappers
{
    public static class ActivityLogMapper
    {
        public static ActivityLogDto ToActivityLogDto(this ActivityLog activityLog)
        {
            return new ActivityLogDto()
            {
                UserId = activityLog.UserId ?? "Unknown",
                UserName = activityLog.Account?.UserName ?? "Unknown",
                Email = activityLog.Account?.Email ?? "unknown",
                ActivityType = activityLog.ActivityType.ToString(),
                IsSuccess = activityLog.IsSuccess,
                Details = activityLog.Details,
                UserClient = activityLog.UserClient,
                TimeStamp = activityLog.TimeStamp,
                IpAddress = activityLog.IpAddress
            };
        }
    }
}
