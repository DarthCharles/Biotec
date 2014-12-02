
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab_Biotec
{
    public class Analisis
    {
        private int colum_count = 0;
        private string nombre;
        private string nombretabla;
        private List<List<String>> examenes;
        private bool especial = false;
        private DataGridView analisisTabla = new DataGridView();
        public int indexAnalisis;

        public string Name
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string nameTabla
        {
            get { return nombretabla; }
            set { nombretabla = value; }
        }

        public int ColumnCount
        {
            get { return colum_count; }
            set { colum_count = value; }
        }

        public List<List<String>> examen
        {
            get { return examenes; }
            set { examenes = value; }
        }

        public bool Especial { get { return especial; } set { especial = value; } }

        public virtual void fillTable() { }

        public override string ToString() { return nombre; }

        public DataGridView tablaAnalisis
        {
            get { return analisisTabla; }

            set
            {
                analisisTabla = value;

                tablaAnalisis.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(tablaAnalisis_CellEndEdit);

                fillTable();
            }
        }

        private void tablaAnalisis_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            indexAnalisis = e.RowIndex;
            updateCells();


        }

        public virtual void updateCells() { }

        public DataGridViewCell buscarCell(string valor)
        {

            foreach (DataGridViewRow row in tablaAnalisis.Rows)
            {

                if (row.Cells[0].Value.ToString().Equals(valor))
                {
                    return row.Cells[1];

                }

            }
            return null;
        }

        public Analisis Clone()
        {
            return (Analisis)this.MemberwiseClone();
        }

        public DataGridViewComboBoxCell ComboPOSNEG()
        {
            DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Items.Add("NEGATIVO");
            cmb.Items.Add("POSITIVO");
            cmb.Value = "NEGATIVO";
            return cmb;
        }

    }

    class Biometria_Hematica : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
    "RBC",
    "HGB",
    "HCT",
    "VCM",
    "HCM",
    "MCHC",
    "PLT",
    "WBC",
    "N SEG",
    "LY",
    "MO",
    "EO",
    "BA",
    "BANDAS"
        }, 
        
    new List<String> {  
    "RBC",
    "HGB",
    "HCT",
    "VCM",
    "HCM",
    "MCHC",
    "PLT",
    "WBC",
    "NSEG",
    "LY",
    "MO",
    "EO",
    "BA",
    "BANDAS"
        }
        };


        private List<string> unidades = new List<String>{ 
    "10^6/uL",
    "g/dl",
    "%",
    "fL",
    "pg",
    "g/dl",
    "10^3/uL",
    "10^3/uL",
    "%",
    "%",
    "%",
    "%",
    "%",
    "%"

  
              };

        private List<string> rangos_h = new List<String>{ 
    "3,7 - 4,7",
    "14,0 - 17",
    "42,0 - 47,0",
    "80,0 - 100,0",
    "27,0 - 32,0",
    "32,0 -- 37,0",
    "150 -- 400",
    "4,5 -- 10,5",
    "20 -- 70",
    "20 -- 30",
    "0,0 -- 10",
    "2,0 -- 6,0",
    "0,0 -- 1,0",
    "0,0 -- 2,0"

              };

        private List<string> rangos_m = new List<String>{ 

    "3,7 - 4,7",
    "12,0 - 16",
    "36,0 - 42,0",
    "80,0 - 100,0",
    "27,0 - 32,0",
    "32,0 -- 37,0",
    "150 -- 400",
    "4,5 -- 10,5",
    "20 -- 70",
    "20 -- 30",
    "0,0 -- 10,0",
    "2,0 --6,0",
    "0,0 -- 4,0",
    "0,0 -- 6,0"


              };

        #endregion

        public Biometria_Hematica()
        {
            ColumnCount = 5;
            nameTabla = "bh";
            Name = "Biometría Hemática";
            examen = this.examenList;

        }

        public override void fillTable()
        {

            //tablaAnalisis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbdir_KeyDown);
            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;

            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }


            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rango H";
            tablaAnalisis.Columns[4].HeaderText = "Rango M";


            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rangos_h[j], this.rangos_m[j]);
            }

            tablaAnalisis.Rows[0].Cells[1].ToolTipText = "(HTC/10)+0.6"; //RBC
            tablaAnalisis.Rows[1].Cells[1].ToolTipText = "(HTC/3)"; //HGB
            tablaAnalisis.Rows[3].Cells[1].ToolTipText = "(HTC/RBC)*10"; //VCM
            tablaAnalisis.Rows[4].Cells[1].ToolTipText = "(HGB/RBC)*10"; //HCM
            tablaAnalisis.Rows[5].Cells[1].ToolTipText = "33 + (HTC/50)"; //MCHC

            tablaAnalisis.Rows[0].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[1].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[3].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[4].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[5].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;

        }

        public override void updateCells()
        {
            try
            {

                if (tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("HCT"))
                {

                    float HCT = float.Parse(buscarCell("HCT").Value.ToString());
                    float RBC = 0;
                    float HGB = 0;

                    RBC = (HCT / 10) + 0.6f;
                    buscarCell("RBC").Value = Math.Truncate(RBC * 100) / 100;

                    HGB = HCT / 3;
                    buscarCell("HGB").Value = Math.Truncate(HGB * 100) / 100;

                    buscarCell("VCM").Value = Math.Truncate((HCT / RBC) * 10 * 100) / 100;

                    buscarCell("HCM").Value = Math.Truncate((HGB / RBC) * 10 * 100) / 100;

                    buscarCell("MCHC").Value = Math.Truncate((33 + (HCT / 50)) * 100) / 100;

                }

            }

            catch { }
        }
    }

    class Multiquimica : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
           "GLUCOSA",		
           "COLESTEROL",		
           "TRIGLICERIDOS"	,	
            "UREA"		,
            "BUN"	,	
            "CREATININA"	,	
            "AC. URICO"	,	
            "HDL COL."	,	
            "LDL COL."	,	
            "VLDL"  	,	
            "INDICE ATEROGENICO"	,	
            "T. G. O."		,
            "T. G. P."		,
            "BILIRRUBINA T."	,	
            "BILIRRUBINA D."	,	
            "BILIRRUBINA I."	,	
            "PROTEINAS T."	,	
            "F. ALCALINA"	,	
            "ALBUMINA"		,
            "GLOBULINA"	,	
            "RELACION A/G"	,	
            "CALCIO",
 		    "FOSFORO",
            "SODIO",
            "POTASIO",
            "CLORO"
        }, 
        
    new List<String> {  
             "GLUCOSA",		
            "COLESTEROL",		
            "TRIGLICERIDOS"	,	
            "UREA"		,
            "BUN"	,	
            "CREATININA"	,	
            "acido_urico"	,	
            "HDL"	,	
            "LDL"	,	
            "VLDL"  	,	
            "INDICE_ATEROGENICO"	,	
            "tgo"		,
            "tgp"		,
            "bilirrubina_t"	,	
            "BILIRRUBINA_d"	,	
            "BILIRRUBINA_I"	,	
            "PROTEINAS_T"	,	
            "F_ALCALINA"	,	
            "ALBUMINA"		,
            "GLOBULINA"	,	
            "RELACION_AG"	,	
            "CALCIO" 	,
            "FOSFORO",
            "SODIO",
            "POTASIO",
            "CLORO"
    }
        };


        private List<String> unidades = new List<String> { 
        
            "mg %"	,
            "mg %"	,
            "mg %"	,
            "mg %"	,
            "mg %"	,
            "mg %"	,
            "mg %"	,
            "mg %"	,
            "mg %"	,
            "mg %"	,
            "-",
            "U/L",
            "U/L",
            "mg %",
            "mg %",
            "mg %",
            "g/dl",
            "U/L",
            "mg %",	
            "mg %"	,
            "mg %",
            "mg/dl",
            "mg/dl",
            "Mmol/L",
            "Mmol/L",
            "Mmol/L"


          
          


        
        };

        private List<String> rango = new List<String> { 
        
            "70 -- 105",
            "hasta 200",
            "hasta 150",
            "10 -- 50",
            "7 -- 25",
            "0.6 -- 1.1",
            "3.5  --  7.2",
            "30 -- 85",
            "76 -- 218",
            "7--38",
            "menor R(0--4), medio R(4--6), mayor R(>6)",
            "0 -- 37",
            "0 -- 40",
            "hasta 1.2",
            "hasta 0.25",
            "hasta 0.7",
            "6.6 -- 8.3",
            "38 -- 126",
            "2.5 -- 4.5",
            "2.0 -- 4.4",
            "1.0 -- 2.0",
            "8.4 -- 10.2",
            "2.5 -- 4.5",
            "137 -- 145",
            "3.5 -- 5.1",
            "98 -- 107"

        
        };

        #endregion

        public Multiquimica()
        {
            ColumnCount = 4;
            nameTabla = "mq";
            Name = "Multiquímica";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rango";



            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rango[j]);
            }

            tablaAnalisis.Rows[4].Cells[1].ToolTipText = "UREA/2.14"; //BUN
            tablaAnalisis.Rows[8].Cells[1].ToolTipText = "COLESTEROL - HDL COL"; //LDL COL 
            tablaAnalisis.Rows[9].Cells[1].ToolTipText = "TRIGLICERIDOS/5"; //VL DL
            tablaAnalisis.Rows[10].Cells[1].ToolTipText = "COLESTEROL/ HDL COL"; //INDICE ATERGENICO
            tablaAnalisis.Rows[15].Cells[1].ToolTipText = "BILIRUBINA T. - BILIRUBINA D."; //BILIRRUBINA INDIRECTA
            tablaAnalisis.Rows[19].Cells[1].ToolTipText = "PROTEINAS TOTALES - ALBUMINA"; // GLOBULINA
            tablaAnalisis.Rows[20].Cells[1].ToolTipText = "ALBUMINA / GLOBULINA"; //RELACION A/G

            tablaAnalisis.Rows[4].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[8].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[9].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[10].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[15].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[19].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;
            tablaAnalisis.Rows[20].Cells[1].Style.BackColor = System.Drawing.Color.Wheat;


        }

        public override void updateCells()
        {
            try
            {

                if (tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("UREA"))
                {
                    float urea = 0;
                    urea = float.Parse(buscarCell("UREA").Value.ToString());
                    buscarCell("BUN").Value = Math.Truncate((urea / 2.14) * 100) / 100;
                }


                if (tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("COLESTEROL") ||
                    tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("HDL COL."))
                {
                    float colesterol = 0;
                    float hdlcol = 0;
                    colesterol = float.Parse(buscarCell("COLESTEROL").Value.ToString());
                    hdlcol = float.Parse(buscarCell("HDL COL.").Value.ToString());
                    buscarCell("LDL COL.").Value = Math.Truncate((colesterol - hdlcol) * 100) / 100;
                }

                if (tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("TRIGLICERIDOS"))
                {
                    float trigliceridos = 0;
                    trigliceridos = float.Parse(buscarCell("TRIGLICERIDOS").Value.ToString());
                    buscarCell("VLDL").Value = Math.Truncate((trigliceridos / 5) * 100) / 100;
                }

                if (tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("COLESTEROL") ||
                     tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("HDL COL."))
                {
                    float colesterol = 0;
                    float hdlcol = 0;
                    colesterol = float.Parse(buscarCell("COLESTEROL").Value.ToString());
                    hdlcol = float.Parse(buscarCell("HDL COL.").Value.ToString());
                    buscarCell("INDICE ATEROGENICO").Value = Math.Truncate((colesterol / hdlcol) * 100) / 100;
                }

                if (tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("BILIRRUBINA T.") ||
                    tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("BILIRRUBINA D.")
                    )
                {

                    float bilirrubinat = 0;
                    float bilirrubinad = 0;
                    bilirrubinat = float.Parse(buscarCell("BILIRRUBINA T.").Value.ToString());
                    bilirrubinad = float.Parse(buscarCell("BILIRRUBINA D.").Value.ToString());
                    buscarCell("BILIRRUBINA I.").Value = Math.Truncate((bilirrubinat - bilirrubinad) * 100) / 100;

                }

                if (tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("PROTEINAS T.") ||
                    tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("ALBUMINA"))
                {
                    float proteinast = 0;
                    float albumina = 0;
  
                    proteinast = float.Parse(buscarCell("PROTEINAS T.").Value.ToString());
                    albumina = float.Parse(buscarCell("ALBUMINA").Value.ToString());
                        buscarCell("GLOBULINA").Value = Math.Truncate((proteinast - albumina) * 100) / 100;

                }

                if (tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("ALBUMINA") ||
                    tablaAnalisis.Rows[indexAnalisis].Cells[0].Value.Equals("GLOBULINA"))
                {
                    float albu = 0;
                    float globu = 0;

                    albu = float.Parse(buscarCell("ALBUMINA").Value.ToString());
                    globu = float.Parse(buscarCell("GLOBULINA").Value.ToString());
                    buscarCell("RELACION A/G").Value = Math.Truncate((albu/globu) * 100) / 100;

                }
            }
            catch { }
            //try
            //{
            //    String seleccionado = ultimo;
            //    float urea = 0;
            //    float colesterol = 0;
            //    float trigliceridos = 0;
            //    float hdlcol = 0;
            //    float bilirrubinat = 0;
            //    float bilirrubinad = 0;
            //    float proteinast = 0;
            //    float albumina = 0;
            //    float globulina = 0;

            //        try { urea = float.Parse(buscarCell("UREA").Value.ToString()); }
            //        catch { }
            //        try { colesterol = float.Parse(buscarCell("COLESTEROL").Value.ToString()); }
            //        catch { }
            //        try { trigliceridos = float.Parse(buscarCell("TRIGLICERIDOS").Value.ToString()); }
            //        catch { }
            //        try { hdlcol = float.Parse(buscarCell("HDL COL.").Value.ToString()); }
            //        catch { }
            //        try { bilirrubinat = float.Parse(buscarCell("BILIRRUBINA T.").Value.ToString()); }
            //        catch { }
            //        try { bilirrubinad = float.Parse(buscarCell("BILIRRUBINA D.").Value.ToString()); }
            //        catch { }
            //        try { proteinast = float.Parse(buscarCell("PROTEINAS T.").Value.ToString()); }
            //        catch { }
            //        try { albumina = float.Parse(buscarCell("ALBUMINA").Value.ToString()); }
            //        catch { }
            //        try { globulina = float.Parse(buscarCell("GLOBULINA").Value.ToString()); }
            //        catch { }

            //        buscarCell("BUN").Value = Math.Truncate((urea / 2.14) * 100) / 100;

            //        buscarCell("LDL COL.").Value = Math.Truncate((colesterol - hdlcol) * 100) / 100;

            //        buscarCell("VLDL").Value = Math.Truncate((trigliceridos / 5) * 100) / 100;

            //        buscarCell("INDICE ATEROGENICO").Value = Math.Truncate((colesterol / hdlcol) * 100) / 100;
            //        if (hdlcol == 0)
            //        {
            //            buscarCell("INDICE ATEROGENICO").Value = "0";
            //        }

            //        buscarCell("BILIRRUBINA I.").Value = Math.Truncate((bilirrubinat - bilirrubinad) * 100) / 100;

            //        buscarCell("GLOBULINA").Value = Math.Truncate((proteinast - albumina) * 100) / 100;

            //buscarCell("RELACION A/G").Value = Math.Truncate((albumina / globulina) * 100) / 100;
            //if (globulina == 0)
            //{
            //    buscarCell("RELACION A/G").Value = "0";
            //}


            //    }

            //    catch { }
        }
    }

    class P_Tiroideo : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
     "TSH",		
            "T4",		
            "FT4"	,	
            "T3"	,	
            "FT3"	,	
            "FT1"	,	
            "TC"	,	
            "YP"		
	
        }, 
        
    new List<String> {  
    "TSH",		
            "T4",		
            "FT4"	,	
            "T3"	,	
            "FT3"	,	
            "FT1"	,	
            "TC"	,	
            "YP"		
          }
        };


        private List<String> unidades = new List<String> { 
        
            "u/UI/ml",	
            "U/gr/dl",	
            "ng/dl",	
            "ng/dl",	
            "pg/ml",	
            "Ug/dl",	
            "U/Cap",	
            "Ug/dl"	

        };

        private List<String> rango = new List<String> { 
        
            "0,4 -- 6.0",
            "4.8 -- 12.0",
            "0.8 -- 2.0",
            "0.6 -- 2.1",
            "1.8 -- 4.7",
            "5.0 -- 12",
            ".72 -- 1.24",
            "4.0 -- 8.0"
        };

        #endregion

        public P_Tiroideo()
        {
            ColumnCount = 4;
            nameTabla = "ptiroideo";
            Name = "Perfil Tiroideo";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rango";


            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rango[j]);
            }


        }
    }

    class Coprologico : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
            "COLOR",       				
            "ASPECTO", 				
            "PH",				
            "C.CETONICOS",				
            "C.REDUCTORES"	,			
            "SANGRE OCULTA"	,			
            "SANGRE VISIBLE",				
            "ALMIDON"	,			
            "AMIBA EN FRESCO"	,			
            "DIFERENCIAL"	,			
            "LEVADURAS"	,			
            "COPRO"		,		
            "MOCO"		,
	        "LEUCOCITOS"
        }, 
        
    new List<String> {  
    "color",
    "aspecto",
    "ph",
    "cetonicos",
    "reductores",
    "sangre_oculta",
    "sangre_visible",
    "almidon",
    "amiba",
    "diferencial",
    "levaduras",
    "copro",
    "moco",
    "leucocitos"
        }
        };

        #endregion

        public Coprologico()
        {
            ColumnCount = 2;
            nameTabla = "copro";
            Name = "Coprológico";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }


            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "I";


            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");
            }


        }


    }

    class Coprologico3 : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
            "COLOR",       				
            "ASPECTO", 				
            "PH",				
            "C.CETONICOS",				
            "C.REDUCTORES"	,			
            "SANGRE OCULTA"	,			
            "SANGRE VISIBLE",				
            "ALMIDON"	,			
            "AMIBA EN FRESCO"	,			
            "DIFERENCIAL"	,			
            "LEVADURAS"	,			
            "COPRO"		,		
            "MOCO"	,
		    "LEUCOCITOS"
        }, 
        
    new List<String> {  
    "color",
    "aspecto",
    "ph",
    "cetonicos",
    "reductores",
    "sangre_oculta",
    "sangre_visible",
    "almidon",
    "amiba",
    "diferencial",
    "levaduras",
    "copro",
    "moco",
    "leucocitos",
    "color2",
    "aspecto2",
    "ph2",
    "cetonicos2",
    "reductores2",
    "sangre_oculta2",
    "sangre_visible2",
    "almidon2",
    "amiba2",
    "diferencial2",
    "levaduras2",
    "copro2",
    "moco2",
    "leucocitos2",
    "color3",
    "aspecto3",
    "ph3",
    "cetonicos3",
    "reductores3",
    "sangre_oculta3",
    "sangre_visible3",
    "almidon3",
    "amiba3",
    "diferencial3",
    "levaduras3",
    "copro3",
    "moco3",
    "leucocitos3"
        }
        };

        #endregion

        public Coprologico3()
        {
            ColumnCount = 4;
            nameTabla = "copro3";
            Name = "Coprológico III";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[2].ReadOnly = false;
            tablaAnalisis.Columns[3].ReadOnly = false;

            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "I";
            tablaAnalisis.Columns[2].HeaderText = "II";
            tablaAnalisis.Columns[3].HeaderText = "III";



            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", "", "");
            }

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            tablaAnalisis.Columns[2].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            tablaAnalisis.Columns[3].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico


        }


    }

    class Coproparasitoscopico : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
            
    "COPRO I",       		
    "COPRO II", 		
    "COPRO III"	
 
        }, 
        
    new List<String> {  
    "copro1",
    "copro2",
    "copro3",
        }
        };



        #endregion

        public Coproparasitoscopico()
        {
            ColumnCount = 2;
            nameTabla = "copropara";
            Name = "Coproparasitoscópico";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");
            }
        }
    }

    class EGO : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
    "COLOR",       		
    "ASPECTO", 		
    "DENSIDAD",		
    "PH",		
    "ALBUMINA",		
    "GLUCOSA",		
    "CETONA",		
    "BILIRRUBINA",		
    "LEUCOCITOS",		
    "HEMOGLOBINA",		
    "CELULAS EPITELIALES",	
	"BACTERIAS",	
	"LEUCOClTOS",	
	"ERITROCITOS",	
	"CRISTALES"	,
	"LEVADURAS"	,
	"F. MUCINA"	,
	"OTROS"	
        }, 
        
    new List<String> {  
    "color",       		
    "aspecto", 		
    "densidad",		
    "ph",		
    "albumina",		
    "glucosa",		
    "cetona",		
    "bilirrubina",		
    "leucocitos",		
    "hemoglobina",		
    "celulas_epiteliales",	
	"bacterias",	
	"leucocitos2",	
	"eritrocitos",	
	"cristales"	,
	"levaduras"	,
	"f_mucina"	,
	"otros"	
        }
        };

        #endregion

        public EGO()
        {
            ColumnCount = 2;
            nameTabla = "ego";
            Name = "Examen Gral. de Orina";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";


            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                if (j == 12 || j == 13)
                {
                    tablaAnalisis.Rows.Add(this.examenList[0][j], "p/c");
                }
                else
                {
                    tablaAnalisis.Rows.Add(this.examenList[0][j], "");
                }
            }



            tablaAnalisis.Rows[0].Cells[1] = combCOLOR();
            tablaAnalisis.Rows[1].Cells[1] = combASPECTO();
            for (int i = 4; i < 10; i++)
            {
                tablaAnalisis.Rows[i].Cells[1] = combPOSNEG();
            }
            tablaAnalisis.Rows[10].Cells[1] = combCANT();
            tablaAnalisis.Rows[11].Cells[1] = combCANT();
            tablaAnalisis.Rows[15].Cells[1] = combCANT1();
            tablaAnalisis.Rows[16].Cells[1] = combCANT1();

        }
        public DataGridViewComboBoxCell combCOLOR()
        {
            DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Items.Add("AMARILLO");
            cmb.Items.Add("VOGUEL I");
            cmb.Items.Add("VOGUEL II");
            cmb.Items.Add("VOGUEL III");
            cmb.Value = "AMARILLO";
            return cmb;
        }
        public DataGridViewComboBoxCell combPOSNEG()
        {
            DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Items.Add("NEGATIVO");
            cmb.Items.Add("POSITIVO(X)");
            cmb.Items.Add("POSITIVO(XX)");
            cmb.Items.Add("POSITIVO(XXX)");
            cmb.Value = "NEGATIVO";
            return cmb;
        }

        public DataGridViewComboBoxCell combASPECTO()
        {
            DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Items.Add("TRANSPARENTE");
            cmb.Items.Add("TURBIO");
            cmb.Items.Add("LIGERAMENTE TURBIO");
            cmb.Value = "TRANSPARENTE";
            return cmb;
        }

        public DataGridViewComboBoxCell combCANT()
        {
            DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Items.Add("ESCASAS");
            cmb.Items.Add("MODERADAS");
            cmb.Items.Add("ABUNDANTES");
            cmb.Value = "ESCASAS";
            return cmb;
        }

        public DataGridViewComboBoxCell combCANT1()
        {
            DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Items.Add("NEGATIVO");
            cmb.Items.Add("ESCASA");
            cmb.Items.Add("MODERADA");
            cmb.Items.Add("ABUNDANTE");
            cmb.Value = "NEGATIVO";
            return cmb;
        }
    }

    class ExPrenup : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
      "GRUPO SANGUINEO",	
        "FACTOR RH",	
        "V.D.R.L.",	
        "H.I.V."
        }, 
        
    new List<String> {  
     "grupo_sanguineo",	
        "factor_rh",	
        "vdrl",	
        "hiv"
        }
        };



        #endregion

        public ExPrenup()
        {
            ColumnCount = 2;
            nameTabla = "expre";
            Name = "Examen Prenupcial";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");
            }
        }
    }

    class DAbuso : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
        "MARIHUANA CANABINOIDES (THC)",	
        "COCAÍNA (COC)",	
        "M-ANFETAMINAS (MAMP)",		
        "ANFETAMINAS (AMP)",	
        "HEROÍNA  (OPC)"		

        }, 
        
    new List<String> {  
    "marihuana",
    "cocaina",
    "meth",
    "anfetaminas",
    "opc",
        }
        };

        #endregion

        public DAbuso()
        {
            ColumnCount = 2;
            nameTabla = "dabuso";
            Name = "Drogas de Abuso";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");
            }
        }
    }

    class Glicohemoglobina : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
        "GLHB",
        " ",
        " ",
        " "
        }, 
        
    new List<String> {  
    "glbh",
        }
        };


        private List<string> rangos = new List<String>{ 
     "NORMAL      6,0 - 8,2",
        "BUEN C,     7,5 - 9,0",
        "REGULAR     C. 9,0 - 10,0",
        "POBRE C.    > A 10,0"

              };



        #endregion

        public Glicohemoglobina()
        {
            ColumnCount = 3;
            nameTabla = "glh";
            Name = "Glicohemoglobina";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultados";
            tablaAnalisis.Columns[2].HeaderText = "Rangos";


            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.rangos[j]);
            }
        }
    }

    class PCardiaco : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
       "CPK-TOTAL",	
        "CPK-MB",	
        "TGO",	
        "DHL"	
	
        }, 
        
    new List<String> {  
    "cpk",	
        "cpkmb",	
        "tgo",	
        "dhl"	
        }
        };


        private List<string> unidades = new List<String>{ 
      "U/L",	
    "U/L",	
    "U/L",	
    "U/L"	
  
              };

        private List<string> rangos_h = new List<String>{ 
   "30 -- 135",	
    "< A 24",	
    "< A 40",	
    "313 -- 618",	
	
              };



        #endregion

        public PCardiaco()
        {
            ColumnCount = 4;
            nameTabla = "pcardiaco";
            Name = "Perfil Cardiaco";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }


            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Valores de Referencia";



            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rangos_h[j]);
            }
        }

    }

    class PHierro : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
        "HIERRO SERICO",			
			
        "CAPACIDAD DE FIJACIÓN DE HIERRO",			
			
        "PORCIENTO DE SATURACIÓN",
			
        "FERRITINA"		

	
        }, 
        
    new List<String> {  
   
	"hierro_serico",			
			
        "capacidad_fijacion",			
			
        "porciento_sat",
			
        "ferritina"		

        }
        };


        private List<string> unidades = new List<String>{ 
    
	 "ug/L",	
	
    "ug/L",	
	
    "%",	
	
    "ng/ml"	
	
              };

        private List<string> rangos_h = new List<String>{ 
     "(41-132)",	
	
    "(250-425)",	
	
    "(15-50)",	
	
    "(70-435)"	
	
              };



        #endregion

        public PHierro()
        {
            ColumnCount = 4;
            nameTabla = "phierro";
            Name = "Perfil de Hierro";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }


            tablaAnalisis.Columns[0].HeaderText = "Parámetro";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rangos";

            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 300;

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rangos_h[j]);
            }
        }

    }

    class PLipidos : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
        "GLUCOSA",		
        "COLESTEROL",		
        "TRIGLICERIDOS",		
        "HDL C",		
        "LDL C",		
        "VLDLC",		
        "INDICE ATEROGENICO"	
        }, 
        
    new List<String> {  
   
	    "glucosa",	
        "colesterol",		
        "trigliceridos",
        "hdl",
		"ldl",		
        "vldl",		
        "indice_aterogenico"
        }
        };


        private List<string> unidades = new List<String>{ 

        "mg %",
        "mg%",
        "mg%",
        "mg%",
        "mg/dl",
        "mg/dl",
        "mg/dl"
              };

        private List<string> rangos = new List<String>{ 

        "70 -- 105",	
        "(hasta 200)",	
        "35 -- 150",
        "30 -- 75",
        "76 -- 318",
        "7 -- 32",
        "Rmen (0-4)Rmed(4-6)Ralto(>6)"	
              };

        #endregion

        public PLipidos()
        {
            ColumnCount = 4;
            nameTabla = "plipidos";
            Name = "Perfil de Lípidos";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rangos";



            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rangos[j]);
            }
        }

    }

    class PReumatoide : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
       "FACTOR REUMATOIDE",	
        "V.S.G.",
        "PROTEINA C REACTIVA",	
        "ANTIESTREPTOLISINAS"	
        }, 
        
    new List<String> {  
   "factor_reumatoide",	
        "vsg",
        "prot_c_reac",	
        "antiestreptolisinas"
        }
        };


        private List<string> unidades = new List<String>{ 
        "UI/ml",
         "mm3/hr",
        "UI/ml",
         "UI/ml"
              };

        private List<string> rangos = new List<String>{ 
         "0.0 - 8.0",	
         "0.0 - 8.0",	
         "0.0 - 6.0",	
         "50 - 200"	

              };
        #endregion

        public PReumatoide()
        {
            ColumnCount = 4;
            nameTabla = "preumatoide";
            Name = "Perfil Reumatoide";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rangos";

            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 180;

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rangos[j]);
            }
        }

    }

    class Plaquetas : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
          "PLAQUETAS",
        " ",
        " ",
        " "
        }, 
        
    new List<String> {  
   "plaquetas"
        }
        };


        private List<string> unidades = new List<String>{ 
         "10^3/uL"	,
          " ",
        " ",
        " "
              };

        private List<string> ValRef = new List<String>{ 
         "150 -- 400",
         " ",
        " ",
        " "

              };
        #endregion

        public Plaquetas()
        {
            ColumnCount = 4;
            nameTabla = "plaquetas";
            Name = "Plaquetas";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Valores de Referencia";

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.ValRef[j]);
            }
        }

    }

    class CGHC : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
          "CUANTIFICACION",	
            "DE HORMONA",
	        "GONADOTROPICA",
            "CORIONICA"	,
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " "
        }, 
        
    new List<String> {  
   "gonadotropica"
        }
        };


        private List<String> unidades = new List<String> { 
        
            "mUI/ml",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        
        };

        private List<String> rango = new List<String> { 
        
                "HOMBRES Y",
                "MUJERES NO",
                "EMBARAZADAS",
                "< 5.0",
                "MUJERES",
                "EMBARAZADAS",
                "SEMANAS",
                "1.3 - 2 16 - 156",
                "2 - 3 101 - 4870",
                "3 - 4 1110 - 31500",
                "4 - 5 2560 - 82300",
                "5 - 6 23100 - 151000",
                "6 - 7 27300 - 233000",
                "7 - 11 20900 - 291000",
                "11 - 16 6140 - 103000",
                "16 - 21 4720 - 80100",
                "21 - 39 2700 - 78100"

        };
        #endregion

        public CGHC()
        {
            ColumnCount = 4;
            nameTabla = "pembarazo";
            Name = "Cuantificación de HGC";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rango";

            tablaAnalisis.Columns[1].Width = 30;
            tablaAnalisis.Columns[2].Width = 30;

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rango[j]);
            }
        }

    }

    class T_Sanguineo : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
            "GRUPO SANGUINEO",       		
           "FACTOR RH",
           " ",
           " "
        }, 
        
    new List<String> {  
     "gsang",       		
           "fact_rh"
        }
        };

        #endregion

        public T_Sanguineo()
        {
            ColumnCount = 2;
            nameTabla = "tsang";
            Name = "Tipeo Sanguíneo";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");
            }
        }

    }

    class T_Coagulacion : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
       
            "T. DE SANGRADO",	
            "T. DE COAGULACION"	,
            "T. DE PROTOMBINA",	
            "T. P. TROMBOPLASTINA",	
            "PLAQUETAS"	

        }, 
        
    new List<String> {  
     "tsang",       		
           "tcoag",
           "tprot",
           "ptromb",
           "plq"
        }
        };

        private List<string> unidades = new List<String>{ 
            "SEGUNDOS"	,
            "MINUTOS"	,
            "SEGUNDOS"	,
            "SEGUNDOS"	,
            "10^3/ul"	

              };

        private List<string> ValRef = new List<String>{ 
            "0 -- 40"	,
            "4 -- 8"	,
            "10 -- 14"	,
            "30  ---  36"	,
            "150  --  400"	

              };
        #endregion

        public T_Coagulacion()
        {
            ColumnCount = 4;
            nameTabla = "tcoag";
            Name = "Tiempos de Coagulación";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Valores de Referencia";


            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 180;

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.ValRef[j]);
            }
        }

    }

    class PEmbarazo : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {     
       
             "P. EMBARAZO",
        " ",
        " ",
        " "

        }, 
        
    new List<String> {  
     "prueba"       		
           
        }
        };


        #endregion

        public PEmbarazo()
        {
            ColumnCount = 2;
            nameTabla = "pembarazo2";
            Name = "Prueba Inmunológica de Embarazo en Sangre";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";



            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 180;

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");
            }


            tablaAnalisis.Rows[0].Cells[1] = ComboPOSNEG();
        }

    }

    class DCO24 : Analisis
    {

        #region Listas con los datos del análisis


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
       "DEPURACIÓN DE CRETININA EN ORINA EN 24HRS",	
            "CREATININA EN ORINA"	,
            "CREATININA EN SUERO",	
            "VOLUMEN",	
            "VALOR NORMAL",	
            "ANTÍGENO PROSTÁTICO ESPECÍFICO"	


        },  
    new List<String> {  
    "dep24",	
            "creatori"	,
            "creatsuero",	
            "vol",	
            "valnor",	
            "ape"	
      		
           
        }
        };

        private List<string> unidades = new List<String>{ 
            "ml/min"	,
            "mg/dl"	,
            "mg/dl"	,
            "ml"	,
            "ml/min",
	        "ng/ml"

              };

        private List<string> ValRef = new List<String>{ 
            ""	,
            ""	,
            ""	,
            ""	,
            ""	,
             "N(0 -- 4)"	

	

              };


        #endregion

        public DCO24()
        {
            ColumnCount = 4;
            nameTabla = "dcreat";
            Name = "Depuración de Creatinina";
            examen = this.examenList;
        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rango";


            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 340;
            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.ValRef[j]);

            }
        }

    }

    class PHepatico : Analisis
    {
        #region listas

        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
      
        "T.G.O.",		
        "T.G.P.",		
        "BILIRUBINA TOTAL",		
        "BILIRRUBINA DIRECTA",		
        "BILIRRUBINA INDIRECTA",		
        "F. ALKALINA",		
        "D.H.L.",
		"G.G.T.P."	
	

        },  
    new List<String> {  
    
        "tgo",		
        "tgp",		
        "biltot",		
        "bildir",		
        "bilindir",		
        "falkalina",		
        "dhl",
		"ggtp"	

    }
        };



        private List<string> unidades = new List<String>{ 

        "U/L",
        "U/L",
        "mg/dl",
        "mg/dl",
        "mg/dl",
        "U/L",
        "U/L",
        "U/L"
	

              };

        private List<string> rangos = new List<String>{ 

        "0 -- 40",	
        "0 -- 45",	
        "0 -- 1.3",
        "0 -- 0.5",
        "0 -- 0.8",
        "42 -- 128",
        "313 -- 618",
	    "12 -- 58"
	


              };
        #endregion
        public PHepatico()
        {
            nameTabla = "phepatico";
            ColumnCount = 4;
            Name = "Perfil Hepático";
            examen = this.examenList;
        }


        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = this.ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;

            }


            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Valor Normal";



            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rangos[j]);

            }

            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 180;
        }




    }

    class AHV : Analisis
    {
        #region listas


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
      
      "HEP VS \"A\"" + " (lgM)",		
        "HC VS AG \"S\"" + " HEP \"B\"",	
         "AC VS HEP \"C\"" + " VIRUS TOTALES"	
	

        },  
    new List<String> {  
    
        "A",		
        "B",	
         "C"	

    }
        };


        #endregion
        public AHV()
        {
            nameTabla = "hepat";
            ColumnCount = 2;
            Name = "Anti-Hepatitis Viral";
            examen = this.examenList;
        }


        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = this.ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;

            }


            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";



            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");

            }
        }




    }

    class RFebriles : Analisis
    {

        #region listas

        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
    
        "TIFICO \"O\"",
        "TIFICO \"H\"",
        "PARATIFICO \"A\"",
        "PARATIFICO \"B\"",
        "HUDDLESON",
        "PROTEUS \"OX19\"",
    
        },  
    new List<String> {  

        "o",
        "h",
        "a",
        "b",
        "huddle",
        "proteus",
        
    }
        };




        #endregion
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = this.ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;

            }



            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";


            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");

            }

        }

        public RFebriles()
        {
            nameTabla = "reac";
            ColumnCount = 2;
            Name = "Reacciones Febriles";
            examen = this.examenList;
        }

    }

    class Reticulocitos : Analisis
    {

        #region listas


        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
    
        "% RETICULOCITOS",
        "% RET. CORREGIDOS ",
        "VALOR ABSOLUTO",
        " "
    
        },  
    new List<String> {  

       "reticu",
        "correg",
        "valor"
        
        
    }
        };


        private List<string> unidades = new List<String>{ 
         "%"	,
          "%",
        "CEL/mm3",
        " "
              };

        private List<string> ValRef = new List<String>{ 
         "0.2 -- 2.0",
         "0.4 -- 2.4",
        "25,0 -- 75,0",
        " "

              };
        #endregion
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = this.ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }


            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Valores de Referencia";

            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 160;

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.ValRef[j]);

            }

        }

        public Reticulocitos()
        {
            nameTabla = "retic";
            ColumnCount = 4;
            Name = "Reticulocitos";
            examen = this.examenList;
        }

    }

    class Cultivo : Analisis
    {

        #region listas
        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
    
        " ",
        " ",
        " ",
         " ",
        "Medicamento",
        "AMIKACINA",
        "AMPICILINA",
        "CEFALOTINA",
        "CEFRIAXONA",
        "CLORANFENICOL",
        "DICLOXACILINA",
        "ENOXACINA",
        "ERITROMICINA",
        "GENTAMICINA",
        "IMIPENEN",
        "CEFOTAXIMA",
        "NITROFURANTOINA",
        "PENICILINA",
        "SULFAMETOXAZOL",
        "CIPROFLOXACINO",
        "NETILMICINA",
        "AMOXILINA/AC. CLAVULANICO",

        },  
    new List<String> {  
        "aislamiento",
        "frotis",
        "cantidad",
        "reng1",
        "reng2",
        "reng3",
        "reng4",
        "reng5",
        "reng6",
        "reng7",
        "reng8",
        "reng9",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
 
        }
        };


        private List<string> unidades = new List<String>{ 
         " ",
         " ",
         " ",
         " ",
         "Concentración",
         "30mgc",
         "10mgc",
         "30mgc",
         "30mgc",
         "30mgc",
         "1mgc",
         "10mgc",
         "15mgc",
         "10mgc",
         "10mgc",
         "10mgc",
         "30mcg",
         "30mcg",
         "25mcg",
         "25mcg",
         "30mcg",
         "30mcg"
              };

        private List<string> resultado = new List<String>{ 
         " ",
         " ",
         " ",
         " ",
         "Resultado",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         "",
         ""
              };

        #endregion
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = this.ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; ;
            }

            tablaAnalisis.Columns[0].HeaderText = "Aislamiento";
            tablaAnalisis.Columns[1].HeaderText = "Frotis";
            tablaAnalisis.Columns[2].HeaderText = "Cantidad";


            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 160;

            tablaAnalisis.Columns[2].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], this.unidades[j], this.resultado[j]);

            }


            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(tablaAnalisis.Font, System.Drawing.FontStyle.Bold);
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablaAnalisis.Rows[4].DefaultCellStyle = style;
            tablaAnalisis.Rows[4].ReadOnly = true;


        }

        public Cultivo()
        {
            nameTabla = "cultivo";
            ColumnCount = 3;
            Name = "Cultivo";
            examen = this.examenList;
        }

    }

    class TNeonatal : Analisis
    {
        #region Listas con los datos del análisis

        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
    
        "HORMONA ESTIMULANTE DE TIROIDES (TSHN)",		
            "TIROXINA (T4N)",		
            "17-HIDROXIPROGESTERONA"	,	
            "TRIPSINOGENO INMUNOREACTIVO"		,
            "GLUCOSA 6 FOSFATO DESHIDROGENESA"	,	
            "BIOTINIDASA"	,	
            "GALACTOSA"	,	
            " ",
            " ",
            "FENILALALINA"	,	
            "MSUD (CETOACIDURIA)"	,	
            "LEUCINA"  	,	
            "ISOLEUCINA"	,	
            "METIONINA"		,
            "VALINA"		,
            "ÁCIDO GUTÁMICO"	,	
            "TREONINA"	,	
            "GLICINA"	,	
            "SERINA"	,	
            "ÁCIDO ASPÁRGICO"	,	
            "ORNITINA"		,
            "LISINA"	,	
            "HISTIDINA"	,	
            "TIROSINA" ,		
            "ALANINA",
            "GLUTAMINA",
            "CITRULINA",
            "ARGININA",
            "CISTEINA",
            "COMP. LEUCINA/ISOLEUCINA",
            "COMP. METIONINA/VALINA",
            "TIROSINA ",
            "ALANINA ",
            "COMP. AC. GLUTÁMICO/TREONUNA",
            "COMP GLICINA/ SERINA/ AC. ASPÁRGICO",
            "GLUTAMINA ",
            "COMP. ORINITINA/ LISINA/ HISTIDINA",
            "ARGININA ",
            "CITRULINA "

        },  
    new List<String> {  
            "tshn",		
            "t4n",		
            "hidrog"	,	
            "trip"		,
            "glucosa"	,	
            "bioti"	,	
            "galac"	,	
            " ",
            " ",
            "fenila",
            "msud",
            "leu"	,	
            "isoleu"	,	
            "metio"  	,	
            "valina"	,	
            "guta"		,
            "treo"		,
            "glic"	,	
            "seri"	,	
            "aspar"	,	
            "orni"	,	
            "lisi"	,	
            "histi"		,
            "tiros"	,	
            "alan"	,	
            "glut" ,		
            "citru",
            "argi",
            "ciste",
            "comp",
            "comp2",
            "tiros2",
            "alan2",
            "comp3",
            "comp4",
            "glut2",
            "comp5",
            "argi2",
            "citru2"
        }
        };



        private List<String> unidades = new List<String> { 
        
            "uUl/ml",		
            "ug/dl",		
            "ng/ml"	,	
           "ng/ml"	,
            "U/g hb"	,	
            " "	,	
            "U/g hb"	,
            " ",
            " ",
	        "mg/dl"	,
            "mg/dl"	,
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            "umo/dl",
            "umo/dl",
            "umo/dl",
            "umo/dl",
            "umo/dl",
            "umo/dl",
            "umo/dl",
            "umo/dl",
            " ",
            " "
        };

        private List<String> normal = new List<String> { 
        
            "0,10 -- 10,0",
            "6,4 -- 22,0",
            "5,0 -- 90,0",
            "0,0 -- 90,0",
            "> 2,40",
            "NEGATIVO",
            "NEGATIVO: > 2,30",
            "POSITIVO: < 1,40",
            "ZONA GRIS: > 1,40 - 2,30",
            "< 2,1",
            "0,0 -- 5,25",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "NEGATIVO",
            "< 254,0",
            "< 250,0",
            "< 281,0",
            "< 468,0",
            "< 1144,0",
            "< 1101,0",
            "< 1100,0",
            "< 219,0",
            "NORMAL",
            "NORMAL"
        




        
        };

        private List<String> vmin = new List<String> { 
        
            "0,1",
            "6,4",
            "5",
            "0",
            "2,4",
            " ",
            " ",
            " ",
            " ",
            "0",
            "0",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            "0",
            "0",
            "0",
            "0",
            "0",
            "0",
            "0",
            "0",
            " ",
            " "
        };
        private List<String> vmax = new List<String> { 
        
            "10",
            "22",
            "90",
            "90",
            "100",
            " ",
            " ",
            " ",
            " ",
            "2,1",
            "5,25",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            "254",
            "250",
            "281",
            "468",
            "1144",
            "1101",
            "1100",
            "219",
            " ",
            " "
        };

        private List<String> kmin = new List<String> { 
        
            "0",
            "4",
            "0",
            "0",
            "1",
            " ",
            " ",
            " ",
            " ",
            "0",
            "0",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            "0",
            "0",
            "0",
            "0",
            "0",
            "0",
            "0",
            "0",
            " ",
            " "
        };

        private List<String> kmax = new List<String> { 
        
            "400",
            "100",
            "200",
            "1500",
            "400",
            " ",
            " ",
            " ",
            " ",
            "30",
            "30",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            " ",
            "400",
            "400",
            "420",
            "550",
            "1300",
            "1290",
            "1300",
            "325",
            " ",
            " "
        };
        #endregion


        public TNeonatal()
        {
            nameTabla = "tamiz";
            ColumnCount = 8;
            Name = "Tamiz Neonatal";
            examen = examenList;
        }

        public override void fillTable()
        {
            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;

            tablaAnalisis.Columns[0].HeaderText = "Analito";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidad";
            tablaAnalisis.Columns[3].HeaderText = "Normal";
            tablaAnalisis.Columns[4].HeaderText = "V Min";
            tablaAnalisis.Columns[5].HeaderText = "V Max";
            tablaAnalisis.Columns[6].HeaderText = "K Min";
            tablaAnalisis.Columns[7].HeaderText = "K Max";


            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 145;
            tablaAnalisis.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[1].Width = 90;
            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico

            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.normal[j], this.vmin[j], this.vmax[j],
                    this.kmin[j], this.kmax[j]);

            }
        }

    }

    class PGinecol : Analisis
    {
        #region listas

        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
    
         "Estradiol (E2)",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "H. ESTIMULANTE DE FOLÍCULO",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "HORMONA LUTEINIZANTE",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "PROGESTERONA",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "PROLACTINA",
        " "
	
			

        },  
    new List<String> {  
             "estradiol",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "estimulante",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "lute",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "proges",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "prolac",
        " "
	
			
        }
        };


        private List<string> unidades = new List<String>{ 

        "ng/ml",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "mUl/ml",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "mUl/ml",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "ng/ml",	
        " ", 
        " ", 
        " ", 
        " ",
        " ",
        "ng/ml",
        " "
	

              };

        private List<string> nombres = new List<String>{ 

        "FOLICULAR",	
        "OVULACIÓN", 
        "LUTEA", 
        "POST-MENOPÁUSICAS", 
        "HOMBRES",
        " ",
        "FOLICULAR",	
        "OVULATORIA", 
        "LUTEA", 
        "POST-MENOPÁUSICAS", 
        "HOMBRES",
        " ",
        "FOLICULAR",	
        "OVULATORIA", 
        "FASE LUTEA", 
        "POST-MENOPÁUSICAS", 
        "HOMBRES",
        " ",
        "FOLICULAR",	
        "OVULATORIA", 
        "LUTEA", 
        "POST-MENOPÁUSICAS", 
        "HOMBRES",
        " ",
        "HOMBRES",
        "MUJERES"
              };
        private List<string> rangos = new List<String>{ 

        "30 -- 10",	
        "100 -- 400", 
        "60 -- 150", 
        "0 -- 18", 
        "HASTA 60",
        " ",
        "7.5 -- 20",	
        "3.5 -- 10", 
        "1.3 -- 11", 
        "36.0 -- 138", 
        "1.0 -- 8.0",
        " ",
        "1.6 -- 15",	
        "21.9 -- 56", 
        "0.6 -- 16.30", 
        "15.0 -- 62.0", 
        "2.0 -- 12",
        " ",
        "0.15 -- 0.75",	
        "5.25 .. 38.63", 
        "2.0 -- 2.5", 
        "0.06 - 1.6", 
        "< 0.2 -- 3.37",
        " ",
        "3.8 -- 23.2",
        "3.28 -- 19.68"
              };
        #endregion
        public PGinecol()
        {
            nameTabla = "gineco";
            ColumnCount = 5;
            Name = "Perfil Ginecológico";
            examen = this.examenList;
        }


        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = this.ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;

            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 3) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;

            }


            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = " ";
            tablaAnalisis.Columns[4].HeaderText = "Valor Normal";



            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.nombres[j], rangos[j]);

            }
            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 230;
            tablaAnalisis.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[1].Width = 90;
            tablaAnalisis.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[2].Width = 90;
        }




    }

    class PProstatico : Analisis
    {
        #region listas

        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
    
        "ANTÍGENO PROSTÁTICO LIBRE",
		" ",
        " ",
        " ",
        "ANTÍGENO PROSTÁTICO ESPECÍFICO",		
        "RAZÓN DE PSA (LIBRE/TOTAL)",		

	
        },  
    new List<String> {  
              "apl",
		" ",
        " ",
        " ",
        "ape",		
        "psa",		

	
			
        }
        };



        private List<string> unidades = new List<String>{ 

        "ng/ml",
        "",
        "",
        "",
        "ng/ml",
        "%",

	

              };

        private List<string> rangos = new List<String>{ 

        "El riesgo de cáncer ",	
        "aumenta si la relación",	
        " entre PSA libre y PSA ",
        "total es menor a 25%",
        "0,0 -- 4.0",
        ""

              };


        private List<string> prob = new List<String>{ 
            "7,0",
            "52,0",
            "73,0",
            "93,0",
            "",
            ""
              };
        private List<string> razon = new List<String>{ 
            "0,0 -- 7,0",
            "7,0 -- 15,0",
            "16,0 -- 25,0",
            "> 25",
            "",
            ""
              };
        #endregion
        public PProstatico()
        {
            nameTabla = "prosta";
            ColumnCount = 6;
            Name = "Perfil Prostático";
            examen = this.examenList;
        }


        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = this.ColumnCount;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                //tablaAnalisis.Columns[j].ReadOnly = true;

            }


            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";
            tablaAnalisis.Columns[2].HeaderText = "Unidades";
            tablaAnalisis.Columns[3].HeaderText = "Rangos";
            tablaAnalisis.Columns[4].HeaderText = "Prob. de no tener cáncer";
            tablaAnalisis.Columns[5].HeaderText = "Razón PSA";
            tablaAnalisis.ColumnHeadersHeight = 60;

            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico
            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "", this.unidades[j], this.rangos[j], this.prob[j], this.razon[j]);

            }

            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 260;
            tablaAnalisis.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[3].Width = 160;


        }




    }

    class Esperma : Analisis
    {

        #region Listas con los datos del análisis



        private List<List<String>> examenList = new List<List<String>> { 
    
    new List<String> {        
    
        "PERIODO DE ABSTINENCIA: ",       		
           "HORA DE RECOLECCIÓN DE LA MUESTRA: ",
           "HUBO PERDIDA O DERRAME DE LA MUESTRA DURANTE LA RECOLECCIÓN: ",
           "HUBO ALGUNA DIFICULTAD PARA LOGRAR LA EYACULACIÓN: ",
           " ",
           "ASPECTO: ",
           "LICUEFACCIÓN: ",
           "CONSISTENCIA (VISCOCIDAD): ",
           "VOLUMEN (ml):",
           "PH:",
           "CONCENTRACIÓN (mlls de espermatozoides/ml):",
           "VIABILIDAD:",
           "MOTILIDAD:",
           "AGLUTINACIÓN:",
           "LEUCOCITOS:",
           "BACTERIAS:",
           "ERITROCITOS:",
           "CELULAS GERMINALES:",
           "CELULAS EPITELIALES:",
           "DETRITUS CELULARES:",
           "MORFOLOGÍA",
           "Normales",
           "MACROCÉFALO",
           "MICROCÉFALO",
           "DOBLES",
           "COLA",
           "ALFILER",
           "ACINTADOS",
           " ",
           "OBSERVACIONES"		

	
        },  
    new List<String> {  

           "periodo",       		
           "hora",
           "perdida",
           "dificultad",
           " ",
           "aspecto",
           "licuefaccion",
           "cons",
           "volumen",
           "ph",
           "conc",
           "viabilidad",
           "motilidad",
           "aglutinacion",
           "leucocitos",
           "bacterias",
           "eritrocitos",
           "germinales",
           "epiteliales",
           "detri",
           "morf",
           "macro",
           "micro",
           "dobles",
           "cola",
           "alfiler",
           "acint",
           " ",
           "obser"
        }
        };




        #endregion

        public Esperma()
        {
            nameTabla = "esperma";
            ColumnCount = 2;
            Name = "Espermatobioscopía";
            examen = this.examenList;

        }
        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            for (int j = 0; j < tablaAnalisis.ColumnCount; j++)
            {
                tablaAnalisis.Columns[j].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable; // impedimos que el usuario modifque el orden de la info
                if (j != 0) tablaAnalisis.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (j == 1) continue;
                tablaAnalisis.Columns[j].ReadOnly = true;
            }

            tablaAnalisis.Columns[0].HeaderText = "Examen";
            tablaAnalisis.Columns[1].HeaderText = "Resultado";

            tablaAnalisis.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablaAnalisis.Columns[0].Width = 520;
            tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico


            for (int j = 0; j < this.examenList[0].Count; j++)
            {
                tablaAnalisis.Rows.Add(this.examenList[0][j], "");
            }


            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(tablaAnalisis.Font, System.Drawing.FontStyle.Bold);
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = System.Drawing.Color.LightGray;


            DataGridViewCellStyle styles = new DataGridViewCellStyle();
            styles.Font = new Font(tablaAnalisis.Font, System.Drawing.FontStyle.Bold);
            styles.BackColor = System.Drawing.Color.LightGray;

            tablaAnalisis.Rows[21].DefaultCellStyle = style;
            tablaAnalisis.Rows[21].ReadOnly = true;
            tablaAnalisis.Rows[4].DefaultCellStyle = styles;
            tablaAnalisis.Rows[4].ReadOnly = true;
            tablaAnalisis.Rows[28].DefaultCellStyle = styles;
            tablaAnalisis.Rows[28].ReadOnly = true;

        }

    }

    class Especial : Analisis
    {

        private List<string> colnames;

        public Especial(int numcol, string nombre, List<string> colnames)
        {
            nameTabla = "";
            ColumnCount = numcol;
            Name = nombre;

            Especial = true;
            this.colnames = colnames;
        }


        public override void fillTable()
        {

            tablaAnalisis.ColumnCount = ColumnCount;
            tablaAnalisis.EditMode = DataGridViewEditMode.EditOnEnter;
            tablaAnalisis.AllowUserToAddRows = true;
            tablaAnalisis.RowCount = 5;
           // tablaAnalisis.RowHeadersVisible = true;
            //tablaAnalisis.Columns[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow; //cambiamos el color de una columna en específico


            for (int i = 0; i < tablaAnalisis.RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    tablaAnalisis.Rows[i].Cells[j].Value = "";
                }
            }

            for (int i = 0; i < ColumnCount; i++)
            {
                if (i != 0) tablaAnalisis.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tablaAnalisis.Columns[i].HeaderText = colnames[i];

            }

        }

    }
}