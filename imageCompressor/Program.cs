using imageCompressor;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

//Register Programflow functions and event listeners
AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
Console.CancelKeyPress += OnCancelKeyPress;
var keyListener = new Thread(KeyWatcher) { IsBackground = true };
keyListener.Start();

string aspectRatio = "320:180";
string currentDirectory = Directory.GetCurrentDirectory();
string frameDirectory = $"{currentDirectory}/frames";
Directory.CreateDirectory(frameDirectory);

var frameFolder = FfmpegRunner.SpliceVideo(frameDirectory, aspectRatio);

TerminalVideoRunner.RenderFramesToTerminal(frameFolder, true);

// Functions for Program Flow :D
static void OnProcessExit(object? sender, EventArgs e)
{
    Console.WriteLine("Program is exiting — running cleanup pipeline...");
    Console.Write(Ansi.EnableMouseTracking);
    Console.Write(Ansi.ShowCursor);
}

static void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
{
    Console.WriteLine("OnCancelKeyPress was executed");
    e.Cancel = true;
    Environment.Exit(0);
}

static void KeyWatcher()
{
    while (true)
    {
        var key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.Spacebar)
        {
            AppState.Paused = !AppState.Paused;
        }
    }
}
////   ------------------------- This are just functions representing different stages of the application that I didn't ant to lose -----------------------------------
