using AccountingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace AccountingService.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AccountingDbContext _context;

        public ClientRepository(AccountingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Client> List()
        {
            return _context.Clients;
        }

        public async Task<Client?> Read(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client?> ReadWithWallet(int id)
        {
            using IDbContextTransaction transaction = _context.Database.BeginTransaction();
            Client? client = await _context.Clients.FindAsync(id);

            if (client != null)
            {
                Wallet? wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.ClientId == client.Id);
                client = client.WithWallet(wallet);
            }

            transaction.Commit();

            return client;
        }

        public async Task<Client> Create(Client client)
        {
            using IDbContextTransaction transaction = _context.Database.BeginTransaction();
            Client created = await CreateEntity(client);

            if (client.Wallet != null)
            {
                await CreateEntity(client.Wallet.WithClientId(created.Id));
            }

            transaction.Commit();

            return created;
        }

        public async Task<Client> Update(int id, Client client)
        {
            using IDbContextTransaction transaction = _context.Database.BeginTransaction();
            
            Client updated = await UpdateEntity(client);

            if (client.Wallet != null)
            {
                await UpdateEntity(client.Wallet);
            }

            transaction.Commit();

            return updated;
        }

        private async Task<TEntity> CreateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            return await SaveEntityState(entity, EntityState.Added);
        }

        private async Task<TEntity> UpdateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            return await SaveEntityState(entity, EntityState.Modified);
        }

        private async Task<TEntity> SaveEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            EntityEntry<TEntity> entry = _context.Entry(entity);
            entry.State = state;

            await _context.SaveChangesAsync();

            entry.State = EntityState.Detached;

            return entry.Entity;
        }
    }
}