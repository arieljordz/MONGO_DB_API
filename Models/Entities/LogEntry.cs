using MongoDB.Bson;

namespace MONGO_DB_API.Models.Entities
{
    public class LogEntry
    {
        public ObjectId Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? LogLevel { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
