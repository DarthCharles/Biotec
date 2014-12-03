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
    public partial class Configuraciones : Form
    {

        String correo, pass, bdpass, dirser;
        bool bd = false;
        public Configuraciones()
        {
            InitializeComponent();

            tbcorreo.Text = ReadVarAppConfig("remit");
            tbcontra.Text = ReadVarAppConfig("pass");
            bdadmin.Text = ReadVarAppConfig("bdpass");
            tbserver.Text = ReadVarAppConfig("dirserver");

            correo = tbcorreo.Text;
            pass = tbcontra.Text;
            bdpass = bdadmin.Text;
            dirser = tbserver.Text;

            if (ReadVarAppConfig("tipousuario").Equals("recep"))
            {
                bexportar.Enabled = false;
                bimportar.Enabled = false;
            }

        }

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

        private void bcambiar_Click(object sender, EventArgs e)
        {
            DialogResult result = folder.ShowDialog();

            if (result == DialogResult.OK)
            {
                String pata;
                pata = folder.SelectedPath;
                pata = pata.Replace(@"\", "/");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!tbcorreo.Text.Equals("") && !tbcontra.Text.Equals("") && !bdadmin.Text.Equals("") && !tbserver.Text.Equals(""))
            {


                if (correo != tbcorreo.Text || pass != tbcontra.Text || bdpass != bdadmin.Text || dirser != tbserver.Text)
                {
                    if (MessageBox.Show("Es necesario reiniciar la aplicación para guardar los cambios, desea continuar?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        EditVarAppConfig("remit", tbcorreo.Text);
                        EditVarAppConfig("pass", tbcontra.Text);
                        EditVarAppConfig("bdpass", bdadmin.Text);
                        EditVarAppConfig("dirserver", tbserver.Text);

                        Application.Restart();
                    }
                }
                else
                {
                    if (bd == true)
                    {
                        this.Close();
                    }
                    else
                    {

                        MessageBox.Show("Nada que modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }


            }
            else
            {
                MessageBox.Show("No puede dejar campos vacíos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {


                OpenFileDialog _file = new OpenFileDialog();
                _file.Title = "Seleccione Archivo";
                _file.InitialDirectory = @"C:\Lab_Biotec\Respaldo";
                _file.Filter = "Archivos SQL(*.sql)|*.sql";
                _file.FilterIndex = 1;
                _file.RestoreDirectory = true;


                if (_file.ShowDialog() == DialogResult.OK)
                {

                    if (MessageBox.Show("Esta a punto de restaurar la base de datos. ¿Desea continuar?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        String archivo = _file.FileName;
                        System.IO.StreamWriter SW;
                        SW = System.IO.File.CreateText("MySQLImport.bat");
                        SW.WriteLine(@"cd\");
                        SW.WriteLine(@"cd Program Files\MySQL\MySQL Server 5.6\bin");
                        SW.WriteLine(@"mysql.exe -u root -p" + bdpass + " laboratorio < " + archivo);
                        SW.Close();
                        System.Diagnostics.Process proc = System.Diagnostics.Process.Start("MySQLImport.bat");
                        proc.WaitForExit();

                        this.Focus();
                        MessageBox.Show(@"Base de datos importada correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bd = true;
                        Inicio.llenartabla();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Está a punto de realizar una copia de seguridad de sus datos, ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {


                System.IO.StreamWriter SW;
                SW = System.IO.File.CreateText("MySQLDump.bat");
                SW.WriteLine(@"cd\");
                SW.WriteLine(@"cd Program Files\MySQL\MySQL Server 5.6\bin");
                SW.WriteLine(@"mysqldump.exe -uroot -p" + bdpass + @" laboratorio > C:\Lab_Biotec\Respaldo\Res_Lab_Biotec_" + DateTime.Today.ToString("dd_MM_yyyy") + ".sql");
                //SW.WriteLine("\"C:\\Program Files\\MySQL\\MySQL Server 5.6\\bin\\mysql.exe\"" + " " + "\"--defaults-file=C:\\ProgramData\\MySQL\\MySQL Server 5.6\\my.ini\"" + " " + "\"-uroot\"" + " " + "\"-p\""); 
                SW.Close();
                System.Diagnostics.Process proc = System.Diagnostics.Process.Start("MySQLDump.bat");

                proc.WaitForExit();
                this.Focus();
                MessageBox.Show(@"Respaldo realizado con éxito en C:\Lab_Biotec\Respaldo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bd = true;
            }

        }

        private void tbserver_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            PrecioAnalisis PA = new PrecioAnalisis();
            PA.ShowDialog();
        }


    }
}
