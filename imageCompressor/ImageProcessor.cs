using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace imageCompressor
{
    public class ImageProcessor
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

        public static void MakeText(float[,] matrix)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);

            using StreamWriter outputFile = new(Path.Combine(filePath, "Test.txt"), true);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var character = ASCEncoder.ASCIIEncoding(matrix[j, i]);
                    outputFile.Write(character);
                }
                outputFile.Write("\n");
            }

        }
    }
}
