using Amazon.Runtime;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MONGO_DB_API.Data;
using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MONGO_DB_API.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employee;
        private readonly IMongoCollection<Department> _department;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EmployeeRepository(MongoDBContext context, IConfiguration configuration, IMapper mapper)
        {
            _employee = context.GetCollection<Employee>("Employees");
            _department = context.GetCollection<Department>("Departments");
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResultDto> LoginAsync(LoginDto data)
        {
            ResultDto result = new ResultDto();
            var filter = Builders<Employee>.Filter.Where(x => x.Email == data.Email && x.Password == data.Password);
            var employee = await _employee.Find(filter).FirstOrDefaultAsync();
            if (employee == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "User data is null";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, employee.FirstName.ToString()),
                    new Claim(ClaimTypes.Email, employee.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                    signingCredentials: creds);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                result.UserId = employee.Id.ToString();
                result.Email = employee.Email.ToString();
                result.IsSuccess = true;
                result.Token = jwtToken;
                result.TokenValidity = token.ValidTo;
            }

            return result;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeByDepartmentAsync(string position)
        {
            var filter = ""; //Builders<Employee>.Filter.Where(x => x.DepartmentId.Equals(position, StringComparison.OrdinalIgnoreCase));
            var employees = await _employee.Find(filter).ToListAsync();

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeeDtos;
        }


    }
}
