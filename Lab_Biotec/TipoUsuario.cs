using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Biotec
{
    public partial class TipoUsuario : Form
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

        public TipoUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditVarAppConfig("tipousuario", "admin");
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditVarAppConfig("tipousuario", "recep");
            Application.Restart();

            

        }
    }
}
