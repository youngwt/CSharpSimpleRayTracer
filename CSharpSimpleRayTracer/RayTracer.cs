using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using CSharpSimpleRayTracer.Models;
using ImageMagick;

namespace CSharpSimpleRayTracer
{
    /// <summary>
    /// Main Methods for the Ray Tracer
    /// </summary>
    public class RayTracer
    {
        /// <summary>
        /// Width of the final image
        /// </summary>
        private int Width { get; }

        /// <summary>
        /// Height of the final image
        /// </summary>
        private int Height { get; }

        /// <summary>
        /// A bitmap frame buffer
        /// </summary>
        private Bitmap FrameBuffer { get; }

        /// <summary>
        /// All our pixel values are added one by one in a string builder
        /// </summary>
        private StringBuilder _imageBufferAsPPMString;

        /// <summary>
        /// A
        /// </summary>
        public IList<I3dObject> SceneObjects { get; set; }

        public RayTracer(int x, int y)
        {
            _imageBufferAsPPMString = new StringBuilder();

            // set pixel type
            _imageBufferAsPPMString.AppendLine("P3");

            // set dimensions
            _imageBufferAsPPMString.AppendLine($"{x} {y}");
            Width = x;
            Height = y;

            // set the maximum value
            _imageBufferAsPPMString.AppendLine("255");

            // initialise the scene with objects
            SceneObjects = new List<I3dObject>();

            FrameBuffer = new Bitmap(x, y);
        }


        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Adds a string representation of a pixel to the frame buffer
        /// </summary>
        /// <param name="pixel"></param>
        [Obsolete("For the time being I will try saving to a frame buffer")]
        public void AddToImageBuffer(string pixel)
        {
            _imageBufferAsPPMString.AppendLine(pixel);
        }

        /// <summary>
        /// This method converts the string frame buffer to a PNG file
        /// and saves the output to disk
        /// </summary>
        /// <param name="name">The file name</param>
        public void SaveImageFromPPM(string name) {

            MagickImage image;

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(_imageBufferAsPPMString);
                writer.Flush();
                stream.Position = 0;

                image = new MagickImage(stream);
            }

            image.Write(name);
        }

        /// <summary>
        /// Saves the frame buffer as an image to disk
        /// </summary>
        /// <param name="filename"></param>
        public void SaveFrameBufferToDisk(string path)
        {
            FrameBuffer.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        /// <summary>
        /// Fills the background with a sky like pattern
        /// </summary>
        public void DrawBackground()
        {
            var lowerBound = new Vec3(-2, -1, -1);
            var dx = new Vec3(4, 0, 0);
            var dy = new Vec3(0, 2, 0);
            var origin = new Vec3(0, 0, 0);

            for (int j = Height - 1; j >= 0; j--)
            {
                for (int i = 0; i < Width; i++)
                {
                    var u = (double)i / (double)Width;
                    var v = (double)j / (double)Height;

                    var currentPointX = Vec3.Add(lowerBound, dx.Scale(u));
                    var currentPointY = Vec3.Add(currentPointX, dy.Scale(v));
                    var ray = new Ray(origin, currentPointY);

                    var colour = ColourSkyByRay(ray);

                    foreach (var obj in SceneObjects)
                    {
                        if (obj is Sphere)
                        {
                            var sphere = obj as Sphere;
                            if (sphere.IsHit(ray) > 0)
                            {
                                colour = ColorFromVec3(sphere.DrawPixel(ray));
                            }
                        }
                    }

                    FrameBuffer.SetPixel(Width - i - 1, Height - j - 1, colour);
                }
            }
        }

        /// <summary>
        /// paints a sky like blue gradient based on the ray's angle
        /// </summary>
        /// <param name="ray">The incoming ray</param>
        /// <returns>A colour value</returns>
        private Color ColourSkyByRay(Ray ray)
        {
            ray.Direction().Normalise();
            var t = 0.5 * ray.Direction().Y + 1;

            var scaledRay = new Vec3(1, 1, 1).Scale(1 - t);

            var sky = Vec3.Add(scaledRay, new Vec3(0.5, 0.7, 1.0).Scale(t));

            return ColorFromVec3(sky);
        }

        private Color ColorFromVec3(Vec3 input)
        {
            var r = (int)(input.X * 255);
            var g = (int)(input.Y * 255);
            var b = (int)(input.Z * 255);

            return Color.FromArgb(255, r, g, b);
        }
    }
}
