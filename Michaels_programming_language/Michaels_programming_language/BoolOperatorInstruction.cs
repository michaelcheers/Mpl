// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.BoolOperatorInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class BoolOperatorInstruction : IInstruction
  {
    private BoolOperator oper = BoolOperator.Equals;
    private object a1 = (object) false;
    private object a2 = (object) false;
    private string variable = "string";
    private string variable1 = "";
    private string variable2 = "";
    private bool ifvariable1 = false;
    private bool ifvariable2 = false;

    public BoolOperatorInstruction()
    {
    }

    public BoolOperatorInstruction(BoolOperator oper, object a1, object a2, string variable, string variable1, string variable2, bool ifvariable1, bool ifvariable2)
    {
      this.oper = oper;
      this.a1 = a1;
      this.a2 = a2;
      this.variable = variable;
      this.variable1 = variable1;
      this.variable2 = variable2;
      this.ifvariable1 = ifvariable1;
      this.ifvariable2 = ifvariable2;
    }

    public BoolOperatorInstruction(BoolOperator oper, object a1, object a2, string variable)
    {
      this.oper = oper;
      this.a1 = a1;
      this.a2 = a2;
      this.variable = variable;
      this.ifvariable1 = false;
      this.ifvariable2 = false;
    }

    public BoolOperatorInstruction(BoolOperator oper, string variable1, bool a2, string variable)
    {
      this.oper = oper;
      this.variable1 = variable1;
      this.a2 = (object) (bool) (a2);
      this.variable = variable;
      this.ifvariable1 = true;
      this.ifvariable2 = false;
    }

    public string GenerateCSharpCode ()
    {
        string symbol = "";
        if (oper == BoolOperator.Equals)
            symbol = "==";
        else if (oper == BoolOperator.Greater_than)
            symbol = ">";
        else if (oper == BoolOperator.Less_than)
            symbol = "<";
        if (ifvariable1)
            a1 = variable1;
        if (ifvariable2)
            a2 = variable2;
        return variable + " = \"" + a1 + " \" " + symbol + "\"" + a2 + "\""; 
    }

    public void Run()
    {
      if (this.ifvariable1)
        this.a1 = (object) Convert.ToString(Program.variables[this.variable1]);
      if (this.ifvariable2)
        this.a2 = (object) Convert.ToString(Program.variables[this.variable2]);
//      if (this.oper == BoolOperator.Not)
//        Program.variables[this.variable] = (object) (bool) (! (bool) this.a1);
      if (this.oper == BoolOperator.Equals)
        Program.variables[this.variable] = (object) (bool) (Convert.ToString(this.a1) == Convert.ToString(this.a2));
      if (this.oper == BoolOperator.Greater_than)
        Program.variables[this.variable] = (object) (bool) (Convert.ToInt32(this.a1) > Convert.ToInt32(this.a2));
      if (this.oper != BoolOperator.Less_than)
        return;
      Program.variables[this.variable] = (object) (bool) (Convert.ToInt32(this.a1) < Convert.ToInt32(this.a2));
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 12) == "boolequalize"))
        return false;
      BoolOperatorInstruction.Load(line);
      return true;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "boolequalizeﻃ");
      Program.WriteWrite(writer, (object) (Enum.GetName(typeof (BoolOperator), (object) this.oper) + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.a1) + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.a2) + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable1 + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable2 + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.ifvariable1) + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.ifvariable2) + (object) '\n'));
    }

    public static IInstruction Load(string line)
    {
      BoolOperatorInstruction operatorInstruction = new BoolOperatorInstruction();
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      operatorInstruction.oper = (BoolOperator) Enum.Parse(typeof (BoolOperator), list[1]);
      operatorInstruction.a1 = (object) list[2];
      operatorInstruction.a2 = (object) list[3];
      operatorInstruction.variable = list[4];
      operatorInstruction.variable1 = list[5];
      operatorInstruction.variable2 = list[6];
      operatorInstruction.ifvariable1 = bool.Parse(list[7]);
      operatorInstruction.ifvariable2 = bool.Parse(list[8]);
      return (IInstruction) operatorInstruction;
    }

    public string GetOriginalCode()
    {
      string str1 = "" + Program.LowerFirstLetter(Enum.GetName(typeof (BoolOperator), (object) this.oper).Replace('_', ' ')) + "\nWhat's the name of the variable you want to set the answer to?\n" + this.variable + "\nWhat's your first value?\n";
      string str2 = (!this.ifvariable1 ? str1 + this.a1 : str1 + this.variable1) + "\nWould you like to convert as text? False or True.\n" + !this.ifvariable1 + "\nWhat's your second value?\n";
      return (!this.ifvariable2 ? str2 + this.a2 : str2 + this.variable2) + "\nWould you like to convert as text? False or True.\n" + !ifvariable2;
    }
  }
}
