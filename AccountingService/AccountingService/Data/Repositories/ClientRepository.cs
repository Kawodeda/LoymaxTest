using AccountingService.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AccountingService.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AccountingDbContext _context;

        public ClientRepository(AccountingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> List()
        {
            return _context.Clients;
        }

        public async Task<Client?> Read(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client?> ReadWithWallet(int id)
        {
            Client? client = await _context.Clients.FindAsync(id);

            if (client != null)
            {
                await _context.Entry(client)
                    .Reference(c => c.Wallet)
                    .LoadAsync();
            }

            return client;
        }

        public async Task<Client> Create(Client client)
        {
            EntityEntry<Client> entry =  await _context.Clients.AddAsync(client);

            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task Update(int id, Client client)
        {
            _context.Clients.Update(client);

            await _context.SaveChangesAsync();
        }
    }
}