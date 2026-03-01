using System.Reflection.Metadata.Ecma335;
using System.Text;
using SixLabors.ImageSharp;

namespace imageCompressor;

public class ASCEncoder {
    public static char ASCIIEncoding(float greyvalue, string encodingImage)
    {
        var spacing = 3.6;
        var index = greyvalue/spacing;
        if(index > 69 ) index = 69;
        return encodingImage[(int)index];
    }
}
