# CSharpSimpleRayTracer

A Simple Ray tracer written in C# based on the book Ray Tracing in one Weekend by Peter Shirly. 

I am writing this ray tracer for my own enjoyment, choosing to write it in C# as that's the language I use in my day job (and I want an excuse to use it on a mac at home). The goal for this project is to generate some nice looking pictures, I prefer simple code to optimised code and am not concerned with having a slow renderer.

I am a big fan of Test Driven Development so will endeavour to write tests first and only commit where all tests pass. In Peter's book he suggests outputting images in PPM format where each pixel is described in ASCII text. I will follow suit internally but will use the ImageMagik library to convert the output to a PNG at the last moment.

## Chapter 1

The first few commits are just about getting the project set up. Visual Studio for Mac didn't auto detect tests written in nUnit so I created a project in xUnit and removed all references to xUnit after pulling down nUnit from nuget. I then made sure I could export PPM files as PNG by writing a test that generates a gradient image as in Peter's book:

![gradient image](Tests/Resources/can_save_ppm_as_png_expected_output.png)

## Chapter 2

Now I have introduced some classes to do basic maths. I don't like the approach in Peter's book of having a generic Vec3 class for representing points, colours etc so I have put all the functionality in an abstract class with appropriate concrete implementations for type safety.