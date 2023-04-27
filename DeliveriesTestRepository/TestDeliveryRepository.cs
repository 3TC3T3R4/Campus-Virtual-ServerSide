using CampusVirtual.Domain.Commands.Delivery;
using CampusVirtual.Domain.Entities;
using CampusVirtual.UseCases.Gateway.Repositories;
using Moq;

namespace DeliveriesTestRepository
{
    public class TestDeliveryRepository
    {
        private readonly Mock<IDeliveryRepository> _mockDeliveryRepository;

        public TestDeliveryRepository()
        {
            _mockDeliveryRepository = new();
        }

        [Fact]
        public async Task CreateDelivery()
        {
            // Arrange
            var createDelivery = new CreateDelivery
            {
                contentID = new Guid(),
                uidUser = "1",
                DeliveryField = "Delivery field"
            };
            _mockDeliveryRepository.Setup(x => x.CreateDelivery(createDelivery)).ReturnsAsync("Delivery created");
            // Act
            var result = await _mockDeliveryRepository.Object.CreateDelivery(createDelivery);
            // Assert
            Assert.Equal("Delivery created", result);
        }

        [Fact]
        public async Task DeleteDelivery()
        {
            // Arrange
            var deliveryId = 1;
            _mockDeliveryRepository.Setup(x => x.DeleteDelivery(deliveryId)).ReturnsAsync("Delivery deleted");
            // Act
            var result = await _mockDeliveryRepository.Object.DeleteDelivery(deliveryId);
            // Assert
            Assert.Equal("Delivery deleted", result);
        }

        [Fact]
        public async Task GetDeliveryById()
        {
            //Arrange
            var deliveryId = 1;

            var delivery = new Delivery
            {
                deliveryID = 1,
                contentID = new Guid(),
                uidUser = "1",
                deliveryAt = DateTime.Now,
                DeliveryField = "Delivery field",
                rating = 1,
                comment = "Comment",
                ratedAt = DateTime.Now,
                stateDelivery = 1
            };
            //Act
            _mockDeliveryRepository.Setup(x => x.GetDeliveryById(deliveryId)).ReturnsAsync(delivery);
            var result = await _mockDeliveryRepository.Object.GetDeliveryById(deliveryId);
            //Assert
            Assert.Equal(delivery, result);
        }

        [Fact]
        public async Task GetDeliveriesByUidUser()
        {
            //Arrange
            var uidUser = "1";

            var deliveries = new List<Delivery>
            {
                new Delivery
                {
                    deliveryID = 1,
                    contentID = new Guid(),
                    uidUser = "1",
                    deliveryAt = DateTime.Now,
                    DeliveryField = "Delivery field",
                    rating = 1,
                    comment = "Comment",
                    ratedAt = DateTime.Now,
                    stateDelivery = 1
                },
                new Delivery
                {
                    deliveryID = 2,
                    contentID = new Guid(),
                    uidUser = "1",
                    deliveryAt = DateTime.Now,
                    DeliveryField = "Delivery field",
                    rating = 1,
                    comment = "Comment",
                    ratedAt = DateTime.Now,
                    stateDelivery = 1
                }
            };

            //Act
            _mockDeliveryRepository.Setup(x => x.GetDeliveriesByUidUser(uidUser)).ReturnsAsync(deliveries);
            var result = await _mockDeliveryRepository.Object.GetDeliveriesByUidUser(uidUser);
            //Assert
            Assert.Equal(deliveries, result);
        }

        [Fact]
        public async Task QualifyDelivery()
        {
            //Arrange
            var qualifyDelivery = new QualifyDelivery
            {
                deliveryID = 1,
                rating = 1,
                comment = "Comment"
            };

            _mockDeliveryRepository.Setup(x => x.QualifyDelivery(qualifyDelivery)).ReturnsAsync("Delivery qualified");
            //Act
            var result = await _mockDeliveryRepository.Object.QualifyDelivery(qualifyDelivery);
            //Assert
            Assert.Equal("Delivery qualified", result);
        }

        [Fact]
        public async Task GetDeliveriesByPathId()
        {
            //Arrange
            var pathID = "1";

            var deliveries = new List<Delivery>
            {
                new Delivery
                {
                    deliveryID = 1,
                    contentID = new Guid(),
                    uidUser = "1",
                    deliveryAt = DateTime.Now,
                    DeliveryField = "Delivery field",
                    rating = 1,
                    comment = "Comment",
                    ratedAt = DateTime.Now,
                    stateDelivery = 1
                },
                new Delivery
                {
                    deliveryID = 2,
                    contentID = new Guid(),
                    uidUser = "1",
                    deliveryAt = DateTime.Now,
                    DeliveryField = "Delivery field",
                    rating = 1,
                    comment = "Comment",
                    ratedAt = DateTime.Now,
                    stateDelivery = 1
                }
            };

            //Act
            _mockDeliveryRepository.Setup(x => x.GetDeliveriesByPathId(pathID)).ReturnsAsync(deliveries);

            var result = await _mockDeliveryRepository.Object.GetDeliveriesByPathId(pathID);
            //Assert
            Assert.Equal(deliveries, result);

        }


    }
}