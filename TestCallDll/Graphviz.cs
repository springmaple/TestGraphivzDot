using System.Diagnostics;
using System.IO;

namespace TestCallDll
{
    public static class Graphviz
    {
        public const string DOT_EXE = @"dot.exe";

        public static byte[] RenderImage(string sourceFile, string layout, string format)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = DOT_EXE,
                Arguments = $@"-T{format} -K{layout} ""{sourceFile}""",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var process = Process.Start(processStartInfo);
            var baseStream = process.StandardOutput.BaseStream as FileStream;
            var lastRead = 0;

            using (var ms = new MemoryStream())
            {
                var buffer = new byte[4096];
                do
                {
                    lastRead = baseStream.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, lastRead);
                } while (lastRead > 0);

                return ms.ToArray();
            }
        }
    }
}