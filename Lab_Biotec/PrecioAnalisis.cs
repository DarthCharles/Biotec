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
    public partial class PrecioAnalisis : Form
    {

        public PrecioAnalisis()
        {
            InitializeComponent();

            List<string> Analisis = new List<String>{ 
                 "Anti-Hepatitis Viral",
                "Biometría Hemática",
                "Coprológico",
                "Coprológico III",
                "Coproparasitoscópico",
                "Cuantificación de HGC",
                "Cultivo",
                "Depuración de Creatinina",
                "Drogas de Abuso",
                "Espermatobioscopía",
                "Examen Gral. de Orina",
                "Examen Prenupcial",
                "Glicohemoglobina",
                "Multiquímica",
                "Perfil Cardiaco",
                "Perfil de Hierro",
                "Perfil de Lípidos",
                "Perfil Ginecológico",
                "Perfil Hepático",
                "Perfil Prostático",
                "Perfil Reumatoide",
                "Perfil Tiroideo",
                "Plaquetas",
                "Prueba Inmunológica de Embarazo en Sangre",
                "Reacciones Febriles",
                "Reticulocitos",
                "Tamiz Neonatal",
                "Tiempos de Coagulación",
                "Tipeo Sanguíneo"



              };

            foreach (string analisis in Analisis)
            {
                precios.Rows.Add(analisis, "$");
            }

            precios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ajustamos el tamaño de las columnas
            precios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            precios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(new System.Drawing.Font("Microsoft Sans Serif", 10.0f), FontStyle.Bold);
            precios.ColumnHeadersHeight = 30;
            precios.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            precios.Columns[0].Width = 200;
            precios.AllowUserToAddRows = false;

        }

        private void guardar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
