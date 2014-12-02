using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace Lab_Biotec
{
    public partial class Correo : Form
    {


        private String from = ReadVarAppConfig("remit");
        private String pass = ReadVarAppConfig("pass");
        private String path = ReadVarAppConfig("path");

        public Correo(String mail, String ruta, String nombre)
        {

            InitializeComponent();

            this.mail = mail;
            this.ruta = ruta;
            this.nombre = nombre;
            txtTo.Text = mail;
            tbruta.Text = ruta.Substring(path.Length+1);
           
            txtFrom.Text = from;
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            txtAsunto.Text = "Resultado de examenes practicados a: " + textInfo.ToTitleCase(nombre);

        }
        String mail;
        String ruta;
        String nombre;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            MailMessage Correo = new MailMessage();

            Correo.From = new MailAddress(from);
            Correo.To.Add(txtTo.Text);
            Correo.Subject = txtAsunto.Text;
            Correo.Body = txtContenido.Text;
            Correo.IsBodyHtml = false;
            Correo.Priority = System.Net.Mail.MailPriority.High;


            Attachment adjunto = new Attachment(ruta);
            Correo.Attachments.Add(adjunto);


            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.NetworkCredential NetCre = new System.Net.NetworkCredential(from, pass);
            smtp.Credentials = NetCre;

            smtp.Host = "smtp.live.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(Correo);
                MessageBox.Show("Correo Enviado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Correo.Dispose();
           
            this.Close();
            this.Cursor = Cursors.Default;

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

        private void correo_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.IO.File.Delete(ruta);
        }

    }
}
