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
}