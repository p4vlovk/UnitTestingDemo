namespace DemoCode
{
    using System;

    public static class LogMessageCreator
    {
        public static LogMessage Create(string message, DateTime when) => new LogMessage
        {
            Year = when.Year,
            Message = message
        };
    }
}
