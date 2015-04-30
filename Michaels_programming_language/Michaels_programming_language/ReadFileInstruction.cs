// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.ReadFileInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class ReadFileInstruction : IInstruction
  {
    public string path;
    public string variable_name;
    public bool variable;
    public int digits;
    public bool all;

    public ReadFileInstruction(string Path, string Variable_name, bool Variable)
    {
      this.path = Program.PathChange(Path);
      this.all = true;
      this.variable_name = Variable_name;
      this.variable = Variable;
      this.digits = 0;
    }

    public ReadFileInstruction(string Path, int Digits, string Variable_name, bool Variable)
    {
      this.path = Program.PathChange(Path);
      this.digits = Digits;
      this.all = false;
      this.variable_name = Variable_name;
      this.variable = Variable;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "readﻃ");
      Program.WriteWrite(writer, (object) (this.path.Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable_name.Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable.ToString() + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.digits + 65219));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.all) + (object) '\n'));
    }

    public static IInstruction Load(string line)
    {
      ReadFileInstruction readFileInstruction = new ReadFileInstruction("", "", false);
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      readFileInstruction.path = Program.PathChange(list[1].Replace("\\n", "\n"));
      readFileInstruction.variable_name = list[2].Replace("\\n", "\n");
      readFileInstruction.variable = bool.Parse(list[3]);
      return (IInstruction) readFileInstruction;
    }

    public string GetOriginalCode()
    {
      return "" + "getfile\nWhere is the file?\n" + this.path + "\nWhat's the name of your variable that you would like to set it to?\n" + this.variable_name;
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 4) == "read"))
        return false;
      ReadFileInstruction.Load(line);
      return true;
    }

    public void Run()
    {
      if (this.variable)
      {
        string str = File.ReadAllText(Program.variables[this.path].ToString());
        Program.variables[this.variable_name] = (object) str.Substring(str.Length - this.digits);
      }
      else
      {
        string str = File.ReadAllText(this.path);
        Program.variables[this.variable_name] = this.all ? (object) str : (object) str.Substring(str.Length - this.digits);
      }
    }
  }
}
