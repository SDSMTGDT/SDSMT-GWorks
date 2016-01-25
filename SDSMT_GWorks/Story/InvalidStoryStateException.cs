using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Story
{
    public class InvalidStoryStateException : Exception
    {
        internal InvalidStoryStateException()
        {
        }

        internal InvalidStoryStateException(string message)
        : base(message)
        {
        }

        internal InvalidStoryStateException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
