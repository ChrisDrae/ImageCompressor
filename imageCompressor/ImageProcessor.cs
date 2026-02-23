using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Text;

namespace imageCompressor;


public class ImageProcessor
{
    public static readonly string asciiSpace = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";
    public static readonly string reversedAscii = ReverseString(asciiSpace);

    public static string ReverseString(string s)
    {
        char[] array = s.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }

    public static float[,] ConvertImage(string path)
    {
        using var image = Image.Load<Rgba32>(path);
        // USE CASE ASCII ART
        //Resize because character boxes are 1x2 Width/Height//
        //

        image.Mutate(x => x.Resize(image.Width, image.Height / 2));

        int width = image.Width;
        int height = image.Height;

        var matrix = new float[width, height];

        for (int i = 0; i < height; i++)
        {
            Span<Rgba32> row = image.DangerousGetPixelRowMemory(i).Span;

            for (int j = 0; j < width; j++)
            {
                Rgba32 pixel = row[j];
                float gray = 0.299f * pixel.R + 0.587f * pixel.G + 0.114f * pixel.B;
                matrix[j, i] = gray;
            }
        }
        return matrix;
    }

    public static void BuildGreyImageFromMatrix(float[,] matrix)
    {
        var width = matrix.GetLength(1);
        var height = matrix.GetLength(0);

        byte[] rawPixelData = new byte[height * width];

        var i = 0;
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                float grayValue = matrix[x, y];
                byte grayValueByte = (byte)(grayValue > 256 ? 255 : grayValue < 0 ? 0 : grayValue);
                rawPixelData[i++] = grayValueByte;
            }
        }

        var image = Image.LoadPixelData<L8>(rawPixelData, height, width);
        image.Save("result.png");
    }

    public static void RenderImageToText(float[,] matrix)
    {
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var height = matrix.GetLength(1);
        var width = matrix.GetLength(0);

        using StreamWriter outputFile = new(Path.Combine(filePath, "Test.txt"), false);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var character = ASCEncoder.ASCIIEncoding(matrix[x, y], reversedAscii);
                outputFile.Write(character);
            }
            outputFile.Write("\r\n");
        }
    }

    public static void WriteFrameToTerminal(float[,] matrix)
    {   
        var height = matrix.GetLength(1);
        var width = matrix.GetLength(0);

        var stringBuilder = new StringBuilder();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var character = ASCEncoder.ASCIIEncoding(matrix[x, y], reversedAscii);
                stringBuilder.Append(character);
            }
            stringBuilder.Append("\r\n");   
        }
        Console.Write(stringBuilder.ToString());
        stringBuilder.Clear();
        Console.Write("\u001b[H");
    }
}
