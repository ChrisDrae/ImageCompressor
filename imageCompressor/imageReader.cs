using System.Security.Cryptography.X509Certificates;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace imageCompressor
{
    public class ImageReader
    {
        public static float[,] ConvertImage(string path)
        {
            using var image = Image.Load<Rgba32>(path);

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

        // public static Image<Rgba32> BuildImageFromMatrix(float[,] matrix)
        // {
        //     var height = matrix.GetLength(0);
        //     var width = matrix.GetLength(1);

        //     var image = new Image<Rgba32>(width, height);

        //     for (int y = 0; y < height; y++)
        //     {
        //         Span<Rgba32> row = image.DangerousGetPixelRowMemory(y).Span;

        //         for (int x = 0; x < width; x++)
        //         {
        //             byte intensity = (byte)matrix[y, x];
        //             row[x] = new L8(intensity).ToRgba32;
        //         }
        //     }
        // }
    }
}
