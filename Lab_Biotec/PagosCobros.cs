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
    public partial class PagosCobros : Form
    {
        public PagosCobros()
        {
            InitializeComponent();
            List<string> Nombres = new List<String>{ 
            "Yanely Izquierdo Vivar",
            "Leandra Pizarro Orozco",
            "Daniel Carvajal Martinez",
            "Nailea Montoya Velarde",
            "Cecilia Vazquez Terrero",
            "Marilu Bracamontes Castro",
            "Humberto Hernandez Figueroa",
            "Ernesto Rodriguez Ogazon",
            "Elvia Brown Clavijero",
            "Elieis Bastidas Almazan",
            "Luz De La Cueva De La Pena",
            "Maria Barra Ogazon",
            "Francisco Zavala Coy",
            "Ignacio Vallejo Herrera",
            "Eru Zirion Arrellano",
            "Xochitl Beltran Castro",
            "Eru Allende Diaz Del Castillo",
            "Jorge Fernandez De Hijar Rodríguez",
            "Alicia Cárdenas Yraeta",
            "Irma Zamora Ana",
            "Aracely Vasquez Guerra",
            "Jaimenacho Gonzalez Estrada",
            "Oswaldo De La Peza Pico",
            "Filiberto Ogazon Vialpando"
                     };

            Random rnd = new Random();
            int saldo = 0;
            foreach (string nombre in Nombres)
            {
                int debe = rnd.Next(100, 800);
                deudores.Rows.Add(nombre, "$" + debe);
                saldo += debe;
            }

            deudores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ajustamos el tamaño de las columnas
            deudores.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            deudores.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(new System.Drawing.Font("Microsoft Sans Serif", 10.0f), FontStyle.Bold);
            deudores.ColumnHeadersHeight = 30;
            deudores.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            deudores.Columns[0].Width = 400;

            SaldoDeudores.Text = "$" + saldo.ToString("N0");
       
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DetallesDeudas DD = new DetallesDeudas();
            DD.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HistorialPagos HP = new HistorialPagos();
            HP.ShowDialog();
        }
    }
}
