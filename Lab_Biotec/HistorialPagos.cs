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
    public partial class HistorialPagos : Form
    {
        public HistorialPagos()
        {
            InitializeComponent();
            List<string> Meses = new List<String>{ 
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"

                     };

            List<string> Nombres = new List<String>{ 
                "Oswaldo Rodas",
                "Irma Bracamontes Cortes",
                "Olademis Jurado Banuelos",
                "Cecilia Del Valle Guerra",
                "Rosamar Hernandez Iguiniz",
                "Myra Montano Herrera",
                "Juaquine Nieto Jaramillo",
                "Inez Muniz Serrano",
                "Manuela Vallejo Vallejo",
                "Leticia Garibay",
                "Javier Rincon Balli",
                "Sergio Hernández Muniz",
                "Kasper Saenz Rael",
                "Zumac Segura Tapia",
                "Emilio Montejano Coy",
                "Maria Valdez Tafoya",
                "Kasper García Muniz",
                "DeMario Barra Gallego",
                "Ramirez Saso Leon",
                "Elieis Sánchez Cruz",
                "Luz Arizpe Padilla"

                     };

            for (int i = 2014; i >= 2010; i--)
            {
                cbaño.Items.Add(i);
            }
            for (int i = 0; i < 12; i++)
            {

                TabPage tab = new TabPage();
                DataGridView tabla_tab = new DataGridView();

           

                System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 10.0f);
                tab.Font = font;
                tab.Text = Meses[i];
                tabControl1.TabPages.Add(tab);            //añadimos el tab al tabcontrol
                tabControl1.ItemSize = new Size(0, 30);

                tab.Controls.Add(tabla_tab);              //añadimos la tabla al tab

                //Propiedades de la tabla

                tabla_tab.AllowUserToAddRows = false;
                tabla_tab.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ajustamos el tamaño de las columnas
                tabla_tab.BackgroundColor = Color.LightSteelBlue; //cambiamos el color de fondo de la tabla
                tabla_tab.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tabla_tab.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(font, FontStyle.Bold);
                tabla_tab.ColumnHeadersHeight = 30;

                //tamaño
                tabla_tab.Height = 100;
                tabla_tab.Width = 200;


                tabla_tab.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);

            }

            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                DataGridView holis = tabControl1.TabPages[i].Controls[0] as DataGridView; //recuperamos el 
                //elemento datagrid del control de tab (el que diga el index). 
                holis.ColumnCount = 2;
                foreach (string nombre in Nombres)
                {

                    holis.Rows.Add(nombre, "$");
                   
                }
            }
        }
    }
}
