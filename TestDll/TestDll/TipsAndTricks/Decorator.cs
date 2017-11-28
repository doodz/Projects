using System;

namespace Ividatalink.TipsAndTricks
{
    public class Decorator
    {
        public Decorator()
        {
            var logger3 = new NoNullString(
                              new NoEmptyString(
                                  new NoEscapeString(
                                      new MoreThanTenChar(new Logger()))));
        }
    }


    public interface ILogger
    {
        void WriteLine(string sentence);
    }

    public class NoNullString : ILogger
    {
        private readonly ILogger _logger;

        public NoNullString(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteLine(string sentence)
        {
            if (string.IsNullOrEmpty(sentence)) return;
            _logger.WriteLine(sentence);
        }
    }


    public class NoEmptyString : ILogger
    {
        private readonly ILogger _logger;

        public NoEmptyString(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteLine(string sentence)
        {
            if (string.IsNullOrEmpty(sentence)) return;
            _logger.WriteLine(sentence);
        }
    }


    public class NoEscapeString : ILogger
    {
        private readonly ILogger _logger;

        public NoEscapeString(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteLine(string sentence)
        {
            if (string.IsNullOrEmpty(sentence)) return;
            _logger.WriteLine(sentence);
        }
    }


    public class MoreThanTenChar : ILogger
    {
        private readonly ILogger _logger;

        public MoreThanTenChar(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteLine(string sentence)
        {
            if (string.IsNullOrEmpty(sentence)) return;
            _logger.WriteLine(sentence);
        }
    }

    public class Logger : ILogger
    {
        public void WriteLine(string sentence)
        {
            Console.WriteLine(sentence);
        }
    }

}