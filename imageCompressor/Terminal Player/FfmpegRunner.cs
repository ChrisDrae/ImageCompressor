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
    public static string SpliceVideo(string path, string aspectRatio)
    {
        var videoPath = Utility.PathValidator("video");
        var videoName = Path.GetFileName(videoPath);
        var frameFolder = $"{path}/{videoName}";

        Directory.CreateDirectory(frameFolder);
        var framePath = $"{frameFolder}/frame_%06d.png";
        Run($"-i {videoPath} -vf scale={aspectRatio} {framePath}");
        return $"{frameFolder}/";
    }
}