// See https://aka.ms/new-console-template for more informatin
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Runtime.Versioning;
using imageCompressor;
using SixLabors.ImageSharp;

string inputVideo = "E:/ProgramAccessStorage/media/video/source.mp4";
string outputDirectory = "E:/ProgramAccessStorage/media/frames/frame_%06d.png";

string currentDirectory = Directory.GetCurrentDirectory();
string frameDirectory = $"{currentDirectory}/frames";
Directory.CreateDirectory(frameDirectory);

var frameFolder = TerminalVideoRunner.SpliceVideo(frameDirectory);

TerminalVideoRunner.RenderFramesToTerminal(frameFolder);
////   ------------------------- This are just functions representing different stages of the application that I didn't ant to lose -----------------------------------

#pragma warning disable CS8321 // Local function is declared but never used
static void Test()
{
    var filePath = Utility.PathValidator("image");
    float[,] imageMatrix = ImageProcessor.ConvertImage(filePath);
    ImageProcessor.WriteFrameToTerminal(imageMatrix);
}
#pragma warning restore CS8321 // Local function is declared but never used
