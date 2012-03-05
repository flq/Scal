using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Scal.Services
{
    public class ProgramArguments : IEnumerable<string>
    {
        private readonly List<string> _args;

        public ProgramArguments(IEnumerable<string> args)
        {
            _args = args.ToList();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _args.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}