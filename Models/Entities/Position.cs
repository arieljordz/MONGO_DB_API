using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MONGO_DB_API.Models.Entities
{
    public class Position
    {
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
    }
}
