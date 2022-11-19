using Cars.Database;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace Cars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController
    {
        private readonly ILogger<CarsController> _logger;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CarsController(ILogger<CarsController> logger, IDbConnectionFactory dbConnectionFactory)
        {
            _logger = logger;
            _dbConnectionFactory = dbConnectionFactory;
        }

        [HttpPost]
        public async Task<CarModel> Create(CarModel model)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                _logger.LogInformation("connection string: " + connection.ConnectionString);
                _logger.LogInformation("model: " + model);
                _logger.LogInformation("Checked=" + model.Available + " Text" + model.Name);
                var result = await connection.QueryAsync<string>("INSERT INTO cars (name, available) values (@Name, @Available); SELECT LAST_INSERT_ID();", model);
                var id = result.Single();
                return await Get("100");
            }
        }

        [HttpGet("{id}")]
        public async Task<CarModel> Get(string id)
        {
            using (var connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                _logger.LogInformation("Connection: " + connection.ConnectionString);
                var result = await connection.QueryAsync<CarModel>("SELECT id,available,name FROM cars WHERE id=@Id", new { Id = id });
                var model = result.FirstOrDefault();
                _logger.LogInformation("Model with id: " + model.Id);
                return model;
            }
        }
    }
}