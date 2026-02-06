// See https://aka.ms/new-console-template for more informatin
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using imageCompressor;

ASCEncoder.ASCIIEncoding(222.4324f);
Console.WriteLine("Enter path to image:");
string? path = Console.ReadLine();

if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
{
    Console.WriteLine("Invalid file path");
    return;
}

float[,] local_matrix = ImageProcessor.ConvertImage(path);

ImageProcessor.MakeText(local_matrix);

ImageProcessor.BuildGreyImageFromMatrix(local_matrix);
