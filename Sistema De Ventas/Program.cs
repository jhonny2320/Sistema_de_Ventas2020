using CapaPresentacion;
using System;
using System.Windows.Forms;

namespace Sistema_De_Ventas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin_Empleado());
        }
    }
}
