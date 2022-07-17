using Example.Application.Services;
using Example.Application.UseCases.Queries.GetListings;
using Example.Application.UseCases.Queries.GetOffers;
using Example.Domain.Buyers;
using Example.Domain.Sellers;
using Example.Infrastructure.Entities;
using Example.Infrastructure.Queries;
using Example.Infrastructure.Repositories;
using Example.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Modules
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructures(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseInMemoryDatabase(nameof(ApplicationDbContext));
            }, ServiceLifetime.Scoped);

            services.AddScoped<IListingQuery, ListingQuery>();
            services.AddScoped<IOfferQuery, OfferQuery>();

            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();

            services.AddSingleton<IUserService, FakeUserService>();
        }
    }
}
