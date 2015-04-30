// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.DeleteFileInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class DeleteFileInstruction : IInstruction
  {
    public string file_name;
    public bool variable;

    public DeleteFileInstruction(string file_name, bool variable)
    {
      this.file_name = file_name;
      this.variable = variable;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) (this.file_name + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.variable) + (object) '\n'));
    }

    public IInstruction Load(string line)
    {
      DeleteFileInstruction deleteFileInstruction = new DeleteFileInstruction("", false);
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      deleteFileInstruction.file_name = list[1];
      deleteFileInstruction.variable = bool.Parse(list[2]);
      return (IInstruction) deleteFileInstruction;
    }

    public string GetOriginalCode()
    {
      return "" + "delete file\nWhere is the file you would like to delete?\n" + this.file_name + "\nWould you like to convert as text? False or True.\n" + Convert.ToString(this.variable);
    }

    public bool TryLoad(string line)
    {
      if (!(line.Substring(0, 7) == "delete file"))
        return false;
      this.Load(line);
      return true;
    }

    public void Run()
    {
      if (!this.variable)
        File.Delete(this.file_name);
      else
        File.Delete(Convert.ToString(Program.variables[this.file_name]));
    }
  }
}
