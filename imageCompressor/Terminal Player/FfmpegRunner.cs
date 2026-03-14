using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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
                CreateNoWindow = true,
            },
        };
        process.Start();

        string stderr = process.StandardError.ReadToEnd();

        process.WaitForExit();

        Console.WriteLine(stderr);
    }

    public static string SpliceVideo(string path, string aspectRatio)
    {
        var videoPath = Utility.PathValidator("video");
        var videoName = Path.GetFileName(videoPath);
        var frameFolder = $"{path}/{videoName}";

        if (Directory.Exists(frameFolder))
        {
            var frameCheck = $"{frameFolder}/frame_000001.png";
            var desiredHeight = int.Parse(aspectRatio.Split(':')[1]);
            if (Image.Load<Rgba32>(frameCheck).Height == desiredHeight)
                return $"{frameFolder}/";
        }
        Directory.CreateDirectory(frameFolder);
        var framePath = $"{frameFolder}/frame_%06d.png";
        Run($"-i {videoPath} -vf scale={aspectRatio} {framePath}");
        return $"{frameFolder}/";
    }
}
