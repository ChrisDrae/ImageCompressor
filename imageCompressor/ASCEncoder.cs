using System.Reflection.Metadata.Ecma335;
using System.Text;
using SixLabors.ImageSharp;

namespace imageCompressor;

public class ASCEncoder {
    public static char ASCIIEncoding(float input)
    {
        var asciiSpace = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";
       
        var spacing = 4;
        var index = ((int)(input/spacing));
        return asciiSpace[index];
    }
    static string ReverseString(string s)
    {
        char[] array = s.ToCharArray();
        Array.Reverse(array);
        return new string(array);
        
    }
}
