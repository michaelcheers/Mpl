// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.Public
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Michaels_programming_language
{
  internal static class Public
  {
    public static void Load(string path)
    {
      if (!File.Exists(path))
        throw new FileNotFoundException(path + " not found.");
      Program.program.Clear();
      Program.encoded_files.Clear();
        Program.written = new List<KeyValuePair<char, ConsoleColor>>();
        char key = '\x0';
        char ch1 = '\x0';
        TriBool triBool = TriBool.False;
        bool flag1 = false;
        string s = "";
        bool flag2 = false;
        bool flag3 = false;
        MplPeriod mplPeriod = MplPeriod.LoadInstruction;
        List<string> list1 = new List<string>();
        string input = File.ReadAllText(path);
        Program.ToWords(input, '\n', (ICollection<string>) list1);
        foreach (string str1 in list1)
        {
          if (str1 == "")
          {
            if (!flag1)
              ++mplPeriod;
            flag1 = true;
            if (mplPeriod == MplPeriod.Instance && (!Program.auto))
            {
              Console.Clear();
              foreach (KeyValuePair<char, ConsoleColor> keyValuePair in Program.written)
              {
                Console.ForegroundColor = keyValuePair.Value;
                Console.Write(keyValuePair.Key);
              }
              Console.ResetColor();
            }
          }
          else
          {
            flag1 = false;
            if (mplPeriod == MplPeriod.LoadInstruction)
                Program.program.Add(Program.LoadInstruction(str1));
            else if (mplPeriod == MplPeriod.Written)
            {
                foreach (char ch2 in str1)
                {
                    if (triBool == TriBool.False)
                        key = ch2;
                    else if (triBool == TriBool.Null)
                        ch1 = ch2;
                    else
                        Program.written.Add(new KeyValuePair<char, ConsoleColor>(key, (ConsoleColor)Convert.ToInt32(ch1.ToString() + ch2.ToString())));
                    if (triBool == TriBool.True)
                        triBool = TriBool.False;
                    else
                        ++triBool;
                }
                key = '\n';
                triBool = TriBool.Null;
            }
            else if (mplPeriod == MplPeriod.Password)
            {
                Random random = new Random();
                Console.Clear();
                bool flag4 = true;
                while (flag4)
                {
                    Console.WriteLine("What's the password?");
                    string str2 = Console.ReadLine();
                    Thread.Sleep(random.Next(1000));
                    int result;
                    if (!int.TryParse(str1, out result) && str2 == str1)
                    {
                        flag2 = true;
                        flag4 = false;
                    }
                    if (str2.GetHashCode() == result)
                    {
                        flag2 = true;
                        flag4 = false;
                    }
                    else if (Program.master_mode)
                    {
                        Console.WriteLine("Would you like to reconstruct and remove password?");
                        string str3;
                        do
                        {
                            str3 = Console.ReadLine();
                            if (str3.ToLower() == "yes")
                            {
                                Program.password = 0;
                                flag4 = false;
                            }
                        }
                        while (str3.ToLower() != "yes" && str3.ToLower() != "no");
                    }
                }
                Program.password = int.Parse(str1);
                Console.Clear();
                Program.WriteWritten();
            }
            else if (mplPeriod == MplPeriod.MagicNumber)
            {
                flag3 = true;
                s = str1;
            }
            else if (mplPeriod == MplPeriod.LoadEncodedFiles)
            {
                List<string> list2 = new List<string>();
                Program.ToWords(str1, 'ﻃ', (ICollection<string>)list2);
            }
            else if (mplPeriod == MplPeriod.Instance)
                Program.readend = Convert.ToBoolean(str1);
          }
        }
        if (flag2)
        {
          if (input.Length - 3 - Convert.ToString(Program.password).Length - s.Length != int.Parse(s))
            flag3 = false;
        }
        else if (input.Length - s.Length - 1 != int.Parse(s))
          flag3 = false;
        if (flag3)
          return;
        if (!Program.master_mode)
        {
          Console.Clear();
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("This file has been hacked or corrupt.");
          File.Delete(path);
        }
        else
        {
          Console.Clear();
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("This file has been hacked or corrupt.");
          string str;
          do
          {
            Console.WriteLine("Would you like to reconstruct the file?");
            str = Console.ReadLine();
            if (str.ToLower() == "yes")
              Program.Save(new StreamWriter(path));
            else
              File.Delete(path);
          }
          while (str.ToLower() != "yes" && str.ToLower() != "no");
          Console.ResetColor();
        }
        Console.ReadKey();
      }
    }
  }