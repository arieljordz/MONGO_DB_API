using Microsoft.AspNetCore.Mvc;
using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MONGO_DB_API.Services.Interfaces
{
    public interface IEmployeeService : IEntityService<Employee, EmployeeDto>
    {
        Task<ResultDto> LoginAsync(LoginDto data);

        Task<IEnumerable<EmployeeDto>> GetEmployeeByPositionAsync(string position);
    }
}
