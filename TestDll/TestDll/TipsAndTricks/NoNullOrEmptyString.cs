namespace Ividatalink.TipsAndTricks
{
    public class NoNullOrEmptyString : ILogger
    {
        private readonly ILogger _logger;

        public NoNullOrEmptyString(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteLine(string sentence)
        {
            if (string.IsNullOrEmpty(sentence)) return;
            _logger.WriteLine(sentence);
        }
    }
}