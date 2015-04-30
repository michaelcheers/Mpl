using System;
using System.IO;
namespace Michaels_programming_language
{
    internal class Installer
    {
        public Installer() { }
        public void Install ()
        {
                Directory.CreateDirectory("C:/Users/" + Environment.UserName + "/AppData/Mpl/1/1/Programs");
                Directory.CreateDirectory("C:/Users/" + Environment.UserName + "/AppData/Mpl/1/1/1");
                Directory.CreateDirectory("C:/Users/" + Environment.UserName + "/AppData/Mpl/ConsoleApplication1/bin/Release");
                System.Net.WebClient web = new System.Net.WebClient();
                web.DownloadFile("http://michael.cheersgames.com/Main/Michael/Games/Mpl/Michaels_programming_language.application", "C:/Users/" + Environment.UserName + "/AppData/Mpl/1/1/1/1.application");
                web.DownloadFile("http://michael.cheersgames.com/Main/Michael/Games/Mpl/in.exe", "C:/Users/" + Environment.UserName + "/AppData/Mpl/ConsoleApplication1/bin/Release/ConsoleApplication1.exe");
                System.Diagnostics.Process.Start("C:/Users/" + Environment.UserName + "/AppData/Mpl/1/1/1/1.application");
        }
    }
}