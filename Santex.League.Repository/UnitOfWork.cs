using Santex.League.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Santex.League.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public LeagueDbContext context;

        public UnitOfWork(LeagueDbContext context)
        {
            this.context = context;
        }

        public void SaveChanges() => context.SaveChanges();

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
                context.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
