using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "";
            if (args.Length > 0 && args != null)
                path = args[0];
            else
                path = "start.mpl";
            Process.Start("language.exe", "auto \"" + path + "\"");
        }
    }
}
