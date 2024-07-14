using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MONGO_DB_API.Models.DTOs
{
    public class EmployeeDto
    {
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string? PositionId { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
