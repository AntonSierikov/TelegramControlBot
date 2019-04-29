using ControlBot.BL.IServices;
using ControlBot.BL.ReplyMarkups;
using ControlBot.BL.Models;
using ControlBot.Core.Entities;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;
using ControlBot.DAL.IQueries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace ControlBot.BL.Services
{
    internal class NotificationService : BaseService, INotificationService
    {
        private ConcurrentBag<NotificationModel> _notifications;

        private DateTime _notifyMessageUpdatedAt;


        private readonly ITelegramBotClient _botClient;
        private readonly Timer _notifyTimer;

        private const Int32 TimerInterval = 5 * 60000;

        //----------------------------------------------------------------//

        public NotificationService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _botClient = serviceProvider.GetRequiredService<ITelegramBotClient>();
            _notifyTimer = new Timer(Notify, null, 0, TimerInterval);
        }

        //----------------------------------------------------------------//

        public async Task<ConcurrentBag<NotificationModel>> GetNotifications(ISession session)
        {
            if (_notifications == null)
            {
                await ReinitNotificationList(session);
            }

            return _notifications;
        }

        //----------------------------------------------------------------//

        public async void Notify(Object obj)
        {
            using(ISession session = SessionFactory.CreateSession())
            {
                ConcurrentBag<NotificationModel> notifications = await GetNotifications(session);
                IHistoryCommand historyCommand = CommandFactory.CreateCommand<IHistoryCommand>(session);
                foreach (NotificationModel notificationModel in notifications) await Notify(historyCommand, notificationModel);
            }
        }

        //----------------------------------------------------------------//

        public async Task Notify(ICommand<History, Int32> historyCommand, NotificationModel model)
        {
            if(model.NotificationTime <= DateTime.Now.TimeOfDay)
            {
                History history = new History(DateTime.Now, model.CaseId, model.UserId);
                history.Id = await historyCommand.InsertAsync(history);
                String message = $"(№{history.Id}). Has @{model.UserName} done {model.CaseName}?";
                InlineKeyboardMarkup markup = MarkupBuilder.CaseNotifyKeyboardMarkup(history.Id);
                await _botClient.SendTextMessageAsync(model.ChatId, message, replyMarkup: markup);
            }
        }

        //----------------------------------------------------------------//
        //session must be input paramater
        public async Task ReinitNotificationList()
        {
            using(ISession session = SessionFactory.CreateSession())
            {
                await ReinitNotificationList(session);
            }
        }

        //----------------------------------------------------------------//
       
        public async Task ReinitNotificationList(ISession session)
        {
            IEnumerable<Case> cases = await QueryFactory.CreateQuery<ICaseQuery>(session).GetDailyActiveCasesAsync();
            IEnumerable<NotificationModel> notifications = cases.Select(c => new NotificationModel(c.ChatId, c.NextUserId.Value, c.Id, c.NextUser?.UserName, c.CaseName, c.TimeOfDay));
            _notifications = new ConcurrentBag<NotificationModel>(notifications);
            _notifyMessageUpdatedAt = DateTime.Now;
        }
    }
}
