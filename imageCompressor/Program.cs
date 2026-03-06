using imageCompressor;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

string aspectRatio = "160:90";
string currentDirectory = Directory.GetCurrentDirectory();
string frameDirectory = $"{currentDirectory}/frames";
Directory.CreateDirectory(frameDirectory);

var frameFolder = FfmpegRunner.SpliceVideo(frameDirectory, aspectRatio);

TerminalVideoRunner.RenderFramesToTerminal(frameFolder, true);
////   ------------------------- This are just functions representing different stages of the application that I didn't ant to lose -----------------------------------

#pragma warning disable CS8321 // Local function is declared but never used
static void Test()
{
    var filePath = Utility.PathValidator("image");
    float[,] imageMatrix = ImageProcessor.ConvertImageToGreyMatrix(filePath);
    ImageProcessor.WriteFrameToTerminal(imageMatrix);
}
#pragma warning restore CS8321 // Local function is declared but never used
