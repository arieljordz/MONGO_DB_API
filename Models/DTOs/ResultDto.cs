namespace MONGO_DB_API.Models.DTOs
{
    public class ResultDto
    {
        public string? UserId { get; set; }

        public string? Email { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public string? Token { get; set; }

        public DateTime? TokenValidity { get; set; }
    }
}
