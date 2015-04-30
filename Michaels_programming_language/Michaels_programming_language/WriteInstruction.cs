// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.WriteInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class WriteInstruction : IInstruction
  {
    public string variable_name;
    public object write;
    public bool variable;

    public WriteInstruction(object Write, bool Variable)
    {
      if (Variable)
      {
        this.variable_name = Convert.ToString(Write);
        this.variable = true;
        this.write = (object) "";
      }
      else
      {
        this.write = Write;
        this.variable_name = "";
      }
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "writeﻃ");
      Program.WriteWrite(writer, (object) (this.variable_name.Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.write.ToString().Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable.ToString() + (object) '\n'));
    }

    public static IInstruction Load(string line)
    {
      WriteInstruction writeInstruction = new WriteInstruction((object) "", false);
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      writeInstruction.variable_name = list[1].Replace("\\n", "\n");
      writeInstruction.write = (object) list[2].Replace("\\n", "\n");
      writeInstruction.variable = bool.Parse(list[3]);
      return (IInstruction) writeInstruction;
    }

    public string GetOriginalCode()
    {
      string str = "" + "write\nWhat's your value?\n";
      return (!this.variable ? str + Convert.ToString(this.write).Replace("\n", "\\n") : str + this.variable_name.Replace("\n", "\\n")) + "\nWould you like to convert as text? False or True.\n" + Convert.ToString(!this.variable);
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 5) == "write"))
        return false;
      WriteInstruction.Load(line);
      return true;
    }

    public void Run()
    {
      if (this.variable)
        Console.Write(Program.variables[this.variable_name]);
      else
        Console.Write(this.write);
    }
  }
}
