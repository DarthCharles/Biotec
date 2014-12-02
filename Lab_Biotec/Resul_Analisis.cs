using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 
using System.Diagnostics;

//Imports para escribir PDF
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

// Para leer del archivo app.config
using System.Configuration;

//?
using System.Reflection;
using System.Collections;
using MySql.Data.MySqlClient;
namespace Lab_Biotec
{
    public partial class Resul_Analisis : Form
    {
        public static List<Analisis> ANALISIS = new List<Analisis>();

        String cliente, mail, nombre;
        String sexo;
        String edad;
        List<string> doctores;

        public Boolean validacion = true;
        Boolean guardado = false;
        public String path = Resul_Analisis.ReadVarAppConfig("path");
        public static ArrayList idAnalisis = new ArrayList();
        int fontSize = 8;
        int fontHeader = 10;
        BaseFont bf = BaseFont.CreateFont("c:/windows/fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

        public iTextSharp.text.Font getFuente(int i)
        {
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(bf, i);
            return fuente;
        }

        public iTextSharp.text.Font getFuenteBold(int i)
        {
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.BOLD);
            return fuente;
        }

        public Resul_Analisis(String mail, String cliente, String nombre, String sexo, String edad, List<string> doctores)
        {
            InitializeComponent();
            this.nombre = nombre;
            this.mail = mail;
            this.sexo = sexo;
            this.edad = edad;
            this.cliente = cliente;
            this.doctores = doctores;
            idAnalisis.Clear();
            pictureBox2.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tabControl1.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            b_correo.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            b_pdf.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            guardar.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            progressBar1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);

            // Agregamos el numero de examenes dependiendo de cuántos analisis seleccionamos
            for (int i = 0; i < ANALISIS.Count; i++)
            {
                doctores[i] = tbdoctor.Text;
                TabPage tab = new TabPage();
                DataGridView tabla_tab = new DataGridView();

                tab.Text = ANALISIS[i].Name; // Aquí nombramos al tab con ayuda del arreglo de examenes    

                System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 10.0f);
                tab.Font = font;

                tabControl1.TabPages.Add(tab);            //añadimos el tab al tabcontrol
                tabControl1.ItemSize = new Size(0, 30);

                tab.Controls.Add(tabla_tab);              //añadimos la tabla al tab

                //Propiedades de la tabla


                //Propiedades de la tabla
                if (ANALISIS[i].nameTabla.Equals("esperma") || ANALISIS[i].nameTabla.Equals("cultivo") || ANALISIS[i].nameTabla.Equals("gineco") || ANALISIS[i].nameTabla.Equals("pembarazo") || ANALISIS[i].nameTabla.Equals("copro3"))
                {
                    tabla_tab.AllowUserToDeleteRows = false;
                }
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

            // aqui es donde se agrega la info en la tabla de los resultados
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                DataGridView holis = tabControl1.TabPages[i].Controls[0] as DataGridView; //recuperamos el 
                //elemento datagrid del control de tab (el que diga el index). 

                ANALISIS[i].tablaAnalisis = holis;
            }



        }




        private bool validaciondoctor()
        {
            if (tbtitulo.Text == "" || tbdoctor.Text == "")
            {
                MessageBox.Show("Por favor ingrese el nombre del doctor al que se dirige.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void b_pdf_Click(object sender, EventArgs e)
        {

            if (validaciondoctor())
            {


                String fecha = DateTime.Today.ToString("dd_MM_yyyy");
                String nombre_archi = "/" + nombre.Replace(" ", "").Substring(0, nombre.Length / 2) + "_" + fecha + ".pdf";
                crear_pdf(path, nombre_archi, "fotis.png", "linea.png");
                Process.Start(path + nombre_archi).WaitForExit(); //abrimos el documento generado y esperamos a que cierre

                try
                {
                    File.Delete(path + nombre_archi);//eliminamos 
                }
                catch { }
            }
        }

        private void b_correo_Click(object sender, EventArgs e)
        {
            if (validaciondoctor())
            {

                String fecha = DateTime.Today.ToString("dd_MM_yyyy");
                String nombre_archi = "/" + nombre.Replace(" ", "").Substring(0, nombre.Length / 2) + "_" + fecha + ".pdf";
                String ruta = path + nombre_archi;
                crear_pdf(path, nombre_archi, "fotis.png", "linea.png");
                Correo c = new Correo(mail, ruta, nombre);
                c.ShowDialog();
            }
        }

        private void guardar_Click(object sender, EventArgs e)
        {
            //try
            //{
            BaseDatos basedatos = new BaseDatos();
            guardado = true;
            if (validacion)
            {
                for (int i = 0; i < doctores.Count; i++)
                {
                    doctores[i] = tbdoctor.Text;
                }

                basedatos.leer_contenido(tabControl1, cliente, Resul_Analisis.ANALISIS, validacion, progressBar1, doctores);
                validacion = false;
                RegistroHecho reg = new RegistroHecho(true);
                reg.label1.Text = "El análisis ha sido guardado correctamente";
                reg.button2.Text = "Regresar al perfil";
                reg.button2.Width = 94;
                reg.button2.Location = new Point(175, 74);
                reg.ShowDialog();
            }
            else
            {
                if (MessageBox.Show("Se sobreescribirá la información del análisis, ¿Continuar?", "Sobreescribir registro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < doctores.Count; i++)
                        {
                            doctores[i] = tbdoctor.Text;
                        }
                        basedatos.leer_contenido(tabControl1, cliente, Resul_Analisis.ANALISIS, validacion, progressBar1, doctores);
                        validacion = false;
                        RegistroHecho reg = new RegistroHecho(true);
                        reg.label1.Text = "El análisis ha sido guardado correctamente";
                        reg.button2.Text = "Regresar al perfil";
                        reg.button2.Width = 94;
                        reg.button2.Location = new Point(175, 74);
                        reg.ShowDialog();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se ha podido cambiar el registro Error: " + ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            progressBar1.Value = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (validaciondoctor())
            {


                String fecha = DateTime.Today.ToString("dd_MM_yyyy");
                String nombre_archi = "/" + nombre.Replace(" ", "").Substring(0, nombre.Length / 2) + "_" + fecha + ".pdf";
                crear_pdf(path, nombre_archi, "fotisblank.png", "lineablank.png");
                Process.Start(path + nombre_archi).WaitForExit(); //abrimos el documento generado y esperamos a que cierre

                try
                {
                    File.Delete(path + nombre_archi);//eliminamos 
                }
                catch { }



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (validaciondoctor())
            {


                crearSobre();
                Process.Start(path + "/sobre.pdf").WaitForExit(); //abrimos el documento generado y esperamos a que cierre


                try
                {
                    File.Delete(path + "/sobre.pdf");//eliminamos 
                }
                catch { }



            }

        }

        private void Resul_Analisis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!guardado)
            {


                if (MessageBox.Show("Se perderá la información no guardada del análisis, ¿Está seguro de querer regresar?", "Regresar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    idAnalisis.Clear();
                    validacion = true;

                    e.Cancel = false;

                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


            this.Close();

        }


        /*\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ METODOS del PDF \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\*/


        public void crear_pdf(String path, String nombre_archi, string fotis, string linea)
        {
            DataGridView holis = tabControl1.TabPages[0].Controls[0] as DataGridView; //recuperamos el elemento datagrid del control de tab (el que diga el index).s

            // objeto del tipo document
            Document document = new Document();
            document.SetMargins(36f, 36f, 36f, 70f);
            //try
            //{
                File.Delete(path + nombre_archi);
                //    // Creamos el documento y seleccionamos la ruta
                PdfWriter writer = PdfWriter.GetInstance(document,

                                          new FileStream(path + nombre_archi,

                                                 FileMode.OpenOrCreate));


                document.Open();
                writer.PageEvent = new PDFFirma();

                encabezado(document, fotis, linea);

                List<PdfPTable> tablas = new List<PdfPTable>();
                List<PdfPTable> especiales = new List<PdfPTable>();


                llena_arreglo_tablas(holis, tablas, especiales);

                int numEspeciales = especiales.Count;
                for (int i = 0; i < especiales.Count; i++)
                {
                    document.Add(especiales[i]);

                    if (tablas.Count >= 1 || numEspeciales > 1)
                    {
                        document.NewPage();
                     //   encabezado(document, fotis, linea);
                        numEspeciales--;
                    }
                }

                document.Add(new Paragraph(" ", getFuente(fontSize)));
                for (int i = 0; i < tablas.Count; i++)
                {

                    tablas[i].TotalWidth = 440;
                    tablas[i].LockedWidth = true;
                    tablas[i].HorizontalAlignment = Element.ALIGN_CENTER;
                    tablas[i].SpacingBefore = 10;
                    tablas[i].KeepTogether = true;
                    document.Add(tablas[i]);

                }

                document.Close();
            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    document.Close();
            //}
        }

        public Resul_Analisis() { }
        public void encabezado(Document document, string fotis, string linea)
        {

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(fotis);
            jpg.ScalePercent(23.8f);   // le cambiamos el tamaño
            jpg.Alignment = iTextSharp.text.Image.ALIGN_LEFT;

            document.Add(jpg);
            String fecha = DateTime.Today.ToString("D").ToUpper();
            PdfPTable enca = new PdfPTable(4);
            enca.TotalWidth = 530;
            enca.LockedWidth = true;
            List<PdfPCell> datos = new List<PdfPCell>();
            PdfPCell desti = new PdfPCell(new Phrase(tbtitulo.Text + ": ", getFuente(fontHeader)));
            PdfPCell destinatario = new PdfPCell(new Phrase(tbdoctor.Text, getFuente(fontHeader)));

            PdfPCell blanc = new PdfPCell(new Phrase(" ", getFuente(fontHeader)));

            PdfPCell paciente = new PdfPCell(new Phrase("PACIENTE: ", getFuente(fontHeader)));
            PdfPCell npaciente = new PdfPCell(new Phrase(nombre.ToUpper(), getFuente(fontHeader)));

            PdfPCell edad = new PdfPCell(new Phrase("EDAD: ", getFuente(fontHeader)));
            PdfPCell edadnum = new PdfPCell(new Phrase(this.edad.ToUpper(), getFuente(fontHeader)));

            PdfPCell fechas = new PdfPCell(new Phrase("FECHA: ", getFuente(fontHeader)));
            PdfPCell fechis = new PdfPCell(new Phrase(fecha, getFuente(fontHeader)));

            PdfPCell sex = new PdfPCell(new Phrase("SEXO: ", getFuente(fontHeader)));
            PdfPCell sexo = new PdfPCell(new Phrase(this.sexo.ToUpper(), getFuente(fontHeader)));

            datos.Add(desti);
            datos.Add(destinatario);

            datos.Add(fechas);
            datos.Add(fechis);

            datos.Add(paciente);
            datos.Add(npaciente);

            datos.Add(sex);
            datos.Add(sexo);

            datos.Add(blanc);
            datos.Add(blanc);

            datos.Add(edad);
            datos.Add(edadnum);


            foreach (PdfPCell dato in datos)
            {
                dato.Border = 0;
                dato.Padding = 5;
                enca.AddCell(dato);

            }
            float[] anchoDeColumnas = new float[] { .8f, 2f, .8f, 2f };
            enca.SetWidths(anchoDeColumnas);
            document.Add(enca);
            document.Add(new Paragraph(" ", getFuente(3)));
            jpg = iTextSharp.text.Image.GetInstance(linea);
            jpg.ScalePercent(23.8f);   // le cambiamos el tamaño
            document.Add(jpg);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbdoctor.Text = doctores[tabControl1.SelectedIndex];
        }

        private void tbdoctor_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < doctores.Count; i++)
            {
                doctores[i] = tbdoctor.Text;
            }
        }

        public void llena_arreglo_tablas(DataGridView holis, List<PdfPTable> tablas, List<PdfPTable> especiales)
        {
            for (int i = 0; i < tabControl1.TabCount; i++)
            {


                holis = tabControl1.TabPages[i].Controls[0] as DataGridView;
                PdfPTable table = new PdfPTable(holis.ColumnCount);
                table.AddCell(celda_nombre(ANALISIS[i].Name, holis));
                table.SpacingBefore = 10f;
                table.SpacingAfter = 10f;
                headers(holis, table); //Agregamos los encabezados de la tabla

                for (int j = 0; j < holis.RowCount; j++)
                {
                    for (int k = 0; k < holis.ColumnCount; k++)
                    {
                        if (holis.Rows[j].Cells[k].Value == null)
                        {
                            holis.Rows[j].Cells[k].Value = " ";
                        }
                        PdfPCell cell = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value.ToString(), getFuente(fontSize)));
                        cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;


                        if (k == 1)
                        {
                            cell.BackgroundColor = new BaseColor(Color.LightGoldenrodYellow);
                        }
                        if (k == 0)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }
                        table.AddCell(cell);
                    }

                }

                switch (ANALISIS[i].Name)
                {

                    case "Espermatobioscopía":
                        especiales.Add(esperma(holis, i));

                        break;

                    case "Tamiz Neonatal":
                        especiales.Add(tneo(holis, i));
                        break;
                    case "Perfil Ginecológico":
                        especiales.Add(pginecol(holis, i));
                        break;
                    case "Perfil Prostático":
                        tablas.Add(pprost(holis, i));
                        break;
                    default:
                        tablas.Add(table);
                        break;
                }
            }
        }

        public void crearSobre()
        {
            // objeto del tipo document
            Document document = new Document(new iTextSharp.text.Rectangle(468, 261));

            try
            {
                //   File.Delete(path + "/sobre.pdf");
                //    // Creamos el documento y seleccionamos la ruta
                PdfWriter writer = PdfWriter.GetInstance(document,

                                          new FileStream(path + "/sobre.pdf",

                                                 FileMode.OpenOrCreate));


                document.Open();

                PdfPTable enca = new PdfPTable(2);
                enca.TotalWidth = 220;
                enca.LockedWidth = true;
                List<PdfPCell> datos = new List<PdfPCell>();

                PdfPCell desti = new PdfPCell(new Phrase(tbtitulo.Text + ": ", getFuente(fontHeader)));
                PdfPCell destinatario = new PdfPCell(new Phrase(tbdoctor.Text, getFuente(fontHeader)));


                PdfPCell paciente = new PdfPCell(new Phrase("PACIENTE: ", getFuente(fontHeader)));
                PdfPCell npaciente = new PdfPCell(new Phrase(nombre.ToUpper(), getFuente(fontHeader)));



                datos.Add(desti);
                datos.Add(destinatario);

                datos.Add(paciente);
                datos.Add(npaciente);

                foreach (PdfPCell dato in datos)
                {
                    dato.Border = 0;
                    enca.AddCell(dato);
                }
                float[] anchoDeColumnas = new float[] { .8f, 2f };
                enca.SetWidths(anchoDeColumnas);

                enca.WriteSelectedRows(0, -1, document.Left + 200, document.Top - 130, writer.DirectContentUnder);


                document.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        #region analisis que se ponen roñosos

        private PdfPTable ego(DataGridView holis)
        {

            List<string> results = new List<string>();


            foreach (DataGridViewRow dr in holis.Rows)
            {
                results.Add(dr.Cells[1].Value as string);

            }

            PdfPTable table = new PdfPTable(3);
            iTextSharp.text.Font fuente = getFuenteBold(fontSize);
            table.TotalWidth = 430;
            table.LockedWidth = true;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;
            table.DefaultCell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
            float[] anchoDeColumnas = new float[] { .5f, 2f, 2f };
            table.SetWidths(anchoDeColumnas);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.KeepTogether = true;
            PdfPCell header = new PdfPCell(new Phrase("EXAMEN GRAL. DE ORINA", fuente));
            header.HorizontalAlignment = Element.ALIGN_CENTER;
            header.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER + iTextSharp.text.Rectangle.TOP_BORDER;
            header.Colspan = 4;
            table.AddCell(header);
            PdfPCell exa = new PdfPCell(new Phrase("Examen", fuente));
            exa.HorizontalAlignment = Element.ALIGN_CENTER;
            exa.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER + iTextSharp.text.Rectangle.TOP_BORDER;
            exa.Colspan = 2;
            table.AddCell(exa);
            PdfPCell res = new PdfPCell(new Phrase("Resultado", fuente));
            res.HorizontalAlignment = Element.ALIGN_CENTER;
            res.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER + iTextSharp.text.Rectangle.TOP_BORDER;
            res.Colspan = 1;
            table.AddCell(res);
            table.AddCell(celda("COLOR", 2));
            table.AddCell(celdix(results[0]));


            table.AddCell(celda("ASPECTO", 2));
            table.AddCell(celdix(results[1]));

            table.AddCell(celda("DENSIDAD", 2));
            table.AddCell(celdix(results[2]));

            table.AddCell(celda("PH", 2));
            table.AddCell(celdix(results[3]));

            table.AddCell(celda("ALBUMINA", 2));
            table.AddCell(celdix(results[4]));

            table.AddCell(celda("GLUCOSA", 2));
            table.AddCell(celdix(results[5]));

            table.AddCell(celda("CETONA", 2));
            table.AddCell(celdix(results[6]));

            table.AddCell(celda("BILLIRUBINA", 2));
            table.AddCell(celdix(results[7]));

            table.AddCell(celda("LEUCOCITOS", 2));
            table.AddCell(celdix(results[8]));

            table.AddCell(celda("HEMOGLOBINA", 2));
            table.AddCell(celdix(results[9]));

            table.AddCell(celdon("S"));
            table.AddCell(celda("CEL. EPITELIALES", 1));
            table.AddCell(celdix(results[10]));

            table.AddCell(celdon("E"));
            table.AddCell(celda("BACTERIA", 1));
            table.AddCell(celdix(results[11]));

            table.AddCell(celdon("D"));
            table.AddCell(celda("LEUCOCITOS", 1));
            table.AddCell(celdix(results[12]));

            table.AddCell(celdon("I"));
            table.AddCell(celda("ERITOCITOS", 1));
            table.AddCell(celdix(results[13]));

            table.AddCell(celdon("M"));
            table.AddCell(celda("CRISTALES", 1));
            table.AddCell(celdix(results[14]));

            table.AddCell(celdon("E"));
            table.AddCell(celda("LEVADURA", 1));
            table.AddCell(celdix(results[15]));

            table.AddCell(celdon("N"));
            table.AddCell(celda("F. MUCINA", 1));
            table.AddCell(celdix(results[16]));

            table.AddCell(celdon("T"));
            table.AddCell(celda("OTROS", 1));
            table.AddCell(celdix(results[17]));

            PdfPCell o = new PdfPCell(new Phrase("O", getFuente(fontSize)));
            o.HorizontalAlignment = Element.ALIGN_CENTER;
            o.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            table.AddCell(o);
            table.AddCell(celda(" ", 1));
            table.AddCell(celdix(" "));

            return table;
        }

        public PdfPTable cultivo(DataGridView holis, int a)
        {
            PdfPTable table = new PdfPTable(holis.ColumnCount);
            for (int i = a; i <= a; i++)
            {

                table.TotalWidth = 430;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.KeepTogether = true;
                table.AddCell(celda_nombre(ANALISIS[i].Name, holis));
                table.SpacingBefore = 10f;
                table.SpacingAfter = 10f;
                headers(holis, table); //Agregamos los encabezados de la tabla

                for (int j = 0; j < holis.RowCount; j++)
                {

                    for (int k = 0; k < holis.ColumnCount; k++)
                    {

                        iTextSharp.text.Font fuente = getFuente(fontSize);
                        PdfPCell cell = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value as string, fuente));
                        cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        if (k == 1)
                        {
                            cell.BackgroundColor = new BaseColor(Color.LightGoldenrodYellow);
                        }
                        if (k == 0)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        if (j == 4)
                        {
                            iTextSharp.text.Font fuentes = getFuenteBold(fontSize);
                            PdfPCell cells = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value as string, fuentes));
                            cells.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                            cells.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cells);
                        }
                        else
                        {

                            table.AddCell(cell);
                        }
                    }
                }
            }

            return table;

        }

        public PdfPTable esperma(DataGridView holis, int a)
        {
            PdfPTable table = new PdfPTable(holis.ColumnCount);
            for (int i = a; i <= a; i++)
            {
                table.TotalWidth = 430;
                table.LockedWidth = true;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.KeepTogether = true;
                table.AddCell(celda_nombre(ANALISIS[i].Name, holis));
                table.SpacingBefore = 50f;
                table.SpacingAfter = 10f;
                headers(holis, table); //Agregamos los encabezados de la tabla

                for (int j = 0; j < holis.RowCount; j++)
                {

                    for (int k = 0; k < holis.ColumnCount; k++)
                    {

                        iTextSharp.text.Font fuente = getFuente(fontSize);
                        PdfPCell cell = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value as string, fuente));
                        cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        if (k == 1)
                        {
                            cell.BackgroundColor = new BaseColor(Color.LightGoldenrodYellow);
                        }
                        if (k == 0)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        if (j == 21)
                        {
                            iTextSharp.text.Font fuentes = getFuenteBold(fontSize);
                            PdfPCell cells = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value as string, fuentes));
                            cells.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                            cells.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cells);
                        }
                        else
                        {

                            table.AddCell(cell);
                        }
                    }


                }


            }

            return table;

        }

        public PdfPTable tneo(DataGridView holis, int a)
        {
            PdfPTable table = new PdfPTable(holis.ColumnCount);
            for (int i = a; i <= a; i++)
            {

                float[] anchoDeColumnas = new float[] { 5f, 2f, 2f, 3f, .8f, .8f, .8f, .8f };

                table.SetWidths(anchoDeColumnas);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.KeepTogether = true;
                table.AddCell(celda_nombre(ANALISIS[i].Name, holis));
                table.SpacingBefore = 20f;
                table.SpacingAfter = 10f;
                headers(holis, table); //Agregamos los encabezados de la tabla

                for (int j = 0; j < holis.RowCount; j++)
                {

                    for (int k = 0; k < holis.ColumnCount; k++)
                    {

                        iTextSharp.text.Font fuente = getFuente(7);
                        PdfPCell cell = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value as string, fuente));
                        cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        if (k == 1)
                        {
                            cell.BackgroundColor = new BaseColor(Color.LightGoldenrodYellow);
                        }
                        if (k == 0)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        table.AddCell(cell);

                    }


                }


            }

            return table;

        }

        public PdfPTable pginecol(DataGridView holis, int a)
        {
            PdfPTable table = new PdfPTable(holis.ColumnCount);
            for (int i = a; i <= a; i++)
            {

                float[] anchoDeColumnas = new float[] { 4f, 3f, 2f, 5f, 2f };

                table.SetWidths(anchoDeColumnas);
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.KeepTogether = true;
                table.AddCell(celda_nombre(ANALISIS[i].Name, holis));
                table.SpacingBefore = 50f;
                table.SpacingAfter = 10f;
                headers(holis, table); //Agregamos los encabezados de la tabla

                for (int j = 0; j < holis.RowCount; j++)
                {

                    for (int k = 0; k < holis.ColumnCount; k++)
                    {

                        iTextSharp.text.Font fuente = getFuente(fontSize);
                        PdfPCell cell = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value as string, fuente));
                        cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        if (k == 1)
                        {
                            cell.BackgroundColor = new BaseColor(Color.LightGoldenrodYellow);
                        }
                        if (k == 0)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        if (k == 3)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }
                        table.AddCell(cell);

                    }


                }


            }

            return table;

        }

        public PdfPTable pprost(DataGridView holis, int a)
        {
            PdfPTable table = new PdfPTable(holis.ColumnCount);
            for (int i = a; i <= a; i++)
            {
                table.TotalWidth = 430;
                float[] anchoDeColumnas = new float[] { 4f, 2f, 2f, 2f, 2f, 3f, };

                table.SetWidths(anchoDeColumnas);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                //table.KeepTogether = true;
                table.AddCell(celda_nombre(ANALISIS[i].Name, holis));
                table.SpacingBefore = 10f;
                table.SpacingAfter = 10f;
                headers(holis, table); //Agregamos los encabezados de la tabla

                for (int j = 0; j < holis.RowCount; j++)
                {

                    for (int k = 0; k < holis.ColumnCount; k++)
                    {

                        iTextSharp.text.Font fuente = getFuente(fontSize);
                        PdfPCell cell = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value as string, fuente));
                        cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        if (k == 1)
                        {
                            cell.BackgroundColor = new BaseColor(Color.LightGoldenrodYellow);
                        }
                        if (k == 0)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }
                        if (k == 3)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                        }
                        if (j == 21)
                        {
                            iTextSharp.text.Font fuentes = getFuente(fontSize);
                            PdfPCell cells = new PdfPCell(new Phrase(holis.Rows[j].Cells[k].Value as string, fuentes));
                            cells.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                            cells.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cells);
                        }
                        else
                        {

                            table.AddCell(cell);
                        }
                    }


                }


            }

            return table;

        }

        #endregion

        #region celdas especiales

        public PdfPCell celdon(String nombre)
        {

            PdfPCell cell = new PdfPCell(new Phrase(nombre, getFuente(fontSize)));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //cell.PaddingTop = 5f;

            return cell;
        }

        public PdfPCell celda(String nombre, int i)
        {

            PdfPCell cell = new PdfPCell(new Phrase(nombre, getFuente(fontSize)));
            // cell.Border = 2;

            cell.Colspan = i;
            cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
            //cell.PaddingTop = 5f;
            return cell;
        }

        public PdfPCell celdix(String cadena)
        {

            PdfPCell cell = new PdfPCell(new Phrase(cadena, getFuente(fontSize)));
            cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //cell.PaddingTop = 5f;
            cell.BackgroundColor = new BaseColor(Color.LightGoldenrodYellow);
            return cell;
        }

        public PdfPCell celda_nombre(String cadena, DataGridView holis)
        {

            iTextSharp.text.Font fuente = getFuenteBold(fontSize); ;
            PdfPCell cell = new PdfPCell(new Phrase(cadena.ToUpper(), fuente));
            // cell.Border = 2;
            cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cell.Colspan = holis.Columns.Count;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            return cell;
        }

        #endregion

        public void headers(DataGridView holis, PdfPTable table)
        {


            for (int i = 0; i < holis.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase((holis.Columns[i].HeaderText), getFuenteBold(fontSize)));
                cell.Border = iTextSharp.text.Rectangle.TOP_BORDER + iTextSharp.text.Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
            }
        }

        public static string ReadVarAppConfig(string nombreVar)
        {
            //leer una variable de app.config
            string resultado = null;
            try
            {
                resultado = ConfigurationManager.AppSettings[nombreVar];
            }
            catch (Exception)
            {
                resultado = null;
            }
            return resultado;
        }


        //|||||||||||||||||||||||||||||||||||||||||||||||Método sobrecargado||||||||||||||||||||||||||||||||||||||||||||||||||||

        public Resul_Analisis(ArrayList idanalisis, ArrayList tipo, String mail, String nombre, string sexo, string edad, List<string> doctores)
        {
            InitializeComponent();
            this.doctores = doctores;
            ArrayList contenido = new ArrayList();
            pictureBox2.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tabControl1.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            b_correo.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            b_pdf.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            guardar.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            progressBar1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            idAnalisis = idanalisis;
            guardado = true;
            this.nombre = nombre;
            this.mail = mail;
            this.sexo = sexo;
            this.edad = edad;
            guardar.Text = "Modificar registro";
            for (int i = 0; i < ANALISIS.Count; i++)
            {
                Conexion con = new Conexion();
                doctores[i] = con.leer1("doctor", "documento", idanalisis[i].ToString());

                TabPage tab = new TabPage();
                DataGridView tabla_tab = new DataGridView();

                tab.Text = ANALISIS[i].Name; // Aquí nombramos al tab con ayuda del arreglo de examenes    

                System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 10.0f);
                tab.Font = font;

                tabControl1.TabPages.Add(tab);            //añadimos el tab al tabcontrol
                tabControl1.ItemSize = new Size(0, 30);

                tab.Controls.Add(tabla_tab);              //añadimos la tabla al tab

                //Propiedades de la tabla

                if (ANALISIS[i].nameTabla.Equals("esperma") || ANALISIS[i].nameTabla.Equals("cultivo") ||  ANALISIS[i].nameTabla.Equals("gineco") || ANALISIS[i].nameTabla.Equals("pembarazo") || ANALISIS[i].nameTabla.Equals("copro3"))
                {
                    tabla_tab.AllowUserToDeleteRows = false;
                }
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

            // aqui es donde se agrega la info en la tabla de los resultados
            for (int i = 0; i < tabControl1.TabCount; i++)
            {

                contenido = ver_detalles(idanalisis[i].ToString(), tipo[i].ToString());
                DataGridView holis = tabControl1.TabPages[i].Controls[0] as DataGridView; //recuperamos el 
                //elemento datagrid del control de tab (el que diga el index). 

                ANALISIS[i].tablaAnalisis = holis;
                int contador = 0;
                contenido.RemoveAt(contenido.Count - 1);




                if (ANALISIS[i].nameTabla.Equals("copro3"))
                {
                    for (int l = 1; l < holis.ColumnCount; l++)
                    {
                        for (int m = 0; m < holis.RowCount; m++)
                        {

                            holis.Rows[m].Cells[l].Value = contenido[contador + 1];
                            if (m < contenido.Count - 1) contador++;

                        }
                    }
                }
                else
                {
                    if (ANALISIS[i].nameTabla.Equals("cultivo"))
                    {

                        holis.Rows[0].Cells[0].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[0].Cells[1].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[0].Cells[2].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[1].Cells[0].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[1].Cells[1].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[1].Cells[2].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[2].Cells[0].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[2].Cells[1].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[2].Cells[2].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[3].Cells[0].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[3].Cells[1].Value = contenido[contador + 1];
                        contador++;
                        holis.Rows[3].Cells[2].Value = contenido[contador + 1];
                        contador++;

                        for (int j = 5; j <= 21; j++)
                        {

                            holis.Rows[j].Cells[2].Value = contenido[contador + 1];
                            if (j < contenido.Count - 1) contador++;

                        }
                    }
                    else
                    {
                        if (ANALISIS[i].nameTabla.Equals("prosta"))
                        {
                            holis.Rows[0].Cells[1].Value = contenido[contador + 1];
                            contador++;

                            holis.Rows[4].Cells[1].Value = contenido[contador + 1];
                            contador++;

                            holis.Rows[5].Cells[1].Value = contenido[contador + 1];
                            contador++;

                            if (holis.Rows[0].Cells[1].Value.ToString().Equals("@@@"))
                            {
                                holis.Rows.RemoveAt(3);
                                holis.Rows.RemoveAt(2);
                                holis.Rows.RemoveAt(1);
                                holis.Rows.RemoveAt(0);
                            }

                            if (holis.Rows[0].Cells[holis.RowCount-1].Value.ToString().Equals("@@@"))
                            {
                                holis.Rows.RemoveAt(holis.RowCount-1);
                            }

                        }
                        else
                        {

                            for (int j = 0; j < holis.RowCount; j++)
                            {
                                if (!holis.Rows[j].Cells[0].Value.ToString().Equals(" ") && !holis.Rows[j].Cells[0].Value.ToString().Equals("Normales") && !holis.Rows[j].Cells[0].Value.ToString().Equals(" ") && !holis.Rows[j].Cells[0].Value.ToString().Equals("") && !holis.Rows[j].Cells[0].Value.ToString().Equals("DE HORMONA") && !holis.Rows[j].Cells[0].Value.ToString().Equals("GONADOTROPICA") && !holis.Rows[j].Cells[0].Value.ToString().Equals("CORIONICA"))
                                {
                                    
                                        holis.Rows[j].Cells[1].Value = contenido[contador + 1];
                                        if (j < contenido.Count) contador++;
                                    
                                }
                            }
                        }
                    }
                }



                for (int j = holis.RowCount - 1; j >= 0; j--)
                {
                    if (holis.Rows[j].Cells[1].Value.ToString().Equals("@@@"))
                    {
                        holis.Rows.RemoveAt(j);
                    }
                }
            }
            tbdoctor.Text = doctores[0];

        }

        ArrayList ver_detalles(string idanalisis, string tipo)
        {
            Conexion con = new Conexion();
            ArrayList contenido = new ArrayList();
            contenido = con.leer2(tipo, idanalisis);

            return contenido;
        }

    }

    public class PDFFirma : PdfPageEventHelper
    {
      
        public override void OnStartPage(PdfWriter writer, Document document)
        {

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("fotisblank.png");
            jpg.ScalePercent(23.8f);   // le cambiamos el tamaño
            jpg.Alignment = iTextSharp.text.Image.ALIGN_LEFT;       

            document.Add(jpg);


        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("firma.png");
            jpg.ScalePercent(40f);   // le cambiamos el tamaño
            jpg.SetAbsolutePosition(227, 40);

            document.Add(jpg);

        }


    }
}
