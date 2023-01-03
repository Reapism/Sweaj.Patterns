//using Sweaj.Patterns.AI.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

namespace Sweaj.Patterns.AI.Generation
{
    public struct Transformer
    {
        [Gene]
        public TResult Transform<T, TResult>(Func<T, TResult> input, Func<Func<T, TResult>, TResult> computer)
        {
            var result = computer(input);
            return result;
        }

    }
}