﻿using System;
using CSharpSimpleRayTracer.Models;
using NUnit.Framework;

namespace Tests.ModelFixtures
{
    [TestFixture]
    public class SphereFixture
    {
        [TestCase(2, 2, false)] // ray misses the sphere
        [TestCase(0, 0, true)] // ray hits the sphere
        public void can_detect_sphere(double u, double v, bool expectedResult)
        {
            // Arrange
            var sphereLocation = new Vec3(0, 0, -5);
            var origin = new Vec3(0, 0, 0);
            var pointOfInterest = new Vec3(u, v, -5);

            var ray = new Ray(origin, pointOfInterest);

            //var rayTracer = new RayTracer(100, 100);

            var sphere = new Sphere(sphereLocation, 1, new Vec3(1, 0, 0));

            // Act

            var result = sphere.IsHit(ray);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}