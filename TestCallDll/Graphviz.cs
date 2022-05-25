using System;
using System.Runtime.InteropServices;

namespace TestCallDll
{
    public static class Graphviz
    {
        public const string LIB_GVC = @"GraphvizDot\gvc.dll";
        public const string LIB_GRAPH = @"GraphvizDot\cgraph.dll";
        public const int SUCCESS = 0;

        [DllImport(LIB_GVC)]
        public static extern IntPtr gvContext();

        [DllImport(LIB_GVC)]
        public static extern int gvFreeContext(IntPtr gvc);

        [DllImport(LIB_GRAPH)]
        public static extern IntPtr agmemread(string data);

        [DllImport(LIB_GRAPH)]
        public static extern void agclose(IntPtr g);

        [DllImport(LIB_GVC)]
        public static extern int gvLayout(IntPtr gvc, IntPtr g, string engine);

        [DllImport(LIB_GVC)]
        public static extern int gvFreeLayout(IntPtr gvc, IntPtr g);

        [DllImport(LIB_GVC)]
        public static extern int gvRenderFilename(IntPtr gvc, IntPtr g,
            string format, string fileName);

        [DllImport(LIB_GVC)]
        public static extern int gvRenderData(IntPtr gvc, IntPtr g,
            string format, out IntPtr result, out int length);

        public static byte[] RenderImage(string source, string layout, string format)
        {
            // Create a Graphviz context
            IntPtr gvc = gvContext();
            if (gvc == IntPtr.Zero)
                throw new Exception("Failed to create Graphviz context.");

            // Load the DOT data into a graph
            IntPtr g = agmemread(source);
            if (g == IntPtr.Zero)
                throw new Exception("Failed to create graph from source. Check for syntax errors.");

            // Apply a layout
            if (gvLayout(gvc, g, layout) != SUCCESS)
                throw new Exception("Layout failed.");

            IntPtr result;
            int length;

            // Render the graph
            if (gvRenderData(gvc, g, format, out result, out length) != SUCCESS)
                throw new Exception("Render failed.");

            // Create an array to hold the rendered graph
            byte[] bytes = new byte[length];

            // Copy the image from the IntPtr
            Marshal.Copy(result, bytes, 0, length);

            // Free up the resources
            gvFreeLayout(gvc, g);
            agclose(g);
            gvFreeContext(gvc);

            return bytes;
        }
    }
}