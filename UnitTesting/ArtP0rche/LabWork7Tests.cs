using Moq;
using System.Diagnostics.Contracts;
using TestingLib.Shop;
using TestingLib.Weather;

namespace UnitTesting.ArtP0rche
{
    public class LabWork7Tests
    {
        private readonly Mock<IWeatherForecastSource> mockWeatherForecastSource;
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<IOrderRepository> mockOrderRepository;
        private readonly Mock<INotificationService> mockNotificationService;

        public LabWork7Tests()
        {
            mockWeatherForecastSource = new Mock<IWeatherForecastSource>();
            mockCustomerRepository = new Mock<ICustomerRepository>();
            mockOrderRepository = new Mock<IOrderRepository>();
            mockNotificationService = new Mock<INotificationService>();
        }

        [Fact]
        public void GetWeatherForecast_ShouldReturnWheatherForecast()
        {
            var dateTime = DateTime.Now;
            // Arrange
            var weatherForecast = new WeatherForecast { Date = dateTime, TemperatureC = 15, Summary = "Погода пасмурная, без осадков" };

            mockWeatherForecastSource.Setup(src => src.GetForecast(dateTime)).Returns(weatherForecast);

            var service = new WeatherForecastService(mockWeatherForecastSource.Object);

            // Act
            var result = service.GetWeatherForecast(dateTime);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        private void CreateOrder_ShouldCreateOrder()
        {
            var dateTime = DateTime.Now;
            var customer = new Customer { Id = 1, Email = "fog@kiki.vom", Name = "lolo" };
            var order = new Order { Id = 1, Date = dateTime, Customer = customer, Amount = 1 };

            mockCustomerRepository.Setup(repo => repo.AddCustomer(customer));
            mockOrderRepository.Setup(repo => repo.AddOrder(order));

            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);
            service.CreateOrder(order);

            mockOrderRepository.Verify(repo => repo.AddOrder(It.IsAny<Order>()), Times.Once);
            mockNotificationService.Verify(repo => repo.SendNotification(customer.Email, $"Order {order.Id} created for customer {order.Customer.Name} total price {order.Amount}"), Times.Once);
        }

        [Fact]
        private void GetCustomerInfo_ShouldReturnCustomerInfo()
        {
            var dateTime = DateTime.Now;
            var customer = new Customer { Id = 1, Email = "fog@kiki.vom", Name = "lolo" };
            var order = new Order { Id = 1, Date = dateTime, Customer = customer, Amount = 1 };

            mockCustomerRepository.Setup(repo => repo.GetCustomerById(1)).Returns(customer);
            mockOrderRepository.Setup(repo => repo.GetOrders()).Returns(new List<Order> { order });

            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);
            var result = service.GetCustomerInfo(1);
            
            Assert.Equal("Customer lolo has 1 orders", result);
            mockCustomerRepository.Verify(repo => repo.GetCustomerById(1));
            mockOrderRepository.Verify(repo => repo.GetOrders());
        }
    }
}
