using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MONGO_DB_API.Models.Entities
{
    public class DepartmentDto
    {
        public string? Id { get; set; }

        public string? Description { get; set; }
    }
}
