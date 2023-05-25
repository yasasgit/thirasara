using System;
using System.Windows.Forms;

namespace ThirasaraTest
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Console.ReadLine();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
