// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.OperatorEquationInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;

namespace Michaels_programming_language
{
  internal class OperatorEquationInstruction : IInstruction
  {
    public Operator oper;
    public int a1;
    public int a2;
    public string variable;
    public string variable1;
    public string variable2;
    public bool ifvariable1;
    public bool ifvariable2;

    public OperatorEquationInstruction(Operator oper, int a1, int a2, string variable, string variable1, string variable2, bool ifvariable1, bool ifvariable2)
    {
      this.a1 = a1;
      this.a2 = a2;
      this.oper = oper;
      this.variable = variable;
      this.variable1 = variable1;
      this.variable2 = variable2;
      this.ifvariable1 = ifvariable1;
      this.ifvariable2 = ifvariable2;
    }

    public OperatorEquationInstruction(Operator oper, int a1, int a2, string variable)
    {
      this.oper = oper;
      this.a1 = a1;
      this.a2 = a2;
      this.variable = variable;
      this.ifvariable1 = false;
      this.ifvariable2 = false;
    }

    public OperatorEquationInstruction(Operator oper, int a1, string variable, string variable2)
    {
      this.oper = oper;
      this.variable2 = variable2;
      this.variable = variable;
      this.a1 = a1;
      this.ifvariable1 = false;
      this.ifvariable2 = true;
    }

    public OperatorEquationInstruction(Operator oper, string variable, string variable1, string variable2)
    {
      this.oper = oper;
      this.variable2 = variable2;
      this.variable = variable;
      this.variable1 = variable1;
      this.ifvariable1 = true;
      this.ifvariable2 = true;
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 8) == "equalize"))
        return false;
      OperatorEquationInstruction.Load(line);
      return true;
    }

        /*public string GenerateCSharpCode ()
        {
            string symbol = "";
            bool power = false;
            bool percentage = false;
            if (oper == Operator.Add)
                symbol = "+";
            else if (oper == Operator.Subtract)
                symbol = "-";
            else if (oper == Operator.Multiply)
                symbol = "*";
            else if (oper == Operator.Divide)
                symbol = "/";
            else if (oper == Operator.Exponents)
                power = true;
            else if (oper == Operator.Modulus)
                symbol = "%";
            else if (oper == Operator.Percentage)
                percentage = true;
            if (ifvariable1)
                a1 = variable1;
            if (ifvariable2)
                a2 = variable2;
            return variable + " = \"" + a1 + " \" " + symbol + "\"" + a2 + "\""; 
        }*/


    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "equalizeﻃ");
      Program.WriteWrite(writer, (object) (Enum.GetName(typeof (Operator), (object) this.oper) + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.a1) + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.a2) + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable1 + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (this.variable2 + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.ifvariable1) + (object) 'ﻃ'));
      Program.WriteWrite(writer, (object) (Convert.ToString(this.ifvariable2) + (object) '\n'));
    }

    public string GetOriginalCode()
    {
      string str1 = "" + Program.LowerFirstLetter(Enum.GetName(typeof (Operator), (object) this.oper)) + "\nWhat's the name of the variable you want to set the answer to?\n" + this.variable + "\nWhat's your first number?\n";
      string str2 = (!this.ifvariable1 ? str1 + (object) this.a1 : str1 + this.variable1) + "\nWhat's your second number?\n";
      return !this.ifvariable2 ? str2 + (object) this.a2 : str2 + this.variable2;
    }

    public static IInstruction Load(string line)
    {
      OperatorEquationInstruction equationInstruction = new OperatorEquationInstruction(Operator.Add, 0, 0, "", "", "", false, false);
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      equationInstruction.oper = (Operator) Enum.Parse(typeof (Operator), list[1]);
      equationInstruction.a1 = int.Parse(list[2]);
      equationInstruction.a2 = int.Parse(list[3]);
      equationInstruction.variable = list[4];
      equationInstruction.variable1 = list[5];
      equationInstruction.variable2 = list[6];
      equationInstruction.ifvariable1 = bool.Parse(list[7]);
      equationInstruction.ifvariable2 = bool.Parse(list[8]);
      return (IInstruction) equationInstruction;
    }

    public void Run()
    {
      if (this.ifvariable1)
        this.a1 = Convert.ToInt32(Program.variables[this.variable1]);
      if (this.ifvariable2)
        this.a2 = Convert.ToInt32(Program.variables[this.variable2]);
      if (this.oper == Operator.Add)
        Program.variables[this.variable] = (object) (this.a1 + this.a2);
      else if (this.oper == Operator.Divide)
        Program.variables[this.variable] = (object) (this.a1 / this.a2);
      else if (this.oper == Operator.Multiply)
        Program.variables[this.variable] = (object) (this.a1 * this.a2);
      else if (this.oper == Operator.Subtract)
        Program.variables[this.variable] = (object) (this.a1 - this.a2);
      else if (this.oper == Operator.Exponents)
        Program.variables[this.variable] = (object) (Program.IntPower(this.a1, this.a2));
      else if (this.oper == Operator.Modulus)
      {
        Program.variables[this.variable] = (object) (this.a1 % this.a2);
      }
      else
      {
        if (this.oper != Operator.Percentage)
          throw new ArgumentException("Something bad happened.");
        Program.variables[this.variable] = (object) (this.a1 * this.a2 / 100);
      }
    }
  }
}
