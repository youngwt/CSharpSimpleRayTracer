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
        /// A
        /// </summary>
        public IList<I3dObject> SceneObjects { get; set; }

        /// <summary>
        /// The frame to render to
        /// </summary>
        public FrameModel Frame { get; }

        public RayTracer(int x, int y)
        {

            Frame = new FrameModel(x, y);

            // initialise the scene with objects
            SceneObjects = new List<I3dObject>();
        }


        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Renders the scene by calculating the colour at each pixel
        /// </summary>
        public void RenderScene()
        {
            for (int j = Frame.Height - 1; j >= 0; j--)
            {
                for (int i = 0; i < Frame.Width; i++)
                {
                    var colour = RenderPixel(i, j);

                    Frame.FrameBuffer.SetPixel(Frame.Width - i - 1, Frame.Height - j - 1, colour);
                }
            }
        }

        /// <summary>
        /// Calculates the colour of the pixel
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <returns>A colour or the pixel</returns>
        public Color RenderPixel(int x, int y)
        {
            var ray = GetRayToPixel(x, y);

            // first draw the background in case the ray does not hit anything
            var colour = ColourSkyByRay(ray);

            foreach (var obj in SceneObjects)
            {
                // t is the distance value down the ray at the point
                // currently being drawn
                double min_t = double.MaxValue;

                if (obj is Sphere)
                {
                    var sphere = obj as Sphere;

                    var current_t = sphere.RayToPointParameter(ray, min_t);

                    if (current_t <= min_t && current_t > 0)
                    {
                        min_t = current_t;
                        colour = ColorFromVec3(sphere.DrawPixel(ray, current_t));
                    }
                }
            }

            return colour;
        }

        /// <summary>
        /// Calculates the ray to a given pixel location on the frame
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <returns>A ray from the origin that points to the XY coordinate</returns>
        private Ray GetRayToPixel(int x, int y)
        {
            var origin = new Vec3(0, 0, 0);
            var uvCoords = GetUVCoordinatesFromXY(x, y);

            var currentPointX = Vec3.Add(Frame.LowerBound, Frame.dx.Scale(uvCoords.u));
            var currentPointY = Vec3.Add(currentPointX, Frame.dy.Scale(uvCoords.v));
            return new Ray(origin, currentPointY);
        }

        private (double u, double v) GetUVCoordinatesFromXY(int x, int y)
        {
            var u = (double)x / (double)Frame.Width;
            var v = (double)y / (double)Frame.Height;

            return (u, v);
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
