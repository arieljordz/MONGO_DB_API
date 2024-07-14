using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MONGO_DB_API.Models.Entities
{
    public class PositionDto
    {
        public string? Id { get; set; }

        public string? Description { get; set; }
    }
}
