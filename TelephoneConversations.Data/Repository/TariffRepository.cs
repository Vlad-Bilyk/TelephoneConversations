﻿using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;
using TelephoneConversations.DataAccess.Repository.IRepository;

namespace TelephoneConversations.DataAccess.Repository
{
    public class TariffRepository : Repository<Tariff>, ITariffRepository
    {
        private readonly ApplicationDbContext _db;

        public TariffRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Tariff> UpdateAsync(Tariff entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
