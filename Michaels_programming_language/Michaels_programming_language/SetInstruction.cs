// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.SetInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class SetInstruction : IInstruction
  {
    public bool variable;
    public string name;
    public object setto;

    public SetInstruction(string Name, object value, bool Variable)
    {
      this.variable = Variable;
      this.name = Name;
      this.setto = value;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "setﻃ");
      Program.WriteWrite(writer, (object) (this.variable.ToString() + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.name.Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.setto.ToString().Replace("\n", "\\n") + (object) '\n'));
    }

    public static IInstruction Load(string line)
    {
      SetInstruction setInstruction = new SetInstruction("", (object) "", false);
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      setInstruction.variable = Convert.ToBoolean(list[1]);
      setInstruction.name = list[2].Replace("\\n", "\n");
      setInstruction.setto = (object) list[3].Replace("\\n", "\n");
      return (IInstruction) setInstruction;
    }

    public string GetOriginalCode()
    {
      string str = "" + "set\nWhat's the name of your variable?\n" + this.name + "\nWhat's your value?\n" + Convert.ToString(this.setto);
      bool flag = false;
      foreach (IInstruction instruction in Program.program)
      {
        if (instruction.GetType() == typeof (DeclarationInstruction) && ((DeclarationInstruction) instruction).name == this.name && (((DeclarationInstruction) instruction).orig_type == typeof (string) || ((DeclarationInstruction) instruction).orig_type == typeof (bool)))
          flag = true;
      }
      if (flag)
        str = str + "\nWould you like to convert as text? False or True.\n" + Convert.ToString(!this.variable);
      return str;
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 3) == "set"))
        return false;
      SetInstruction.Load(line);
      return true;
    }

    public void Run()
    {
      if (this.variable)
        Program.variables[this.name] = Program.variables[(string) this.setto];
      else
        Program.variables[this.name] = this.setto;
    }
  }
}
