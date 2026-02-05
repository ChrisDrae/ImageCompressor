// See https://aka.ms/new-console-template for more informatin
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using imageCompressor;

Console.WriteLine("Enter path to image:");
string? path = Console.ReadLine();

if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
{
    Console.WriteLine("Invalid file path");
    return;
}

float[,] local_matrix = ImageReader.ConvertImage(path);

var length = local_matrix.GetLength(0);
var width = local_matrix.GetLength(1);

for (int i = 0; i < length; i++)
{
    for (int j = 0; j < width; j++)
    {
        Console.Write(float.Round(local_matrix[i, j]) + ",");
    }
}
