namespace MONGO_DB_API.Models.DTOs
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public string? Token { get; set; }

        public DateTime? TokenValidity { get; set; }
    }
}
