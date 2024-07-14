using MONGO_DB_API.Models.DTOs;
using MONGO_DB_API.Models.Entities;
using System.Threading.Tasks;

namespace MONGO_DB_API.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ResultDto> LoginAsync(LoginDto data);

        Task<IEnumerable<EmployeeDto>> GetEmployeeByPositionAsync(string position);
    }
}
