using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Biotec
{

    class BaseDatos
    {
        Conexion con = new Conexion();

        public void leer_contenido(TabControl tabControl1, String cliente, List<Analisis> ANALISIS,
            Boolean validacion, ProgressBar progress, List<string> doctores)
        {
            progress.Maximum = tabControl1.TabCount;
            progress.Step = 1;
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                progress.PerformStep();

                if (ANALISIS[i].Especial)
                {
                    MessageBox.Show("El analisis " + ANALISIS[i].Name + " no se guardará por que es especial", "Advertencia"
                        , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {

                    string tipo = Resul_Analisis.ANALISIS[i].nameTabla;
                    DataGridView tabla = tabControl1.TabPages[i].Controls[0] as DataGridView;
                    ArrayList values = new ArrayList();
                    ArrayList parametros = new ArrayList();

                    #region especiales
                    if (ANALISIS[i].nameTabla.Equals("copro3"))
                    {
                        tabla.AllowUserToDeleteRows = false;

                        parametros.Add("color");
                        parametros.Add("aspecto");
                        parametros.Add("ph");
                        parametros.Add("cetonicos");
                        parametros.Add("reductores");
                        parametros.Add("sangre_oculta");
                        parametros.Add("sangre_visible");
                        parametros.Add("almidon");
                        parametros.Add("amiba");
                        parametros.Add("diferencial");
                        parametros.Add("levaduras");
                        parametros.Add("copro");
                        parametros.Add("moco");
                        parametros.Add("leucocitos");
                        parametros.Add("color2");
                        parametros.Add("aspecto2");
                        parametros.Add("ph2");
                        parametros.Add("cetonicos2");
                        parametros.Add("reductores2");
                        parametros.Add("sangre_oculta2");
                        parametros.Add("sangre_visible2");
                        parametros.Add("almidon2");
                        parametros.Add("amiba2");
                        parametros.Add("diferencial2");
                        parametros.Add("levaduras2");
                        parametros.Add("copro2");
                        parametros.Add("moco2");
                        parametros.Add("leucocitos2");
                        parametros.Add("color3");
                        parametros.Add("aspecto3");
                        parametros.Add("ph3");
                        parametros.Add("cetonicos3");
                        parametros.Add("reductores3");
                        parametros.Add("sangre_oculta3");
                        parametros.Add("sangre_visible3");
                        parametros.Add("almidon3");
                        parametros.Add("amiba3");
                        parametros.Add("diferencial3");
                        parametros.Add("levaduras3");
                        parametros.Add("copro3");
                        parametros.Add("moco3");
                        parametros.Add("leucocitos3");



                        for (int j = 0; j < tabla.RowCount; j++)
                        {
                            values.Add(tabla.Rows[j].Cells[1].Value.ToString());
                        }

                        for (int j = 0; j < tabla.RowCount; j++)
                        {
                            values.Add(tabla.Rows[j].Cells[2].Value.ToString());
                        }

                        for (int j = 0; j < tabla.RowCount; j++)
                        {
                            values.Add(tabla.Rows[j].Cells[3].Value.ToString());
                        }
                    }
                    else
                    {
                        if (ANALISIS[i].nameTabla.Equals("esperma"))
                        {
                            tabla.AllowUserToDeleteRows = false;
                            parametros.Add("periodo");
                            parametros.Add("hora");
                            parametros.Add("perdida");
                            parametros.Add("dificultad");
                            parametros.Add("aspecto");
                            parametros.Add("licuefaccion");
                            parametros.Add("cons");
                            parametros.Add("volumen");
                            parametros.Add("ph");
                            parametros.Add("conc");
                            parametros.Add("viabilidad");
                            parametros.Add("motilidad");
                            parametros.Add("aglutinacion");
                            parametros.Add("leucocitos");
                            parametros.Add("bacterias");
                            parametros.Add("eritrocitos");
                            parametros.Add("germinales");
                            parametros.Add("epiteliales");
                            parametros.Add("detri");
                            parametros.Add("morf");
                            parametros.Add("macro");
                            parametros.Add("micro");
                            parametros.Add("dobles");
                            parametros.Add("cola");
                            parametros.Add("alfiler");
                            parametros.Add("acint");
                            parametros.Add("obser");

                            for (int j = 0; j <= 3; j++)
                            {
                                values.Add(tabla.Rows[j].Cells[1].Value.ToString());
                            }

                            for (int j = 5; j <= 20; j++)
                            {
                                values.Add(tabla.Rows[j].Cells[1].Value.ToString());
                            }

                            for (int j = 22; j <= 27; j++)
                            {
                                values.Add(tabla.Rows[j].Cells[1].Value.ToString());
                            }

                            values.Add(tabla.Rows[29].Cells[1].Value.ToString());

                        }
                        else
                        {
                            if (ANALISIS[i].nameTabla.Equals("cultivo"))
                            {
                                tabla.AllowUserToDeleteRows = false;
                                parametros.Add("aislamiento");
                                parametros.Add("frotis");
                                parametros.Add("cantidad");

                                parametros.Add("reng1");
                                parametros.Add("reng2");
                                parametros.Add("reng3");
                                parametros.Add("reng4");
                                parametros.Add("reng5");
                                parametros.Add("reng6");
                                parametros.Add("reng7");
                                parametros.Add("reng8");
                                parametros.Add("reng9");


                                parametros.Add("a");
                                parametros.Add("b");
                                parametros.Add("c");
                                parametros.Add("d");
                                parametros.Add("e");
                                parametros.Add("f");
                                parametros.Add("g");
                                parametros.Add("h");
                                parametros.Add("i");
                                parametros.Add("j");
                                parametros.Add("k");
                                parametros.Add("l");
                                parametros.Add("m");
                                parametros.Add("n");
                                parametros.Add("o");
                                parametros.Add("p");
                                parametros.Add("q");


                                values.Add(tabla.Rows[0].Cells[0].Value.ToString());
                                values.Add(tabla.Rows[0].Cells[1].Value.ToString());
                                values.Add(tabla.Rows[0].Cells[2].Value.ToString());

                                values.Add(tabla.Rows[1].Cells[0].Value.ToString());
                                values.Add(tabla.Rows[1].Cells[1].Value.ToString());
                                values.Add(tabla.Rows[1].Cells[2].Value.ToString());

                                values.Add(tabla.Rows[2].Cells[0].Value.ToString());
                                values.Add(tabla.Rows[2].Cells[1].Value.ToString());
                                values.Add(tabla.Rows[2].Cells[2].Value.ToString());

                                values.Add(tabla.Rows[3].Cells[0].Value.ToString());
                                values.Add(tabla.Rows[3].Cells[1].Value.ToString());
                                values.Add(tabla.Rows[3].Cells[2].Value.ToString());

                                for (int j = 5; j <= 21; j++)
                                {
                                    values.Add(tabla.Rows[j].Cells[2].Value.ToString());
                                }

                            }
                            else
                            {
                                

                                    if (ANALISIS[i].nameTabla.Equals("gineco"))
                                    {
                                        tabla.AllowUserToDeleteRows = false;
                                        parametros.Add("estradiol");
                                        parametros.Add("estimulante");
                                        parametros.Add("lute");
                                        parametros.Add("proges");
                                        parametros.Add("prolac");
                                        values.Add(tabla.Rows[0].Cells[1].Value.ToString());
                                        values.Add(tabla.Rows[6].Cells[1].Value.ToString());
                                        values.Add(tabla.Rows[12].Cells[1].Value.ToString());
                                        values.Add(tabla.Rows[18].Cells[1].Value.ToString());
                                        values.Add(tabla.Rows[24].Cells[1].Value.ToString());
                                    }
                                    else
                                    {

                                        if (ANALISIS[i].nameTabla.Equals("pembarazo"))
                                        {
                                            tabla.AllowUserToDeleteRows = false;
                                            parametros.Add("gonadotropica");
                                            values.Add(tabla.Rows[0].Cells[1].Value.ToString());
                                        }
                                        else
                                        {
                    #endregion

                                            parametros = getTablaNombres(Resul_Analisis.ANALISIS[i], tabla);

                                            for (int j = 0; j < tabla.RowCount; j++)
                                            {
                                                if (!tabla.Rows[j].Cells[0].Value.ToString().Equals(" "))
                                                {
                                                    values.Add(tabla.Rows[j].Cells[1].Value.ToString());
                                                }
                                            }
                                        }
                                }
                            }
                        }
                    }

                    if (validacion)
                    {
                        nuevoRegistro(tipo, cliente, parametros, values, doctores[i]);
                    }
                    else
                    {
                        update_analisis(tipo, parametros, values, Resul_Analisis.idAnalisis[i].ToString(), doctores[i]);
                    }

                }
            }
        }

        private void nuevo_documento(string tipo, string cliente, string doctor)
        {
            string dt = DateTime.Today.ToString("yyyy-MM-dd");
            string[] parametros = { "fecha", "id_cliente", "tipo", "doctor" };
            string[] values = { dt, cliente, tipo, doctor };
            Conexion con = new Conexion();
            con.insertar("documento", parametros, values);
        }

        public string ultimo_analisis()
        {
            return con.leer("max(idanalisis)", "documento")[0].ToString();
        }

        public string ultimo_cliente()
        {
            return con.leer("max(id_cliente)", "clientes")[0].ToString();
        }

        private void nuevo_analisis(string tipo, ArrayList parametros, ArrayList values)
        {
            con.insertar(tipo, parametros, values);
        }

        private void update_analisis(String tabla, ArrayList parametros, ArrayList values, string idAnalisis, string doctor)
        {
            Conexion con = new Conexion();
            con.update(tabla, parametros, values, idAnalisis, doctor);
        }

        private void nuevoRegistro(String tipo, String cliente, ArrayList parametros, ArrayList values, string doctor)
        {
            nuevo_documento(tipo, cliente, doctor);
            Resul_Analisis.idAnalisis.Add(ultimo_analisis());
            parametros.Add("idAnalisis");
            values.Add(ultimo_analisis());
            nuevo_analisis(tipo, parametros, values);
        }

        private ArrayList getTablaNombres(Analisis analisis, DataGridView tabla)
        {
            ArrayList nombreRegistros = new ArrayList();
            List<List<String>> examen = analisis.examen;

            for (int i = 0; i < tabla.RowCount; i++)
            {
                if (!tabla.Rows[i].Cells[0].Value.ToString().Equals(" "))
                {

                    for (int j = 0; j < examen[1].Count; j++)
                    {

                        if (tabla.Rows[i].Cells[0].Value.ToString().Equals(examen[0][j]))
                        {
                            nombreRegistros.Add(examen[1][j]);
                        }

                    }
                }
            }


            //for (int i = 0; i < tabla.RowCount; i++)
            //{

            //}

            //for (int i = 0; i < tabla.RowCount; i++)
            //{
            //    int index = examen[0].FindIndex(x => x.StartsWith(tabla.Rows[i].Cells[0].Value.ToString()));

            //    if (index != -1 && index < analisis.examen[1].Count)
            //    {
            //        nombreRegistros.Add(analisis.examen[1][index]);
            //    }

            //}
            return nombreRegistros;
        }
    }
}