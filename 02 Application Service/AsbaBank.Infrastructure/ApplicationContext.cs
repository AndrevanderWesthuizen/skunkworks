using System.Data.Entity;
using AsbaBank.Core;
using AsbaBank.Domain.Models;

namespace AsbaBank.Infrastructure
{
    public class ApplicationContext : DbContext, IDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<BankCard> BankCards { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
