// See https://aka.ms/new-console-template for more informatin
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using imageCompressor;

string inputVideo = "C:/Users/erich/projects/ImageCompressor/media/video/source.mp4";
string outputDirectory = "C:/Users/erich/projects/ImageCompressor/media/frames/frame_%06d.png";

// FfmpegRunner.Run($"-i {inputVideo} {outputDirectory}");

TerminalVideoRunner.RenderToTerminal();

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