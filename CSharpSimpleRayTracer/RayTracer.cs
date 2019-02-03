using System;
using System.IO;
using System.Text;
using ImageMagick;

namespace CSharpSimpleRayTracer
{
    public class RayTracer
    {
        private StringBuilder _imageBufferAsPPMString;

        public RayTracer(int x, int y)
        {
            _imageBufferAsPPMString = new StringBuilder();

            // set pixel type
            _imageBufferAsPPMString.AppendLine("P3");

            // set dimensions
            _imageBufferAsPPMString.AppendLine($"{x} {y}");

            // set the maximum value
            _imageBufferAsPPMString.AppendLine("255");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public void AddToImageBuffer(string pixel)
        {
            _imageBufferAsPPMString.AppendLine(pixel);
        }

        public void SaveImageFromPPM(string name) {

            MagickImage image;

            using(var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(_imageBufferAsPPMString);
                writer.Flush();
                stream.Position = 0;

                image = new MagickImage(stream);
            }

            image.Write(name);
        }
    }
}
