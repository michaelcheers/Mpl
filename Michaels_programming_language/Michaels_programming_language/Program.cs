// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.Program
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Michaels_programming_language
{
  internal static class Program
  {
    public static bool readend = true;
    public static List<KeyValuePair<char, ConsoleColor>> written = new List<KeyValuePair<char, ConsoleColor>>();
    public static Dictionary<string, object> variables = new Dictionary<string, object>();
    public static List<IInstruction> program = new List<IInstruction>();
    public static int password = 0;
    public static List<List<IInstruction>> ifactive = new List<List<IInstruction>>();
    public static int BytesWrittenWithWriteWrite = 0;
    public static string writtenwithwritewrite = "";
    public static string autocorrectdirectory = AppDomain.CurrentDomain.BaseDirectory;
    public static Random rnd = new Random();
    public static List<FileRepresentation> encoded_files = new List<FileRepresentation>();
    public static bool master_mode = false;
    public static bool argsloaded;
    public static OnCrashList OnCrash = new OnCrashList();

    public static string ToString<T>(ICollection<T> input, string separator)
    {
      string str1 = "";
      string str2 = "";
      foreach (T obj in (IEnumerable<T>) input)
      {
        str1 = str1 + (object) str2 + (string) (object) obj;
        str2 = separator;
      }
      return str1;
    }
    
    public static int IntPower(int input, int pow)
    {
        int result = 1;
        for (int n = 0; n < pow; n++)
            result *= input;
        return result;
    }

    public static char[] Add(char args1, char[] args2)
    {
      List<char> list = new List<char>();
      list.Add(args1);
      list.AddRange((IEnumerable<char>) args2);
      return list.ToArray();
    }

    public static string LowerFirstLetter(string input)
    {
      char[] chArray = input.ToCharArray();
      chArray[0] = char.ToLower(chArray[0]);
      return new string(chArray);
    }

    public static void ToWords(string input, char seperator, ICollection<string> result)
    {
      string str = "";
      for (int index = 0; index < input.Length; ++index)
      {
        char ch = input[index];
        if ((int) ch == (int) seperator)
        {
          result.Add(str);
          str = "";
        }
        else
          str = str + (object) ch;
      }
      result.Add(str);
    }

    public static void Write<T>(T input)
    {
      List<string> list = new List<string>();
      Program.ToWords(Convert.ToString((object) input), 'ﻃ', (ICollection<string>) list);
      int index = 0;
      int num = 0;
      bool flag = false;
      for (; index < list.Count; ++index)
      {
        if (Console.WindowWidth - num >= list[index].Length)
        {
          if (flag)
          {
            Console.Write(" " + list[index]);
            num += list[index].Length + 1;
          }
          else
          {
            Console.Write(list[index]);
            flag = true;
            num += list[index].Length;
          }
        }
        else if (flag)
        {
          Console.Write("\n" + list[index]);
          num = list[index].Length;
        }
        else
        {
          Console.Write(list[index]);
          num = list[index].Length;
        }
      }
    }
    internal static void appShortcut(string wherefiletolinktoiskept, string wherelnkwillbekept)
    {
        string deskDir = wherelnkwillbekept;

        using (StreamWriter writer = new StreamWriter(deskDir + "\\" + wherefiletolinktoiskept + ".url"))
        {
            string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
            writer.WriteLine("[InternetShortcut]");
            writer.WriteLine("URL=file:///" + app);
            writer.WriteLine("IconIndex=0");
            string icon = app.Replace('\\', '/');
            writer.WriteLine("IconFile=" + icon);
            writer.Flush();
        }
    }
    public static void Run(IEnumerable<IInstruction> instruction)
    {
      int num = 1;
      foreach (IInstruction instruction1 in instruction)
      {
        try
        {
          instruction1.Run();
        }
        catch (Exception ex)
        {
            if (!auto)
            {
                Exception exception = ex;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                if (exception.Message == "An item with the same key has already been added.")
                    exception = (Exception)new ArgumentException("Cannot declare variable with one name twice.");
                if (exception.Message == "The given key was not present in the dictionary.")
                    exception = (Exception)new ArgumentException("Variable not found.");
                Program.Write<string>(string.Concat(new object[4]
          {
            (object) exception.Message.Replace(".", ""),
            (object) " ocurred on line ",
            (object) num,
            (object) "."
          }));
                Console.ResetColor();
                Console.ReadKey();
                break;
            }
            else
            {
                Console.Clear();
                System.Windows.Forms.MessageBox.Show("This application has crashed.", "Crash Result", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (System.Windows.Forms.MessageBox.Show("Would you like to continue application?", "Crash Result", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.No)
                    break;
            }
        }
        ++num;
      }
    }

    public static void Save(IEnumerable<KeyValuePair<StreamWriter, IInstruction>> input)
    {
      foreach (KeyValuePair<StreamWriter, IInstruction> keyValuePair in input)
        keyValuePair.Value.Save(keyValuePair.Key);
    }

    public static void Save(StreamWriter writer, IEnumerable<IInstruction> instructions)
    {
      foreach (IInstruction instruction in instructions)
        instruction.Save(writer);
    }

    public static IInstruction LoadInstruction(string s)
    {
      if (DeclarationInstruction.TryLoad(s))
        return DeclarationInstruction.Load(s);
      if (GetInputInstruction.TryLoad(s))
        return GetInputInstruction.Load(s);
      if (ReadFileInstruction.TryLoad(s))
        return ReadFileInstruction.Load(s);
      if (SetInstruction.TryLoad(s))
        return SetInstruction.Load(s);
      if (WriteInstruction.TryLoad(s))
        return WriteInstruction.Load(s);
      if (WriteToFileInstruction.TryLoad(s))
        return WriteToFileInstruction.Load(s);
      if (OperatorEquationInstruction.TryLoad(s))
        return OperatorEquationInstruction.Load(s);
      if (PlaySoundInstruction.TryLoad(s))
        return PlaySoundInstruction.Load(s);
      if (BoolOperatorInstruction.TryLoad(s))
        return BoolOperatorInstruction.Load(s);
      if (IfInstruction.TryLoad(s))
        return IfInstruction.Load(s);
      else
        throw new ArgumentException("Please add " + s + " to the load list.");
    }

    public static void WriteWrite(this StreamWriter writer, object value)
    {
      string str = Convert.ToString(value);
      writer.Write(str);
      Program.BytesWrittenWithWriteWrite += str.Length;
      Program.writtenwithwritewrite = Program.writtenwithwritewrite + str;
    }

    public static void Save(StreamWriter writer)
    {
      Program.BytesWrittenWithWriteWrite = 0;
      Program.Save(writer, (IEnumerable<IInstruction>) Program.program);
      Program.WriteWrite(writer, (object) "\n\n");
      foreach (KeyValuePair<char, ConsoleColor> keyValuePair in Program.written)
      {
        Program.WriteWrite(writer, (object) keyValuePair.Key);
        if (((int) keyValuePair.Value).ToString().Length == 1)
          Program.WriteWrite(writer, (object) 0);
        Program.WriteWrite(writer, (object) Convert.ToInt32((object) keyValuePair.Value));
      }
      Program.WriteWrite(writer, (object) "\n\n");
      Program.WriteWrite(writer, (object) Program.readend);
      Program.WriteWrite(writer, (object) "\n\n");
      Program.WriteWrite(writer, (object) Program.BytesWrittenWithWriteWrite);
      Program.WriteWrite(writer, (object) "\n");
      if (Program.password != 0)
        Program.WriteWrite(writer, (object) ("\n\n" + (object) Program.password));
      if (Program.encoded_files.Count != 0)
        Program.WriteWrite(writer, (object) "\n\n");
      foreach (FileRepresentation fileRepresentation in Program.encoded_files)
        Program.WriteWrite(writer, (object) string.Concat(new object[4]
        {
          (object) fileRepresentation.original_path,
          (object) 'ﻃ',
          (object) new BigInteger(fileRepresentation.bytes),
          (object) '\n'
        }));
      ((TextWriter) writer).Flush();
      writer.Close();
    }

    public static void WriteWritten()
    {
      foreach (KeyValuePair<char, ConsoleColor> keyValuePair in Program.written)
      {
        Console.ForegroundColor = keyValuePair.Value;
        Console.Write(keyValuePair.Key);
      }
    }

    public static string PathChange(string path)
    {
      if (!Enumerable.Contains<char>((IEnumerable<char>) path, '\\') && !Enumerable.Contains<char>((IEnumerable<char>) path, '/'))
        path = Path.Combine(Program.autocorrectdirectory, path);
      if (Program.argsloaded)
        path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
      if (path.StartsWith("encoder/"))
        path = Path.GetTempPath() + "/encoder/" + path.Remove(0, 8).Replace('/', '兆').Replace('\\', '兆').Replace(':', '黅');
      return path;
    }

    public static void Test()
    {
      try
      {
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public static void Run()
    {
      Console.Clear();
      Directory.CreateDirectory(Path.GetTempPath() + "/encoder");
      foreach (FileRepresentation fileRepresentation in Program.encoded_files)
        File.WriteAllBytes(Path.GetTempPath() + "/encoder/" + fileRepresentation.original_path.Replace('/', '兆').Replace('\\', '兆').Replace(':', '黅'), fileRepresentation.bytes);
      new DeclarationInstruction("string", typeof (object)).Run();
      Program.Run((IEnumerable<IInstruction>) Program.program);
      if (Program.readend)
        new GetInputInstruction("string").Run();
      Console.Clear();
      int num = 0;
      if (!auto)
      {
          foreach (KeyValuePair<char, ConsoleColor> keyValuePair in Program.written)
          {
              Console.ForegroundColor = keyValuePair.Value;
              Console.Write(keyValuePair.Key);
              ++num;
          }
      }
      Program.variables.Clear();
    }

    private static void WriteLine(string s)
    {
      foreach (char key in s)
        Program.written.Add(new KeyValuePair<char, ConsoleColor>(key, Console.ForegroundColor));
      Program.written.Add(new KeyValuePair<char, ConsoleColor>('\n', Console.ForegroundColor));
      Program.Write<string>(s + (object) '\n');
    }

    private static void WriteLine(string s, ConsoleColor color)
    {
      Console.ForegroundColor = color;
      foreach (char key in s)
        Program.written.Add(new KeyValuePair<char, ConsoleColor>(key, Console.ForegroundColor));
      Program.written.Add(new KeyValuePair<char, ConsoleColor>('\n', Console.ForegroundColor));
      Program.Write<string>(s + (object) '\n');
      Console.ResetColor();
    }

    private static void SetArgs(string[] args, string set)
    {
      args = new string[1]
      {
        set
      };
    }

    private static string ReadLine()
    {
      string str = Console.ReadLine();
      foreach (char key in str)
        Program.written.Add(new KeyValuePair<char, ConsoleColor>(key, Console.ForegroundColor));
      Program.written.Add(new KeyValuePair<char, ConsoleColor>('\n', Console.ForegroundColor));
      return str;
    }

    private static string ReadLineAsDot()
    {
      string str = "";
      while (true)
      {
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
        if (consoleKeyInfo.Key == ConsoleKey.Backspace)
        {
            if ((int)Enumerable.Last<KeyValuePair<char, ConsoleColor>>((IEnumerable<KeyValuePair<char, ConsoleColor>>)Program.written).Key != 10)
            {
                Program.written.RemoveAt(Program.written.Count - 1);
                str.Remove(str.Length - 1);
                Console.Clear();
                Program.WriteWritten();
            }
        }
        else if (consoleKeyInfo.Key != ConsoleKey.Enter)
        {
            Program.written.Add(new KeyValuePair<char, ConsoleColor>('.', Console.ForegroundColor));
            Console.Write('.');
            str = str + (object)consoleKeyInfo.KeyChar;
        }
        else
        {
            WriteLine("");
            break;
        }
      }
      return str;
    }

    public static byte[] ObjectToByteArray(object obj)
    {
      if (obj == null)
        return (byte[]) null;
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      MemoryStream memoryStream = new MemoryStream();
      binaryFormatter.Serialize((Stream) memoryStream, obj);
      return memoryStream.ToArray();
    }

    public static bool SafeSecureEquals<Type>(Type first, Type second)
    {
      Thread.Sleep(Program.rnd.Next(1000));
      return object.Equals(first, second);
    }

    private static IInstruction AskInstruction(string instruction)
    {
      if (instruction == "declare")
      {
        Program.WriteLine("What's the name of your variable?");
        string Name = Program.ReadLine();
        bool flag = false;
        List<string> list = new List<string>();
        if (Name == "*")
        {
          Console.WriteLine("Write the list of variables you would like to declare with spaces in between.");
          Program.ToWords(Program.ReadLine(), ' ', (ICollection<string>) list);
          flag = true;
        }
        if (flag)
          Program.WriteLine("What's the type for all your variables?");
        else
          Program.WriteLine("What type is your variable?");
        string str = Program.ReadLine();
        if (flag)
          Program.WriteLine("What's your value for all the variables?");
        else
          Program.WriteLine("What's your value");
        string s = Program.ReadLine();
        if (str == "int")
        {
          if (flag)
          {
            int result;
            if (int.TryParse(s, out result))
            {
                List<IInstruction> instructions = new List<IInstruction>();
                foreach (string s2 in list)
                    instructions.Add(new DeclarationInstruction(s2, result, false, typeof(int)));
                return new ListInstruction(instructions);
            }
            else
            {
                List<IInstruction> instructions = new List<IInstruction>();
                foreach (string s2 in list)
                    instructions.Add(new DeclarationInstruction(s2, s, true, typeof(int)));
                return new ListInstruction(instructions);
            }
          }
          else
          {
            int result;
            if (int.TryParse(s, out result))
              return (IInstruction) new DeclarationInstruction(Name, (object) result, false, typeof (int));
            else
              return (IInstruction) new DeclarationInstruction(Name, (object) s, true, typeof (int));
          }
        }
        else if (str == "string")
        {
          Program.WriteLine("Would you like to convert as text? False or True.");
          if (Convert.ToBoolean(Program.ReadLine()))
            return (IInstruction) new DeclarationInstruction(Name, (object) s.Replace("\\n", "\n").Replace("\\\\", "\\").Replace("\\r", "\r"), false, typeof (string));
          else
            return (IInstruction) new DeclarationInstruction(Name, (object) s, true, typeof (string));
        }
        else
        {
          if (!(str == "bool"))
            throw new ArgumentException("Type not Supported");
          Program.WriteLine("Would you like to convert as text? False or True.");
          if (Convert.ToBoolean(Program.ReadLine()))
            return (IInstruction) new DeclarationInstruction(Name, (object) Convert.ToBoolean(s), false, typeof (bool));
          else
            return (IInstruction) new DeclarationInstruction(Name, (object) s, true, typeof (bool));
        }
      }
      else if (instruction == "delete")
        Program.program.RemoveAt(Program.program.Count - 1);
      else if (instruction == "encode")
      {
        Program.WriteLine("What's the path of the file you want to encode?");
        string path = Program.ReadLine();
        if (File.Exists(path))
        {
          foreach (FileRepresentation fileRepresentation in Program.encoded_files)
          {
            if (path == fileRepresentation.original_path)
              return (IInstruction) null;
          }
          Program.encoded_files.Add(new FileRepresentation(path));
        }
      }
      else if (instruction == "start oncrash")
      {
          OnCrash.normal = false;
          while (true)
          {
              string input = ReadLine();
              if (input == "end oncrash")
                  break;
              IInstruction output = AskInstruction(input);
              if (output != null)
              {
                  OnCrash.instructions.Add(output);
              }
          }
      }
      else if (instruction == "start if")
      {
        Program.WriteLine("What's the name of the variable for the condition?");
        string variable_name = Program.ReadLine();
        Program.ifactive.Add(new List<IInstruction>());
        while (true)
        {
          Program.WriteLine("What's your instruction?");
          string instruction1 = Program.ReadLine();
          if (!(instruction1 == "end if"))
          {
            IInstruction instruction2 = Program.AskInstruction(instruction1);
            if (instruction2 != null)
            {
                if (instruction2 is ListInstruction)
                {
                    List<IInstruction> list = Enumerable.ToList<IInstruction>((IEnumerable<IInstruction>)Program.ifactive[Program.ifactive.Count - 1]);
                    list.AddRange(((ListInstruction)instruction2).instructions);
                    Program.ifactive[Program.ifactive.Count - 1] = list;
                }
                else
                {
                    List<IInstruction> list = Enumerable.ToList<IInstruction>((IEnumerable<IInstruction>)Program.ifactive[Program.ifactive.Count - 1]);
                    list.Add(instruction2);
                    Program.ifactive[Program.ifactive.Count - 1] = list;
                }
            }
          }
          else
            break;
        }
        List<IInstruction> instructions1 = Enumerable.Last<List<IInstruction>>((IEnumerable<List<IInstruction>>) Program.ifactive);
        Program.ifactive.RemoveAt(Program.ifactive.Count - 1);
        if (Program.ifactive.Count == 0)
          return (IInstruction) new IfInstruction(instructions1, variable_name);
        List<IInstruction> instructions2 = Enumerable.Last<List<IInstruction>>((IEnumerable<List<IInstruction>>) Program.ifactive);
        instructions2.Add((IInstruction) new IfInstruction(instructions2, variable_name));
        Program.ifactive[Program.ifactive.Count - 1] = instructions2;
      }
      else if (instruction == "master")
      {
        Program.WriteLine("What's the password?");
        string s = ReadLineAsDot();
        Console.Clear();
        if (Program.SafeSecureEquals<BigInteger>(BigInteger.Parse(File.ReadAllText("/Private/Password Hub/password.txt")), new BigInteger(HashAlgorithm.Create().ComputeHash(Encoding.Default.GetBytes(s)))))
        {
          Program.master_mode = true;
          Program.WriteWritten();
          bool flag = false;
            if (Directory.Exists("../../../Bugs"))
          foreach (string path in Directory.GetFiles("../../../Bugs"))
          {
            if (!flag)
              Console.Clear();
            Console.WriteLine("The bugs are:");
            Console.WriteLine(File.ReadAllText(path));
            flag = true;
          }
          if (flag)
          {
            Console.ReadKey();
            Console.Clear();
            Program.WriteWritten();
          }
        }
        else
        {
          Console.Beep();
          Console.Write("Incorrect!");
        }
      }
      else if (instruction == "set password")
      {
        Program.WriteLine("What's your password?");
        Program.password = ReadLineAsDot().GetHashCode();
      }
      else if (instruction == "active password")
      {
        Random random = new Random();
        Console.Clear();
        bool flag = true;
        while (flag)
        {
          Console.WriteLine("What's the password?");
          string str = ReadLineAsDot();
          Thread.Sleep(random.Next(1000));
          if (str.GetHashCode() == Program.password)
            flag = false;
        }
        Console.Clear();
        Program.WriteWritten();
      }
      else if (instruction == "play")
      {
        Program.WriteLine("What path is your sound at?");
        string str = Program.ReadLine();
        if (!Enumerable.Contains<char>((IEnumerable<char>) str, '\\') && !Enumerable.Contains<char>((IEnumerable<char>) str, '/'))
          str = Path.Combine(Program.autocorrectdirectory, str);
        if (Program.argsloaded)
          str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, str);
        return (IInstruction) new PlaySoundInstruction(str);
      }
      else if (instruction == "set directory")
      {
        Program.WriteLine("Where is the directory?");
        Program.autocorrectdirectory = Program.ReadLine();
        Program.autocorrectdirectory = Program.PathChange(Program.autocorrectdirectory);
      }
      else if (instruction == "delete mistakes")
      {
        List<string> list = new List<string>();
        foreach (IInstruction instruction1 in Program.program)
          list.Add(instruction1.GetOriginalCode());
        Console.Clear();
        if (!Program.readend)
          Console.Write("What's your instruction?\ninstance\n");
        if (list.Count > 0)
          Console.Write("What's your instruction?\n" + Program.ToString<string>((ICollection<string>) list, "\nWhat's your instruction?\n") + "\n");
        if (Program.password != 0)
          Console.Write("password set\n");
        Console.ResetColor();
        Program.written.Clear();
        if (!Program.readend)
        {
          foreach (char key in "What's your instruction?\ninstance\n")
            Program.written.Add(new KeyValuePair<char, ConsoleColor>(key, Console.ForegroundColor));
        }
        foreach (char key in "What's your instruction?\n" + Program.ToString<string>((ICollection<string>) list, "\nWhat's your instruction?\n") + "\n")
          Program.written.Add(new KeyValuePair<char, ConsoleColor>(key, Console.ForegroundColor));
        if (Program.password != 0)
        {
          foreach (char key in "password set\n")
            Program.written.Add(new KeyValuePair<char, ConsoleColor>(key, Console.ForegroundColor));
        }
      }
      else if (instruction == "list")
      {
        List<string> list = new List<string>();
        foreach (IInstruction instruction1 in Program.program)
          list.Add(instruction1.GetOriginalCode());
        Console.Clear();
        if (list.Count > 0)
          Console.Write("What's your instruction?\n" + Program.ToString<string>((ICollection<string>) list, "\nWhat's your instruction?\n"));
        if (Program.password != 0)
          Console.Write("password set\n");
        Console.ReadKey();
        Console.Clear();
        if (!Program.readend)
          Console.Write("What's your instruction?\ninstance\n");
        foreach (KeyValuePair<char, ConsoleColor> keyValuePair in Program.written)
        {
          Console.ForegroundColor = keyValuePair.Value;
          Console.Write(keyValuePair.Key);
        }
        Console.ResetColor();
      }
      else if (instruction == "delete file")
      {
        Console.WriteLine("Where is the file you would like to delete?");
        string file_name = Program.ReadLine();
        Console.WriteLine("Would you like to convert as text? False or True.");
        bool flag = bool.Parse(Program.ReadLine());
        return (IInstruction) new DeleteFileInstruction(file_name, !flag);
      }
      else if (instruction == "equals")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first string?");
        string variable1 = Program.ReadLine();
        Program.WriteLine("Would you like to convert as text? False or True.");
        bool flag1 = !bool.Parse(Program.ReadLine());
        string str1 = !flag1 ? variable1 : variable1;
        Program.WriteLine("What's your second string?");
        string variable2 = Program.ReadLine();
        Program.WriteLine("Would you like to convert as text? False or True.");
        bool flag2 = !bool.Parse(Program.ReadLine());
        string str2 = !flag2 ? variable2 : variable2;
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new BoolOperatorInstruction(BoolOperator.Equals, (object) false, (object) false, variable, variable1, variable2, true, true);
          else
            return (IInstruction) new BoolOperatorInstruction(BoolOperator.Equals, (object) false, (object) str2, variable, variable1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new BoolOperatorInstruction(BoolOperator.Equals, (object) str1, (object) false, variable, "", variable2, false, true);
        else
          return (IInstruction) new BoolOperatorInstruction(BoolOperator.Equals, (object) str1, (object) str2, variable);
      }
      else if (instruction == "multiply")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new OperatorEquationInstruction(Operator.Multiply, 0, 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new OperatorEquationInstruction(Operator.Multiply, 0, result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new OperatorEquationInstruction(Operator.Multiply, result1, 0, variable, "", str2, false, true);
        else
          return (IInstruction) new OperatorEquationInstruction(Operator.Multiply, result1, result2, variable);
      }
      else if (instruction == "mod")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new OperatorEquationInstruction(Operator.Modulus, 0, 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new OperatorEquationInstruction(Operator.Modulus, 0, result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new OperatorEquationInstruction(Operator.Modulus, result1, 0, variable, "", str2, false, true);
        else
          return (IInstruction) new OperatorEquationInstruction(Operator.Modulus, result1, result2, variable);
      }
      else if (instruction == "divide")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new OperatorEquationInstruction(Operator.Divide, 0, 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new OperatorEquationInstruction(Operator.Divide, 0, result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new OperatorEquationInstruction(Operator.Divide, result1, 0, variable, "", str2, false, true);
        else
          return (IInstruction) new OperatorEquationInstruction(Operator.Divide, result1, result2, variable);
      }
      else if (instruction == "exponents")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new OperatorEquationInstruction(Operator.Exponents, 0, 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new OperatorEquationInstruction(Operator.Exponents, 0, result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new OperatorEquationInstruction(Operator.Exponents, result1, 0, variable, "", str2, false, true);
        else
          return (IInstruction) new OperatorEquationInstruction(Operator.Exponents, result1, result2, variable);
      }
      else if (instruction == "add")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new OperatorEquationInstruction(Operator.Add, 0, 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new OperatorEquationInstruction(Operator.Add, 0, result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new OperatorEquationInstruction(Operator.Add, result1, 0, variable, "", str2, false, true);
        else
          return (IInstruction) new OperatorEquationInstruction(Operator.Add, result1, result2, variable);
      }
      else if (instruction == "greater than")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new BoolOperatorInstruction(BoolOperator.Greater_than, (object) 0, (object) 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new BoolOperatorInstruction(BoolOperator.Greater_than, (object) 0, (object) result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new BoolOperatorInstruction(BoolOperator.Greater_than, (object) result1, (object) 0, variable, "", str2, false, true);
        else
          return (IInstruction) new BoolOperatorInstruction(BoolOperator.Greater_than, (object) result1, (object) result2, variable);
      }
      else if (instruction == "less than")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new BoolOperatorInstruction(BoolOperator.Less_than, (object) 0, (object) 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new BoolOperatorInstruction(BoolOperator.Less_than, (object) 0, (object) result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new BoolOperatorInstruction(BoolOperator.Less_than, (object) result1, (object) 0, variable, "", str2, false, true);
        else
          return (IInstruction) new BoolOperatorInstruction(BoolOperator.Less_than, (object) result1, (object) result2, variable);
      }
      else if (instruction == "subtract")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new OperatorEquationInstruction(Operator.Subtract, 0, 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new OperatorEquationInstruction(Operator.Subtract, 0, result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new OperatorEquationInstruction(Operator.Subtract, result1, 0, variable, "", str2, false, true);
        else
          return (IInstruction) new OperatorEquationInstruction(Operator.Subtract, result1, result2, variable);
      }
      else if (instruction == "percentage")
      {
        Program.WriteLine("What's the name of the variable you want to set the answer to?");
        string variable = Program.ReadLine();
        Program.WriteLine("What's your first number?");
        string str1 = Program.ReadLine();
        int result1;
        bool flag1 = !int.TryParse(str1, out result1);
        Program.WriteLine("What's your second number?");
        string str2 = Program.ReadLine();
        int result2;
        bool flag2 = !int.TryParse(str2, out result2);
        if (flag1)
        {
          if (flag2)
            return (IInstruction) new OperatorEquationInstruction(Operator.Percentage, 0, 0, variable, str1, str2, true, true);
          else
            return (IInstruction) new OperatorEquationInstruction(Operator.Percentage, 0, result2, variable, str1, "", true, false);
        }
        else if (flag2)
          return (IInstruction) new OperatorEquationInstruction(Operator.Percentage, result1, 0, variable, "", str2, false, true);
        else
          return (IInstruction) new OperatorEquationInstruction(Operator.Percentage, result1, result2, variable);
      }
      else if (instruction == "clear")
      {
        Program.written.Clear();
        Console.Clear();
      }
      else if (instruction == "delete number")
      {
        Program.WriteLine("What number instruction do you want to delete?");
        int num = int.Parse(Program.ReadLine());
        Program.program.RemoveAt(num - 1);
      }
      else if (instruction == "getinput")
      {
        Program.WriteLine("What's the name of the variable you want to set the input to. (string means none)");
        return (IInstruction) new GetInputInstruction(Program.ReadLine());
      }
      else if (instruction == "getfile")
      {
        Program.WriteLine("Where is the file?");
        string Path = Program.PathChange(Program.ReadLine());
        Program.WriteLine("What's the name of your variable that you would like to set it to?");
        string Variable_name = Program.ReadLine();
        return (IInstruction) new ReadFileInstruction(Path, Variable_name, false);
      }
      else if (instruction == "write")
      {
        Program.WriteLine("What's your value?");
        string str = Program.ReadLine();
        Program.WriteLine("Would you like to convert as text? False or True.");
        if (Convert.ToBoolean(Program.ReadLine()))
          return (IInstruction) new WriteInstruction((object) str.Replace("\\n", "\n").Replace("\\\\", "\\").Replace("\\r", "\r"), false);
        else
          return (IInstruction) new WriteInstruction((object) str, true);
      }
      else if (instruction == "run")
        Program.Run();
      else if (instruction == "purge")
      {
        Program.program.Clear();
        Program.written.Clear();
        Program.encoded_files.Clear();
        Console.Clear();
      }
      else if (instruction == "instance")
        Program.readend = !Program.readend;
      else if (instruction == "set")
      {
        Program.WriteLine("What's the name of you're variable");
        string Name = Program.ReadLine();
        Program.WriteLine("What's you're value");
        string s = Program.ReadLine();
        object obj = (object) Convert.ToChar(5467);
        bool flag = false;
        foreach (IInstruction instruction1 in Program.program)
        {
          if (instruction1.GetType() == typeof (DeclarationInstruction) && ((DeclarationInstruction) instruction1).name == Name)
          {
            obj = (object) ((DeclarationInstruction) instruction1).orig_type;
            flag = true;
          }
        }
        if (!flag)
          throw new ArgumentException("Variable does not exist.");
        if ((Type) obj == typeof (int))
        {
          int result;
          if (int.TryParse(s, out result))
            return (IInstruction) new SetInstruction(Name, (object) int.Parse(s), false);
          else
            return (IInstruction) new SetInstruction(Name, (object) s, true);
        }
        else if ((Type) obj == typeof (string))
        {
          Program.WriteLine("Would you like to convert as text? False or True.");
          if (Convert.ToBoolean(Program.ReadLine()))
            return (IInstruction) new SetInstruction(Name, (object) s.Replace("\\n", "\n").Replace("\\\\", "\\").Replace("\\r", "\r"), false);
          else
            return (IInstruction) new SetInstruction(Name, (object) s, true);
        }
        else
        {
          if (!((Type) obj == typeof (bool)))
            throw new ArgumentException("Type not supported.");
          Program.WriteLine("Would you like to convert as text? False or True.");
          if (Convert.ToBoolean(Program.ReadLine()))
            return (IInstruction) new SetInstruction(Name, (object) Convert.ToBoolean(s), false);
          else
            return (IInstruction) new SetInstruction(Name, (object) s, true);
        }
      }
      else if (instruction == "help")
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Program.WriteLine("\"add\" adds 2 numbers");
        Program.WriteLine("\"active password\" makes the user type the password");
        Program.WriteLine("\"clear\" clears the screen");
        Program.WriteLine("\"declare\" declares a variable");
        Program.WriteLine("\"declare\" using * as your variable name declares multiple variables");
        Program.WriteLine("\"delete\" deletes the last instruction");
        Program.WriteLine("\"delete mistakes\" deletes all things typed wrongly and all help commands");
        Program.WriteLine("\"delete number\" deletes an instruction");
        Program.WriteLine("\"divide\" multiplys 2 numbers");
        Program.WriteLine("\"end if\" ends an if statement");
        Program.WriteLine("\"exponents\" powers 2 numbers");
        Program.WriteLine("\"equals checks if to values are equal");
        Program.WriteLine("\"getfile\" gets the contents of a file");
        Program.WriteLine("\"getinput\" gets the input of the user");
        Program.WriteLine("\"help\" gives you the instruction list");
        Program.WriteLine("\"instance\" makes the program when run stop at the end");
        Program.WriteLine("\"list\" provides a list of all instructions typed with no mistakesresult +=");
        Program.WriteLine("\"multiply\" multiplys 2 numbers");
        Program.WriteLine("\"percentage\" does a% of b");
        Program.WriteLine("\"play\" plays a sound");
        Program.WriteLine("\"purge\" clears the instruction list and the screen");
        Program.WriteLine("\"remove password\" removes the password");
        Program.WriteLine("\"run\" runs the program");
        Program.WriteLine("\"save\" saves your program to the selcected place");
        Program.WriteLine("\"set\" sets a variable");
        Program.WriteLine("\"set directory\" sets the default directory");
        Program.WriteLine("\"set password\" sets the password");
        Program.WriteLine("\"subtract\" subtracts 2 numbers");
        Program.WriteLine("\"start if\" starts an if statement");
        Program.WriteLine("\"write\" writes to the console");
        Program.WriteLine("\"write file\" writes to a file");
        Console.ResetColor();
      }
      else if (instruction == "save")
      {
        Program.WriteLine("Where do you want to save the file?");
        StreamWriter writer = new StreamWriter(Program.PathChange(Program.ReadLine()));
        Program.Save(writer);
        writer.Close();
      }
      else if (instruction == "write file")
      {
        Program.WriteLine("Where is the file?");
        string path = Program.PathChange(Program.ReadLine());
        Program.WriteLine("What are the contents");
        string contents = Program.ReadLine();
        Program.WriteLine("Would you like to convert as text? False or True.");
        bool flag = bool.Parse(Program.ReadLine());
        return (IInstruction) new WriteToFileInstruction(contents, path, !flag);
      }
      else if (instruction == "load")
      {
        Program.WriteLine("What file path would you like to load?");
        Public.Load(Program.ReadLine());
      }
      else if (instruction == "remove password")
        Program.password = 0;
      else if (Program.master_mode && instruction == "test all")
      {
        Program.master_mode = false;
        List<IInstruction> list1 = Program.program;
        List<KeyValuePair<char, ConsoleColor>> list2 = Program.written;
        bool flag = Program.readend;
        Program.Test();
        try
        {
          Public.Load("../../../bug.mpl");
          Program.Run();
        }
        catch (Exception ex)
        {
          Console.Write(ex.Message);
        }
        Program.master_mode = true;
      }
      else if (instruction == "save exe")
      {
          WriteLine("Type the location of the directory you want to save the exe parts?");
          string path = PathChange(ReadLine());
          if (!Directory.Exists(path))
              Directory.CreateDirectory(path);
          File.Copy(PathChange(AppDomain.CurrentDomain.FriendlyName), path + "/language.exe");
          if (home)
              File.Copy(PathChange("../../../ConsoleApplication1/bin/Release/ConsoleApplication1.exe"), path + "/run.exe");
          else
              File.Copy(PathChange("int.exe"), path + "/run.exe");
          Save(new StreamWriter(path + "/start.mpl"));
      }
      else if (Program.master_mode && instruction == "create abstract")
      {
        Console.WriteLine("Warning: Any unsaved content will be deleted! Type 1 to cancel.");
        Console.WriteLine("Please enter your text for the program?");
        string str1 = ReadLine();
        if (str1 == "1")
          return (IInstruction) null;
        Program.written.Clear();
        WriteLine("Please type number of color. 7 is default.");
        int num1 = 0;
        foreach (string str2 in Enum.GetNames(typeof (ConsoleColor)))
        {
          WriteLine(num1 +  " = " + str2);
          ++num1;
        }
        int num2 = int.Parse(ReadLine());
        WriteLine("Would you like to use current program?");
        if (!(ReadLine().ToLower() == "yes"))
          Program.program.Clear();
        WriteLine("Choose a path.");
        string path = Program.PathChange(ReadLine());
        WriteLine("Choose a password. Type nothing for no password?");
        string str3 = ReadLineAsDot();
        if (str3 != "")
          Program.password = str3.GetHashCode();
        List<KeyValuePair<char, ConsoleColor>> other = written;
        written.Clear();
        foreach (char key in str1)
            Program.written.Add(new KeyValuePair<char, ConsoleColor>(key, (ConsoleColor)num2));
        Program.Save(new StreamWriter(path));
        written.Clear();
        written = other;
      }
      else if (Program.master_mode && instruction == "test")
      {
        Program.WriteLine("What's your instruction?");
        try
        {
          Program.AskInstruction(Program.ReadLine()).Run();
        }
        catch (Exception ex)
        {
          Console.WriteLine("Fail!");
          Console.WriteLine(ex);
          Console.WriteLine("Would you like to exit?");
          if (Program.ReadLine() != "no")
            Environment.Exit(0);
        }
      }
      else if (instruction == "report bug")
      {
        Console.WriteLine("Would you like to save this program as a bug?");
        if (Program.ReadLine() == "yes")
        {
          Program.Save(new StreamWriter("../../../bug.mpl"));
        }
        else
        {
          Console.WriteLine("Please write your error.");
          string contents = Program.ReadLine();
          int num = 1;
          foreach (string fileName in Directory.GetFiles("../../../Bugs"))
          {
            if (num > int.Parse(new FileInfo(fileName).Name.Replace(".txt", "")))
              num = int.Parse(new FileInfo(fileName).Name.Replace(".txt", ""));
          }
          File.WriteAllText("../../../Bugs/" + Convert.ToString(num + 1) + ".txt", contents);
        }
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Program.WriteLine("Invald Instructon! Type \"help\" to find valid instuction!");
        Console.ResetColor();
      }
      return (IInstruction) null;
    }

    internal static bool auto = false;
    internal static bool home = false;
    private static void Main(string[] args)
    {
      if (args != null && args.Length != 0)
      {
          Program.argsloaded = true;
          if (args[0] == "auto")
          {
              string path = args[1];
              auto = true;
              Public.Load(path);
              Run();
              Environment.Exit(0);
          }
          else if (args[0] == "install")
          {

          }
          else
          {
              string path = args[0];
              Public.Load(path);
          }
      }
      else
        Program.argsloaded = false;
      if (File.Exists(PathChange("home.txt")))
          home = true;
      else if (!File.Exists(PathChange("int.exe")))
      {
          System.Net.WebClient web = new System.Net.WebClient();
          web.DownloadFile("http://michael.cheersgames.com/Main/Michael/Games/Mpl/int.exe", PathChange("int.exe"));
      }
      while (true)
      {
        try
        {
          Program.WriteLine("What's your instruction?");
          IInstruction instruction = Program.AskInstruction(Program.ReadLine());
          if (instruction != null)
              if (!(instruction is ListInstruction))
                  Program.program.Add(instruction);
              else
                  foreach (IInstruction ii in ((ListInstruction)instruction).instructions)
                      program.Add(ii);
        }
        catch (Exception ex)
        {
          Exception exception = ex;
          while (exception.InnerException != null)
            exception = exception.InnerException;
          Console.Clear();
          Console.ForegroundColor = ConsoleColor.Red;
          Program.Write<string>(exception.Message);
          Console.ResetColor();
          Console.ReadKey();
          Console.Clear();
          foreach (KeyValuePair<char, ConsoleColor> keyValuePair in Program.written)
          {
            Console.ForegroundColor = keyValuePair.Value;
            Console.Write(keyValuePair.Key);
          }
          Console.ResetColor();
        }
      }
    }
  }
}
