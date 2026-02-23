using System.Diagnostics;

namespace imageCompressor;

public static class FfmpegRunner
{
    public static void Run(string arguments)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true, 
                UseShellExecute = false,
                CreateNoWindow =  true
            }
        };
        process.Start();

        string stderr = process.StandardError.ReadToEnd();

        process.WaitForExit();

        Console.WriteLine(stderr);
    }
}