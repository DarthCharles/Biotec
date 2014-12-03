using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab_Biotec
{
    public partial class DetallesDeudas : Form
    {
        public DetallesDeudas()
        {
            InitializeComponent();

            abonos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            abonos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(new System.Drawing.Font("Microsoft Sans Serif", 10.0f), FontStyle.Bold);
            abonos.ColumnHeadersHeight = 30;
            abonos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            abonos.Columns[0].Width = 150;

            cargos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ajustamos el tamaño de las columnas
            cargos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cargos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(new System.Drawing.Font("Microsoft Sans Serif", 10.0f), FontStyle.Bold);
            cargos.ColumnHeadersHeight = 30;
            cargos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            cargos.Columns[0].Width = 150;

            List<string> Analisis = new List<String>{ 
                "Perfil Cardiaco",
                "Coprológico III",
                "Perfil Hepático",
                "Cuantificación de HGC",
                "Glicohemoglobina"
              };

            Random rnd = new Random();

            int saldoabonos = 0;
            foreach (string analisis in Analisis)
            {
                int debe = rnd.Next(80, 150);

                abonos.Rows.Add(DateTime.Now.ToString(), "$" + debe);
                saldoabonos += debe;
            }

            int saldodeudas = 0;
            foreach (string analisis in Analisis)
            {
                int debe = rnd.Next(100, 260);

                cargos.Rows.Add(analisis, "$" + debe);
                saldodeudas += debe;
            }


            saldoCargos.Text = "$" + saldodeudas.ToString("N0");

            saldoAbonos.Text = "$" + saldoabonos.ToString("N0");

            saldoTotal.Text = "$" + (saldodeudas - saldoabonos).ToString("N0") + "";
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            String valorid = 3 + "";
            int id = int.Parse(valorid);
            Perfil perf = new Perfil(id);

            perf.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarAbono AA = new AgregarAbono();
            AA.Show();
        }
    }
}
