#define LOG_TRACES
using System;
using System.Windows.Forms;
using System.Diagnostics;
#if !DEBUG || LOG_TRACES
using System.IO;
using System.Reflection;
#endif

namespace Starlight
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

#if !DEBUG || LOG_TRACES
            var path = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, "log");
            if (File.Exists(path))
                File.Delete(path);
            var listener = new TextWriterTraceListener(path)
            {
                Filter = new EventTypeFilter(SourceLevels.Warning)
            };
            Trace.Listeners.Add(listener);
#endif

            using (var game = new StarlightGame())
            {
                Trace.TraceInformation("StarlightGame starting");
                game.Run();
                Trace.TraceInformation("StarlightGame exiting");
            }
#if !DEBUG || LOG_TRACES
            listener.Flush();
#endif
        }
    }
}
