//using Sweaj.Patterns.AI.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

namespace Sweaj.Patterns.AI.Generation
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Auto)]
    public struct Organism
    {
        [Gene]
        public byte GeneA;

        [Gene]
        public byte GeneB;

        [Gene]
        public byte GeneC;
    }
}