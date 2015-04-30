// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.IInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System.IO;

namespace Michaels_programming_language
{
  internal interface IInstruction
  {
    void Run();

    //string GenerateCSharpCode();

    void Save(StreamWriter writer);

    string GetOriginalCode();
  }
}
