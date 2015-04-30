// Decompiled with JetBrains decompiler
// Type: Michaels_programming_language.PlaySoundInstruction
// Assembly: Michaels_programming_language, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1BCE1E7-81A7-4478-970F-E84E6EEBF7C3
// Assembly location: E:\Michaels_programming_language.exe

using System.Collections.Generic;
using System.IO;
using System.Media;

namespace Michaels_programming_language
{
  internal class PlaySoundInstruction : IInstruction
  {
    private SoundPlayer player;

    public PlaySoundInstruction(string path)
    {
      this.player = new SoundPlayer(Program.PathChange(path));
      if (path.StartsWith("encoder/"))
        return;
      this.player.LoadAsync();
    }

    public PlaySoundInstruction()
    {
    }

    public void Run()
    {
      this.player.PlaySync();
    }

    public static IInstruction Load(string line)
    {
      PlaySoundInstruction soundInstruction = new PlaySoundInstruction();
      List<string> list = new List<string>();
      Program.ToWords(line, 'ﻃ', (ICollection<string>) list);
      soundInstruction.player = new SoundPlayer(list[1]);
      return (IInstruction) soundInstruction;
    }

    public static bool TryLoad(string line)
    {
      if (!(line.Substring(0, 4) == "play"))
        return false;
      PlaySoundInstruction.Load(line);
      return true;
    }

    public string GetOriginalCode()
    {
      return "" + "play\nWhat path is your sound at?\n" + this.player.SoundLocation;
    }

    public void Save(StreamWriter writer)
    {
      Program.WriteWrite(writer, (object) "playﻃ");
      Program.WriteWrite(writer, (object) (this.player.SoundLocation + "\n"));
    }
  }
}
