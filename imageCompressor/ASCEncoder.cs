using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace imageCompressor;

public class ASCEncoder {
    public static char ASCIIEncoding(float input)
    {
        var asciiSpace = "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\"^`'. ";
        var spacing = 3.64;
        var index = ((int)(input/spacing));
        return asciiSpace[index];
    }
}
