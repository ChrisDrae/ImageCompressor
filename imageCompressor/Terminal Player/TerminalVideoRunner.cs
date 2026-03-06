using System.Diagnostics;
using imageCompressor;

namespace imageCompressor;

public static class TerminalVideoRunner
{
    const double fps = 12.5;
    const double targetFrameTime = 1000.0 / fps;

    public static void RenderTxtToNewTerminal()
    {
        var pathToYourTxtFile = @"c:\Users\erich\Desktop\Test.txt";
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "wt.exe",
                Arguments = $"cmd /k type \"{pathToYourTxtFile}\"",
                UseShellExecute = true,
                CreateNoWindow = false,
            },
        };

        process.Start();
    }

    public static void RenderFramesToTerminal(string frameDirectory, Boolean isColored)
    {
        var stopwatch = new Stopwatch();
        var frameIndex = 1;
        var isRendering = true;
        Console.Write(Ansi.EnableAlternateBuffer);

        while (isRendering)
        {
            stopwatch.Restart();
            Console.Write("\e[H");
            var framePath = $"{frameDirectory}frame_{frameIndex:D6}.png";
            if (!File.Exists(framePath))
            {
                isRendering = false;
            }
            else
            {
                if (isColored)
                {
                    var frameMatrix = ImageProcessor.ConvertImageToRGBMatrix(framePath);

                    ImageProcessor.WriteColoredFrameToTerminal(frameMatrix, frameIndex);
                }
                else
                {
                    var frameMatrix = ImageProcessor.ConvertImageToGreyMatrix(framePath);
                    ImageProcessor.WriteFrameToTerminal(frameMatrix);
                }
            }
            frameIndex += 1;
            stopwatch.Stop();
            var sleep = targetFrameTime - stopwatch.Elapsed.TotalMilliseconds;

            if (sleep > 0)
            {
                Thread.Sleep((int)sleep);
            }
        }
    }
}
