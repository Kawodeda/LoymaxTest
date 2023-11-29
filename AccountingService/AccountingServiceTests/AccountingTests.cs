using AccountingService.Models;
using AccountingService.Services;
using AccountingServiceTests.Fakes;

namespace AccountingServiceTests
{
    public class AccountingTests
    {
        [Test]
        public async Task Test_ConcurrentAccounting()
        {
            var clientRepository = new ClientRepositoryFake();
            var clientsService = new ClientService(clientRepository);

            for (int i = 1; i <= 50; i++)
            {
                await clientsService.RegisterClient(
                    new Client(i, $"Last Name #{i}", $"Name #{i}", $"Middle Name #{i}", new DateOnly(1999, 1, 1)));
            }

            var threads = new List<Thread>();
            for (int i = 0; i < 10; i++)
            {
                var thread = new Thread(() =>
                {
                    var accountingService = new ClientAccountingService(clientRepository);
                    IEnumerable<Task> tasks = Enumerable.Range(1, 50).Select(async id =>
                    {
                        await accountingService.CreditClientAccount(id, 100);
                        await accountingService.CreditClientAccount(id, 214);
                        await accountingService.DebitClientAccount(id, 100);
                        await accountingService.CreditClientAccount(id, 18);
                        await accountingService.DebitClientAccount(id, 190);
                    });

                    Task.WaitAll(tasks.ToArray());
                });
                thread.Start();
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            var accountingService = new ClientAccountingService(clientRepository);
            for (int i = 1; i <= 50; i++)
            {
                decimal actualAmount = await accountingService.GetAccountAmount(i);
                Assert.That(actualAmount, Is.EqualTo(420m));
            }
        }
    }
}