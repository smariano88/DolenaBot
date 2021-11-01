﻿using D0lenaBot.Server.App.Domain;
using System.Threading.Tasks;

namespace D0lenaBot.Server.App.Application.Infrastructure
{
    public interface IDolarSiProvider
    {
        Task<ExchangeRate> GetCurrentExchangeRate();
    }
}
