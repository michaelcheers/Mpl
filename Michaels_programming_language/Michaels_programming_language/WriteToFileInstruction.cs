// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.WriteToFileInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class WriteToFileInstruction : IInstruction
  {
    private string Contents;
    private string Path;
    private bool Variable;

    public WriteToFileInstruction(string contents, string path, bool variable)
    {
      this.Contents = contents;
      this.Path = path;
      this.Variable = variable;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "write fileﻃ");
      Program.WriteWrite(writer, (object) (this.Contents.Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.Path.Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) ((string) (object) (bool) (this.Variable) + (object) "\n"));
    }

    public static IInstruction Load(string line)
    {
      WriteToFileInstruction toFileInstruction = new WriteToFileInstruction("", "", false);
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      toFileInstruction.Contents = list[1].Replace("\\n", "\n");
      toFileInstruction.Path = list[2];
      toFileInstruction.Variable = bool.Parse(list[3]);
      return (IInstruction) toFileInstruction;
    }

    public string GetOriginalCode()
    {
      return "" + "write file\nWhere is the file?\n" + this.Path + "\nWhat are the contents?\n" + this.Contents + "\nWould you like to convert as text? False or True." + Convert.ToString(this.Variable);
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 10) == "write file"))
        return false;
      WriteToFileInstruction.Load(line);
      return true;
    }

    public void Run()
    {
      StreamWriter streamWriter = new StreamWriter(this.Path, true);
      if (this.Variable)
        streamWriter.Write(Program.variables[this.Contents]);
      else
        streamWriter.Write(this.Contents);
      ((TextWriter) streamWriter).Flush();
      streamWriter.Close();
      streamWriter.Dispose();
    }
  }
}
