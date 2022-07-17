using Example.Domain.Sellers;
using Moq;

namespace ApplicationTests
{
    public class CreateListUseCaseTests :IClassFixture<CreateListUseCaseTestsFixture>
    {
        private readonly CreateListUseCaseTestsFixture _fixture;

        public CreateListUseCaseTests(CreateListUseCaseTestsFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Method_Find_Of_SellerRepository_Should_Be_Called()
        {
            var mockSeller = new Seller(_fixture.CurrentUserId);

            _fixture.MockSellerRepository.Setup(
                m => m.Find(It.IsAny<Guid>()))
                .ReturnsAsync(mockSeller); ;

            var mockAssetId = Guid.Parse("6261A988-6009-4F8D-B561-D6362E8B8BE1");
            var mockPrice = 1_000_000m;

            var useCase = _fixture.UseCase;

            await useCase.Execute(assetId: mockAssetId, price: mockPrice);

            //Find{userId} method should be call once
            //CurrentUserId must be the same as mock
            _fixture.MockSellerRepository.Verify(
                m => m.Find(It.Is<Guid>(val => val == _fixture.CurrentUserId)), Times.Once());

            _fixture.MockSellerRepository.Verify(
                m => m.Save(It.IsAny<Seller>()), Times.Once());
        }
    }
}