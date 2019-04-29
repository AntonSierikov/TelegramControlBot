using System;

namespace ControlBot.BL.Models
{
    public class NotificationModel
    {
        public Int64 ChatId { get; }

        public Int32 UserId { get; }

        public String UserName { get; }

        public Int32 CaseId { get; }

        public String CaseName { get; }

        public TimeSpan NotificationTime { get; }

        public NotificationModel(Int64 chatId, Int32 userId, Int32 caseId, String caseName, String userName, TimeSpan notificationDateTime)
        {
            ChatId = chatId;
            CaseId = caseId;
            UserId = userId;
            CaseName = caseName;
            NotificationTime = notificationDateTime;
        }
    }
}
