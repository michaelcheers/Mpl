// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.GetInputInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class GetInputInstruction : IInstruction
  {
    private string Variable_Name;

    public GetInputInstruction(string variable_name)
    {
      this.Variable_Name = variable_name;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "getinputﻃ");
      Program.WriteWrite(writer, (object) (this.Variable_Name.Replace("\n", "\\n") + (object) '\n'));
    }

    public static IInstruction Load(string line)
    {
      GetInputInstruction inputInstruction = new GetInputInstruction("");
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      inputInstruction.Variable_Name = list[1].Replace("\\n", "\n");
      return (IInstruction) inputInstruction;
    }

    public string GetOriginalCode()
    {
      return "" + "getinput\nWhat's the name of the variable you want to set the input to. (string means none)\n" + this.Variable_Name;
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 8) == "getinput"))
        return false;
      GetInputInstruction.Load(line);
      return true;
    }

    public void Run()
    {
      Program.variables[this.Variable_Name] = (object) Console.ReadLine();
    }
  }
}
