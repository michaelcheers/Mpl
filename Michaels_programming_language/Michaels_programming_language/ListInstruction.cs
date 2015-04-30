using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Michaels_programming_language
{
    class ListInstruction : IInstruction
    {
        internal List<IInstruction> instructions = new List<IInstruction>();
        public ListInstruction()
        {
        }
        public ListInstruction(List<IInstruction> instructions)
        {
            this.instructions = instructions;
        }
        public void Run ()
        {
            foreach (IInstruction ii in instructions)
                ii.Run();
        }
        public void Save (System.IO.StreamWriter writer)
        {
            throw new InvalidOperationException("Save not expected.");
        }
        public string GetOriginalCode ()
        {
            throw new InvalidOperationException("GetOriginalCode not expected.");
        }
    }
}
