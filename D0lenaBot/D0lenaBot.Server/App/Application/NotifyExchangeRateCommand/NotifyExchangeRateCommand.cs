﻿using D0lenaBot.Server.App.Application.Infrastructure;
using D0lenaBot.Server.App.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace D0lenaBot.Server.App.Application.NotifyExchangeRateCommand
{
    // ToDo: 
    // * Notifications should be in a queue, so we have retries for free, among other benefits
    internal class NotifyExchangeRateCommand : INotifyExchangeRateCommand
    {
        private readonly IExchangeRates exchangeRates;
        private readonly IUsers users;
        private readonly INotificationSender notificationSender;
        public NotifyExchangeRateCommand(IExchangeRates exchangeRates, IUsers users, INotificationSender notificationSender)
        {
            this.exchangeRates = exchangeRates;
            this.users = users;
            this.notificationSender = notificationSender;
        }

        public async Task Send(DateTime date)
        {
            ExchangeRate exchangeRate = await this.exchangeRates.Get(date);
            var chatIds = (await this.users.GetAll()).Select(u => u.ChatId);

            foreach (var chatId in chatIds)
            {
                await this.notificationSender.Send(exchangeRate, chatId);
            }
        }
    }
}
