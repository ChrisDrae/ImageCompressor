using System.Diagnostics;
using imageCompressor;
using NAudio;
using NAudio.Wave;

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

    public static WaveOutEvent PlayAudio(string path)
    {
        Console.Write(path);
        var audioFile = new AudioFileReader(path);
        var outputDevice = new WaveOutEvent();

        outputDevice.Init(audioFile);
        outputDevice.Play();

        return outputDevice;
    }

    public static void RenderFramesToTerminal(string frameDirectory, Boolean isColored)
    {
        var wasPaused = false;
        var stopwatch = new Stopwatch();
        var frameIndex = 1;
        var isRendering = true;
        Console.Write(Ansi.EnableAlternateBuffer);
        Console.Write(Ansi.DisableMouseTracking);
        Console.Write(Ansi.HideCursor);
        var audioPlayer = PlayAudio(AppState.AudioFilePath);

        while (isRendering)
        {
            if (AppState.Paused)
            {
                if (!wasPaused)
                {
                    audioPlayer.Pause();
                    wasPaused = true;
                }

                Thread.Sleep(50);
                continue;
            }
            else if (wasPaused)
            {
                audioPlayer.Play();
                wasPaused = false;
            }

            audioPlayer.Play();
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
