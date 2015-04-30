// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.FileRepresentation
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Michaels_programming_language
{
  internal class FileRepresentation
  {
    public string original_path;
    public byte[] bytes;

    public FileRepresentation(string path)
    {
      path = Program.PathChange(path);
      this.original_path = path;
      this.bytes = File.ReadAllBytes(path);
    }

    public FileRepresentation(string path, byte[] bytes)
    {
      if (!Enumerable.Contains<char>((IEnumerable<char>) path, '\\') && Enumerable.Contains<char>((IEnumerable<char>) path, '/'))
        path = Path.Combine(Program.autocorrectdirectory, path);
      if (Program.argsloaded)
        path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
      this.original_path = path;
      this.bytes = bytes;
    }
  }
}
