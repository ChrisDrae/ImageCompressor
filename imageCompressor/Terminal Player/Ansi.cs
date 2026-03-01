using Microsoft.VisualBasic;
using SixLabors.ImageSharp.PixelFormats;
namespace imageCompressor
{
    public static class Ansi
    {
        public static string Csi => "\e[";
        public static string ShowCursor => Csi + "?25h";
        public static string HideCursor => Csi + "?25l";
        public static string EraseInDisplay => Csi + "2J";
        public static string DisableAlternateBuffer => Csi + "?1049l";
        public static string EnableAlternateBuffer => Csi + "?1049h";
        public static string DisableMouseTracking => Csi + "?1000;1006;1003l";
        public static string EnableMouseTracking => Csi + "?1000;1006;1003h";

        public static string GetAnsiColorCommand(Rgba32 pixel, char character)
        {
            return $"{Csi}38;2;{pixel.R*255};{pixel.G*255};{pixel.B*255}m{character}";
        }
    }
}
