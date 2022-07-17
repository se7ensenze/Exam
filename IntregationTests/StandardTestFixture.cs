using Example.Infrastructure.Entities;
using Example.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IntregationTests
{
    public class StandardTestFixture:IDisposable
    {
        public StandardTestFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyContext")
                .Options;

            DbContext = new ApplicationDbContext(options);

            SellerRepository = new SellerRepository(DbContext);

            BuyerRepository = new BuyerRepository(DbContext);

        }

        public SellerRepository SellerRepository { get; }
        public BuyerRepository BuyerRepository { get; }
        public ApplicationDbContext DbContext { get; }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}