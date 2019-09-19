using System;
using System.IO;
using System.Reflection;
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

        [TearDown]
        public void Teardown()
        {
            // remove all images created during the tests
            string currentDirectory = TestContext.CurrentContext.TestDirectory; //Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var files = Directory.GetFiles(currentDirectory, "*.png", SearchOption.TopDirectoryOnly);

            foreach(var file in files)
            {
                File.Delete(file);
            }
        }

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
        public void Can_Draw_Background()
        {
            // Arrange
            var rayTracer = new RayTracer(200, 100);

            // Act

            rayTracer.DrawBackground();

            var fileName = "Can_Draw_Background.png";
            rayTracer.SaveFrameBufferToDisk(TestContext.CurrentContext.WorkDirectory+"/"+fileName);

            // Assert
            Assert.That(VerifyImage(fileName), Is.True);
        }

        [Test]
        public void Can_Draw_sphere_on_background()
        {
            // Arrange
            var rayTracer = new RayTracer(200, 100);

            var sphere = new Sphere(new Vec3(0, 0, -1), 0.5, new Vec3(1, 0, 0));

            rayTracer.SceneObjects.Add(sphere);

            // Act
            rayTracer.DrawBackground();            
            var fileName = $"{TestContext.CurrentContext.Test.MethodName}.png";
            rayTracer.SaveFrameBufferToDisk(TestContext.CurrentContext.WorkDirectory + "/" + fileName);

            // Assert
            Assert.That(VerifyImage(fileName), Is.True);
        }

        [Test]
        [Ignore("There is a known issue with z sorting - see comments in chapter 5 section in readme.md")] 
        public void Can_Draw_2_spheres_on_background()
        {
            // Arrange
            var rayTracer = new RayTracer(200, 100);

            var sphere = new Sphere(new Vec3(0, 0, -1), 0.5, new Vec3(1, 0, 0));
            var sphere2 = new Sphere(new Vec3(0, -100.5, -1), 100, new Vec3(0, 1, 0));

            rayTracer.SceneObjects.Add(sphere);
            rayTracer.SceneObjects.Add(sphere2);

            // Act
            rayTracer.DrawBackground();
            var fileName = $"{TestContext.CurrentContext.Test.MethodName}.png";
            rayTracer.SaveFrameBufferToDisk(TestContext.CurrentContext.WorkDirectory + "/" + fileName);

            // Assert
            Assert.That(VerifyImage(fileName), Is.True);
        }

        /// <summary>
        /// Compares 2 images and returns true if they are the same
        /// </summary>
        /// <returns><c>true</c>, if image was verifyed, <c>false</c> otherwise.</returns>
        private bool VerifyImage(string fileName)
        {
            var dir = TestContext.CurrentContext.TestDirectory;
            MagickImage expectedImage;
            using (var expectedOutput = new FileStream($"{dir}../../../../Resources/{fileName}", FileMode.Open))
            {
                expectedImage = new MagickImage(expectedOutput);
            }


            MagickImage resultingImage;
            using (var resultAsStream = new FileStream($"{TestContext.CurrentContext.WorkDirectory}/{fileName}", FileMode.Open))
            {
                resultingImage = new MagickImage(resultAsStream);
            }

            return Math.Abs(resultingImage.Compare(expectedImage, ErrorMetric.Absolute)) < 0.1;
        }
    }
}
