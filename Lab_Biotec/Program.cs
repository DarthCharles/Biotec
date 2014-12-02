using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Biotec
{
    static class Program
    {

        public static string ReadVarAppConfig(string nombreVar)
        {
            //leer una variable de app.config
            string resultado = null;
            try
            {
                resultado = System.Configuration.ConfigurationManager.AppSettings[nombreVar];
            }
            catch (Exception)
            {
                resultado = null;
            }
            return resultado;
        }

        public static void EditVarAppConfig(String var, String nombreVar)
        {
            Configuration conf = System.Configuration.ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            AppSettingsSection seccion = conf.AppSettings;
            seccion.Settings[var].Value = nombreVar;
            conf.Save();

        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

        
            if (ReadVarAppConfig("tipousuario") == "")
            {
                Application.Run(new TipoUsuario());
            }
            else
            {
                if (ReadVarAppConfig("bdpass") == "")
                {
                    Application.Run(new Configuraciones());
                }
                else
                {

                Application.Run(new Inicio());
            }
            }
           
        }
    }
}
