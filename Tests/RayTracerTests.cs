using System;
using System.IO;
using System.Text;
using CSharpSimpleRayTracer;
using CSharpSimpleRayTracer.Models;
using ImageMagick;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RayTracerTests
    {
        [Test]
        public void Test_project_is_using_nUnit_correctly()
        {
            Assert.True(true);
        }

        [Test]
        public void TestRunner_can_run_main_method()
        {
            //var rayTracer = new RayTracer(100, 100);
            RayTracer.Main(new string[0]);
        }

        [Test]
        public void Can_save_ppm_as_png()
        {
            // Arrange

            // use a string builder to build a 2d image of a gradient

            var nx = 200;
            var ny = 100;

            var rayTracer = new RayTracer(nx, ny);

            for (var j = ny - 1; j >= 0; j--)
            {
                for (var i = 0; i < nx; i++)
                {

                    var col = new Vec3(
                        (float)i / (float)nx,
                        (float)j / (float)ny,
                        51);

                    var ir = Math.Round(255.99 * col.X);
                    var ig = Math.Round(255.99 * col.Y);

                    rayTracer.AddToImageBuffer($"{ir} {ig} {col.Z}");
                }
            }

            // Act
            var filename = "test_ppm_as_png.png";
            rayTracer.SaveImageFromPPM(filename);

            // Assert
            Assert.That(VerifyImage("can_save_ppm_as_png_expected_output.png", filename), Is.True);
        }

        [Test]
        public void Can_Draw_Background()
        {
            // Arrange
            var nx = 200;
            var ny = 100;
            var rayTracer = new RayTracer(nx, ny);

            var lowerBound = new Vec3(-2, -1, -1);
            var dx = new Vec3(4, 0, 0);
            var dy = new Vec3(0, 2, 0);
            var origin = new Vec3(0, 0, 0);

            // Act

            for (int j = ny - 1; j >= 0; j--)
            {
                for (int i = 0; i < nx; i++)
                {
                    var u = (double)i / (double)nx;
                    var v = (double)j / (double)ny;

                    var currentPoint = lowerBound.Add(dx.Scale(u).Add(dy.Scale(v)));
                    var ray = new Ray(origin, currentPoint);

                    var colour = ColourByRay(ray);
                    var r = (int)(colour.X * 255);
                    var g = (int)(colour.Y * 255);
                    var b = (int)(colour.Z * 255);

                    rayTracer.AddToImageBuffer($"{r} {g} {b}");
                }
            }

            var fileName = "Can_Draw_Background.png";
            rayTracer.SaveImageFromPPM(fileName);

            // Assert
            Assert.That(VerifyImage(fileName, fileName), Is.True);
        }

        private Vec3 ColourByRay(Ray r)
        {
            r.Direction().Normalise();
            var t = 0.5 * r.Direction().Y + 1;

            return new Vec3(1, 1, 1).Scale(1 - t).Add(new Vec3(0.5, 0.7, 1.0).Scale(t));
        }

        /// <summary>
        /// Compares 2 images and returns true if they are the same
        /// </summary>
        /// <returns><c>true</c>, if image was verifyed, <c>false</c> otherwise.</returns>
        /// <param name="expectedFileName">Expected file name.</param>
        /// <param name="resultFileName">Result file name.</param>
        private bool VerifyImage(string expectedFileName, string resultFileName)
        {
            var dir = TestContext.CurrentContext.TestDirectory;
            MagickImage expectedImage;
            using (var expectedOutput = new FileStream($"{dir}../../../../Resources/{expectedFileName}", FileMode.Open))
            {
                expectedImage = new MagickImage(expectedOutput);
            }


            MagickImage resultingImage;
            using (var resultAsStream = new FileStream($"{TestContext.CurrentContext.WorkDirectory}/{resultFileName}", FileMode.Open))
            {
                resultingImage = new MagickImage(resultAsStream);
            }

            return Math.Abs(resultingImage.Compare(expectedImage, ErrorMetric.Absolute)) < 0.1;
        }
    }
}
