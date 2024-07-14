namespace MONGO_DB_API.Helpers
{
    public class DatabaseLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseLoggerProvider(Func<string, LogLevel, bool> filter, IServiceScopeFactory scopeFactory)
        {
            _filter = filter;
            _scopeFactory = scopeFactory;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(categoryName, _filter, _scopeFactory);
        }

        public void Dispose()
        {
        }
    }

}
