using Example.Application.Services;
using Example.Application.UseCases.Commands.CreateListing;
using Example.Domain.Sellers;
using Moq;

namespace ApplicationTests
{
    public class CreateListUseCaseTestsFixture
    { 
        public Guid CurrentUserId { get; }
        public Mock<IUserService> MockUserService { get; }
        public Mock<ISellerRepository> MockSellerRepository { get; }
        public CreateListingUseCase UseCase { get; }

        public Mock<IOutputPort> MockOutputPort { get; }

        public CreateListUseCaseTestsFixture()
        {

            CurrentUserId = Guid.Parse("CCCC7626-1C96-4405-A8E4-F24B94114F30");

            MockUserService = new Mock<IUserService>();

            MockSellerRepository = new Mock<ISellerRepository>();

            MockUserService.Setup(m => m.GetCurrentUserId())
                .Returns(CurrentUserId);

            MockOutputPort = new Mock<IOutputPort>();

            UseCase = new CreateListingUseCase(
                userService: MockUserService.Object,
                sellerRepository: MockSellerRepository.Object);

            UseCase.SetOutputPort(MockOutputPort.Object);
        }
    }
}