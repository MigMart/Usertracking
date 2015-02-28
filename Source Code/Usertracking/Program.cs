using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Usertracking
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
            MyApp app = new MyApp();
            Application.Run(app.MainForm());
            if (app != null)
                if (app.kinect != null)
                    if (app.kinect.IsRunning)
                        app.kinect.Stop();

        }
    }
}
