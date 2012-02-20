using System;

namespace Scal
{
    /// <summary>
    /// Throw this to ensure that a run of startup tasks is interrupted
    /// </summary>
    public class TaskChainTerminationException : Exception
    {
        public TaskChainTerminationException()
        {
        }

        public TaskChainTerminationException(string message) : base(message)
        {
        }

        public TaskChainTerminationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}