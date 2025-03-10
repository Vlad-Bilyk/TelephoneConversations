﻿using System.Linq.Expressions;
using TelephoneConversations.Core.Interfaces;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.Entities;

namespace TelephoneConversations.Core.Services
{
    public class CallService : ICallService
    {
        private readonly ICallRepository _callRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IDiscountRepository _discountRepository;

        public CallService(ICallRepository callRepository, ITariffRepository tariffRepository, IDiscountRepository discountRepository)
        {
            _callRepository = callRepository;
            _tariffRepository = tariffRepository;
            _discountRepository = discountRepository;
        }

        public async Task<List<Call>> GetAllAsync(Expression<Func<Call, bool>>? filter = null)
        {
            return await _callRepository.GetAllAsync(filter);
        }

        public async Task<Call> GetAsync(Expression<Func<Call, bool>>? filter = null, bool tracked = true)
        {
            return await _callRepository.GetAsync(filter, tracked);
        }

        public Task<IEnumerable<Call>> SearchСallsAsync(string? cityName, string? subscriberName, CancellationToken cancellationToken = default)
        {
            return _callRepository.SearchСallsAsync(cityName, subscriberName, cancellationToken);
        }

        public async Task<Call> CreateCallAsync(Call call)
        {
            if (call == null)
            {
                throw new ArgumentNullException(nameof(call));
            }

            decimal tariffRate = await GetTariffAsync(call);
            call.BaseCost = CalculateBaseCost(call, tariffRate);
            call.Discount = await GetDiscountRateAsync(call);
            call.CostWithDiscount = CalculateCostWithDiscount(call.BaseCost, call.Discount);

            await _callRepository.CreateAsync(call);

            return call;
        }

        private async Task<decimal> GetTariffAsync(Call call)
        {
            var tariff = await _tariffRepository.GetAsync(t => t.CityID == call.CityID);
            if (tariff == null)
            {
                throw new ArgumentException("Не знайдено тариф для обраного міста.");
            }

            return string.Equals(call.TimeOfDay, "день", StringComparison.OrdinalIgnoreCase)
                ? tariff.DayPrice
                : tariff.NightPrice;
        }

        private async Task<decimal> GetDiscountRateAsync(Call call)
        {
            var discount = await _discountRepository.GetDiscountByDurationAsync(call.Duration);
            return discount != null ? discount.DiscountRate : 0;
        }

        private static decimal CalculateBaseCost(Call call, decimal tariffRate)
        {
            return Math.Round(((call.Duration / 60m) * tariffRate), 2);
        }

        private static decimal CalculateCostWithDiscount(decimal baseCost, decimal discountRate)
        {
            return Math.Round((baseCost * (1 - discountRate / 100m)), 2);
        }
    }
}
