using MONGO_DB_API.Data;
using MONGO_DB_API.Models.Entities;

namespace MONGO_DB_API.Helpers
{
    public class DatabaseLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseLogger(string categoryName, Func<string, LogLevel, bool> filter, IServiceScopeFactory scopeFactory)
        {
            _categoryName = categoryName;
            _filter = filter;
            _scopeFactory = scopeFactory;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null; // Optional, not used in this example
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter(_categoryName, logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message) && exception == null)
            {
                return;
            }

            // Example: Save log entry to the database
            var logEntry = new LogEntry
            {
                LogLevel = logLevel.ToString(),
                Timestamp = DateTime.UtcNow,
                Message = message,
                Exception = exception?.ToString()
            };

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MongoDBContext>();
                var logsCollection = dbContext.GetCollection<LogEntry>("Logs"); // Assuming "Logs" is the collection name
                logsCollection.InsertOne(logEntry); // Insert logEntry into the "Logs" collection
            }
        }
    }

}
