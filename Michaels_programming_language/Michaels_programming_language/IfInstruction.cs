// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.IfInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class IfInstruction : IInstruction
  {
    private string variable_name = "";
    private List<IInstruction> instructions;

    public IfInstruction(List<IInstruction> instructions, string variable_name)
    {
      this.instructions = instructions;
      this.variable_name = variable_name;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "if黅");
      Program.WriteWrite(writer, (object) (this.variable_name + "黅" + "黅"));
      Program.writtenwithwritewrite = "";
      StreamWriter writer1 = new StreamWriter(Path.GetTempFileName());
      int num = Program.BytesWrittenWithWriteWrite;
      foreach (IInstruction instruction in this.instructions)
        instruction.Save(writer1);
      writer1.Close();
      Program.BytesWrittenWithWriteWrite = num;
      Program.WriteWrite(writer, (object) Program.writtenwithwritewrite.Replace('\n', '黅'));
      Program.WriteWrite(writer, (object) '\n');
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 2) == "if"))
        return false;
      IfInstruction.Load(line);
      return true;
    }

    public static IInstruction Load(string line)
    {
      IfInstruction ifInstruction = new IfInstruction(new List<IInstruction>(), "");
      List<string> list = new List<string>();
      Program.ToWords(line, '黅', (ICollection<string>) list);
      ifInstruction.variable_name = list[1];
      list.RemoveAt(0);
      list.RemoveAt(0);
      list.RemoveAt(0);
      list.RemoveAt(list.Count - 1);
      foreach (string s in list)
        ifInstruction.instructions.Add(Program.LoadInstruction(s));
      return (IInstruction) ifInstruction;
    }

    public int Know()
    {
      int num = 0;
      IfInstruction ifInstruction = this;
      while (true)
      {
        if (ifInstruction.instructions.Count == 2)
        {
          ifInstruction = (IfInstruction) ifInstruction.instructions[1];
          ++num;
        }
        else
          break;
      }
      return num;
    }

    public string GetOriginalCode()
    {
      string str = "start if\n";
      List<string> list = new List<string>();
      foreach (IInstruction instruction in this.instructions)
        list.Add(instruction.GetOriginalCode());
      if (list.Count > 0)
        str = str + "What's your instruction?\n" + Program.ToString<string>((ICollection<string>) list, "\nWhat's your instruction?\n");
      return str + "\nWhat's your instruction?\nend if";
    }

    public void Run()
    {
      if (!Convert.ToBoolean(Program.variables[this.variable_name]))
        return;
      foreach (IInstruction instruction in this.instructions)
        instruction.Run();
    }
  }
}
