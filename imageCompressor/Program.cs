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

float[,] local_matrix = ImageProcessor.ConvertImage(path);

var length = local_matrix.GetLength(0);
var width = local_matrix.GetLength(1);

ImageProcessor.BuildGreyImageFromMatrix(local_matrix);
