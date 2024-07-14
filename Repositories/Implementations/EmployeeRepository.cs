using Amazon.Runtime;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MONGO_DB_API.Data;
using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MONGO_DB_API.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _collection;
        private readonly IMapper _mapper;

        public EmployeeRepository(MongoDBContext context, IMapper mapper)
        {
            _collection = context.GetCollection<Employee>("Employees");
            _mapper = mapper;
        }

        public async Task<ResultDto> LoginAsync(LoginDto data)
        {
            ResultDto result = new ResultDto();
            var filter = Builders<Employee>.Filter.Where(x => x.Email == data.Email && x.Password == data.Password);
            var employees = await _collection.Find(filter).FirstOrDefaultAsync();
            if (employees != null)
            {
                result.IsSuccess = true;
                //result.Token = jwtToken;
                //result.TokenValidity = token.ValidTo;
            }
            return result;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByPositionAsync(string position)
        {
            var filter = Builders<Employee>.Filter.Where(x => x.PositionId.Equals(position, StringComparison.OrdinalIgnoreCase));
            var employees = await _collection.Find(filter).ToListAsync();

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeeDtos;
        }


    }
}
