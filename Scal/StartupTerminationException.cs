using System;

namespace Scal
{
    /// <summary>
    /// Throw this to ensure that 
    /// </summary>
    public class StartupTerminationException : Exception
    {
        public StartupTerminationException()
        {
        }

        public StartupTerminationException(string message) : base(message)
        {
        }

        public StartupTerminationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}