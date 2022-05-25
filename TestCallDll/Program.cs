using System;
using System.IO;

namespace TestCallDll
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var inputPath = @"C:\Users\wilson\Desktop\test.dot";
            var outputPath = @"C:\Users\wilson\Desktop\output.png";

            try
            {
                var bytes = GraphvizWrapper.RenderImage(inputPath, "dot", "png");
                File.WriteAllBytes(outputPath, bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}