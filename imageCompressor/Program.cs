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

float[,] imageMatrix = ImageProcessor.ConvertImage(path);

ImageProcessor.RenderImageToText(imageMatrix);

ImageProcessor.BuildGreyImageFromMatrix(imageMatrix);