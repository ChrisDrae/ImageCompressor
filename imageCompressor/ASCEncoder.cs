using System.Reflection.Metadata.Ecma335;
using System.Text;
using SixLabors.ImageSharp;

namespace imageCompressor;

public class ASCEncoder {
    public static char ASCIIEncoding(float input, string encodingImage)
    {
        var spacing = 3.6;
        var index = ((int)(input/spacing));
        if( index > 70 ) index -= 2;
        if( index > 69 ) index--;
        return encodingImage[index];
    }
}
