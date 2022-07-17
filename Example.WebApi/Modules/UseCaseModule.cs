using Example.Application.UseCases.Commands.ApproveOffer;
using Example.Application.UseCases.Commands.CancelListing;
using Example.Application.UseCases.Commands.CancelOffer;
using Example.Application.UseCases.Commands.CreateListing;
using Example.Application.UseCases.Commands.CreateOffer;
using Example.Application.UseCases.Queries.GetListings;
using Example.Application.UseCases.Queries.GetOffers;

namespace Example.WebApi.Modules
{
    public static class UseCaseModule
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetListingUseCase, GetListingUseCase>();
            services.AddScoped<IGetOffersUseCase, GetOffersUseCase>();

            services.AddScoped<ICreateListingUseCase, CreateListingUseCase>();
            services.AddScoped<IApproveOfferUseCase, ApproveOfferUseCase>();
            services.AddScoped<ICancelListingUseCase, CancelListingUseCase>();

            services.AddScoped<ICreateOfferUseCase, CreateOfferUseCase>();
            services.AddScoped<ICancelOfferUseCase, CancelOfferUseCase>();
        }
        
    }
}
