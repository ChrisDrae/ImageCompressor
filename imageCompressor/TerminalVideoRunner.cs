using System.Diagnostics;
using Microsoft.VisualBasic;

namespace imageCompressor;

public static class TerminalVideoRunner
{
    public static void RenderToTerminal()
    {   
        var pathToYourTxtFile =  @"c:\Users\erich\Desktop\Test.txt";
        var process = new Process
        {
            StartInfo =  new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k type \"{pathToYourTxtFile}\"",
                UseShellExecute = true,
                CreateNoWindow =  false
            }
        };

        process.Start();
    }
}