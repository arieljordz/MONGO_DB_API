using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;

namespace MONGO_DB_API.Models.Entities
{
    public class Employee
    {
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public string? PositionId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
