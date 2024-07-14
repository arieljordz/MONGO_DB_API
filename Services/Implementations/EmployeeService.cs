using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Repositories.Interfaces;
using MONGO_DB_API.Services.Interfaces;
using AutoMapper;
using MONGO_DB_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MONGO_DB_API.Services.Implementations
{
    public class EmployeeService : EntityService<Employee, EmployeeDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEntityRepository<Employee> repository, IMapper mapper, IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
            : base(repository, mapper)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<ResultDto> LoginAsync(LoginDto data)
        {
            _logger.LogInformation("Logging in employee: {data.Email}", data.Email);

            try
            {
                var result = await _employeeRepository.LoginAsync(data);
                _logger.LogInformation("Successfully logged in employee: {data.Email}", data.Email);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging employee: {data.Email}", data.Email);
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByPositionAsync(string position)
        {
            _logger.LogInformation("Fetching employees by position: {Position}", position);

            try
            {
                var employees = await _employeeRepository.GetEmployeeByPositionAsync(position);
                _logger.LogInformation("Successfully fetched employees by position: {Position}", position);
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employees by position: {Position}", position);
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }

    }
}
