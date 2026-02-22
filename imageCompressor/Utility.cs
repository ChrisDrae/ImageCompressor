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
}