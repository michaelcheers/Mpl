// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.DeclarationInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class DeclarationInstruction : IInstruction
  {
    public string name;
    public object setto;
    public bool Variable;
    public Type orig_type;

    public DeclarationInstruction(string Name, object Setto, bool variable, Type orig_type)
    {
      this.Variable = variable;
      if (variable)
      {
        this.name = Name;
        this.setto = Setto;
      }
      else
      {
        this.name = Name;
        this.setto = Setto;
      }
      this.orig_type = orig_type;
    }

    public DeclarationInstruction(string Name, Type orig_type)
    {
      this.name = Name;
      this.setto = (object) "";
      this.orig_type = orig_type;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "declareﻃ");
      Program.WriteWrite(writer, (object) (this.name.Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.setto).Replace("\n", "\\n") + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.Variable) + (object) 'ﻃ'));
      if (this.setto.GetType() == typeof (string))
        Program.WriteWrite(writer, (object) "000001\n");
      else if (this.setto.GetType() == typeof (int))
      {
        Program.WriteWrite(writer, (object) "000002\n");
      }
      else
      {
        if (!(this.setto.GetType() == typeof (bool)))
          throw new ArgumentException((string) (object) this.setto.GetType() + (object) " not supported.");
        Program.WriteWrite(writer, (object) "000003\n");
      }
    }

    public string GetOriginalCode()
    {
      string str1 = "" + "declare\nWhat's the name of your variable?\n" + this.name.Replace("\n", "\\n") + "\nWhat type is your variable?\n";
      string str2;
      if (this.orig_type == typeof (string))
        str2 = str1 + "string";
      else if (this.orig_type == typeof (int))
      {
        str2 = str1 + "int";
      }
      else
      {
        if (!(this.orig_type == typeof (bool)))
          throw new ArgumentException((string) (object) this.orig_type + (object) " not supported.");
        str2 = str1 + "bool";
      }
      string str3 = str2 + "\nWhat's your value?\n" + this.setto;
      if (this.orig_type != typeof (int))
        str3 = str3 + "\nWould you like to convert as text? False or True.\n" + !this.Variable;
      return str3;
    }

    public static IInstruction Load(string line)
    {
      DeclarationInstruction declarationInstruction = new DeclarationInstruction("", new object(), false, typeof (string));
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      declarationInstruction.name = list[1].Replace("\\n", "\n");
      declarationInstruction.setto = (object) list[2].Replace("\\n", "\n");
      declarationInstruction.Variable = bool.Parse(list[3]);
      if (list.Count == 5)
      {
        if (Convert.ToString(list[4]) == "000001")
          declarationInstruction.orig_type = typeof (string);
        else if (Convert.ToString(list[4]) == "000002")
        {
          declarationInstruction.orig_type = typeof (int);
        }
        else
        {
          if (!(Convert.ToString(list[4]) == "000003"))
            throw new ArgumentException("type " + list[4] + " not supported.");
          declarationInstruction.orig_type = typeof (bool);
        }
      }
      else
      {
        int result;
        declarationInstruction.orig_type = !int.TryParse(list[2].Replace("\\n", "\n"), out result) ? typeof (string) : typeof (int);
      }
      return (IInstruction) declarationInstruction;
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 7) == "declare"))
        return false;
      DeclarationInstruction.Load(line);
      return true;
    }

    public void Run()
    {
      if (this.Variable)
      {
        Program.variables.Add(this.name, (object) null);
        new SetInstruction(this.name, this.setto, this.Variable).Run();
      }
      else
      {
        Program.variables.Add(this.name, (object) null);
        new SetInstruction(this.name, this.setto, this.Variable).Run();
      }
    }
  }
}
