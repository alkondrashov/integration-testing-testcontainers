using System.Net.Http;
using System.Text;
using Cars.Controllers;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Cars.Tests
{
    public class CarsControllerTests : IClassFixture<IntegrationTestFactory>
    {
        private readonly IntegrationTestFactory _factory;

        private string _endpoint = "/api/cars";

        private ITestOutputHelper _outputHelper { get; }

        public CarsControllerTests(IntegrationTestFactory factory, ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _factory = factory;
        }
        
        [Fact]
        public async void testCreateCar()
        {
            var client = _factory.CreateClient();

            var car = new CarModel { Available = false, Name = "Test Text" };

            var result = await client.PostAsync(_endpoint, new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json"));
            var expectedModel = JsonConvert.DeserializeObject<CarModel>(await result.Content.ReadAsStringAsync());
            
            var response = await client.GetAsync($"{_endpoint}/{expectedModel.Id}");
            var actualModel = JsonConvert.DeserializeObject<CarModel>(await response.Content.ReadAsStringAsync());
            
            Assert.Equal(expectedModel.Id, actualModel.Id);
            Assert.Equal(expectedModel.Name, actualModel.Name);
        }
    }
}
