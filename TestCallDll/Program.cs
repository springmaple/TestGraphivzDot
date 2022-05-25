using System.IO;

namespace TestCallDll
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var inputPath = @"C:\Users\wilson\Desktop\test.dot";
            var outputPath = @"C:\Users\wilson\Desktop\output.png";

            var source = File.ReadAllText(inputPath);
            var bytes = Graphviz.RenderImage(source, "dot", "png");
            File.WriteAllBytes(outputPath, bytes);
        }
    }
}