using System.Diagnostics;
using SixLabors.ImageSharp.PixelFormats;

namespace imageCompressor;

public static class Utility
{
    public static string PathValidator(string file)
    {
        var hasValidPath = false;
        string? path;
        while (!hasValidPath)
        {
            Console.WriteLine($"Enter path to {file}:");
            path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                Console.WriteLine("Invalid file path");
            }
            else
            {
                return path;
            }
        }
        return "";
    }

    public static float PixelToGrey(Rgba32 pixel)
    {
        return 0.299f * pixel.R + 0.587f * pixel.G + 0.114f * pixel.B;
    }

    public static void Logger(string? log, bool start)
    {
        var loggerWatch = new Stopwatch();

        if (start)
        {
            loggerWatch.Restart();
        }
        else
        {
            loggerWatch.Stop();
            string docPath = Environment.CurrentDirectory;
            using StreamWriter outputFile = new(
                Path.Combine(docPath, "PerformanceLog.txt"),
                append: true
            );
            string performanceReport =
                $"Frame {log} took {loggerWatch.Elapsed.TotalMilliseconds} to be written to the terminal.";
            outputFile.WriteLine(performanceReport + "\n");
        }
    }
}
