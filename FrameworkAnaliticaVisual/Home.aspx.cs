using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameworkAnaliticaVisual
{
    public partial class Home : System.Web.UI.Page
    {
        ///aqui si va las que se usan
        private List<string> ListaObjetosCadeConexion;
        private List<string> ListaTablas;
        private int ContadorDDLTablas;
        private List<clsControlesDDLTablas> ListaControlesDDLTablas;
        private List<string> ListaTablasConstraint;
        private List<string> ListaddlColumnCanvas;



        /// <summary>
        /// Revisar cuales se ocupan
        /// </summary>
        private List<string> ListaddlCanvas;
        private List<string> ListaddlTablas;
        private List<string> ListaEnlacesTablas;
        private DataTable dtCanvas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ListaObjetosCadeConexion"] == null)
            {
                ListaObjetosCadeConexion = new List<string>();
                Session["ListaObjetosCadeConexion"] = ListaObjetosCadeConexion;
            }

            if (Session["ContadorDDLTablas"] == null)
            {
                ContadorDDLTablas = 1;
                Session["ContadorDDLTablas"] = ContadorDDLTablas;
            }

            if (Session["ListaControlesDDLTablas"] == null)
            {
                ListaControlesDDLTablas = new List<clsControlesDDLTablas>();
                Session["ListaControlesDDLTablas"] = ListaControlesDDLTablas;
            }

            if (Session["ListaTablasConstraint"] == null)
            {
                ListaTablasConstraint = new List<string>();
                Session["ListaTablasConstraint"] = ListaTablasConstraint;
            }

            if (Session["ListaddlColumnCanvas"] == null)
            {
                ListaddlColumnCanvas = new List<string>();
                ListaddlColumnCanvas.Add("Label");
                ListaddlColumnCanvas.Add("Index Label");
                ListaddlColumnCanvas.Add("Posicion X");
                ListaddlColumnCanvas.Add("Posicion Y");
                Session["ListaddlColumnCanvas"] = ListaddlColumnCanvas;
            }

            string ddlid = Page.Request.QueryString["ddlid"];
            if (!string.IsNullOrEmpty(ddlid))
            {
                QuitarTabla(Convert.ToInt32(ddlid));
            }
        }

        protected void WizardStep1_Load(object sender, EventArgs e)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (!string.IsNullOrEmpty(objEsquema.getConnectionString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "enable_startbutton", "enable_startbutton();", true);
            }
        }

        protected void WizardStep2_Load(object sender, EventArgs e)
        {
            ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
            if (ListaControlesDDLTablas.Count > 0)
            {
                ddlTablas.Enabled = false;
                btnEstablecerTablaPrincipal.Enabled = false;
            }
            else
            {
                ddlTablas.Enabled = true;
                btnEstablecerTablaPrincipal.Enabled = true;
            }
            foreach (clsControlesDDLTablas control in ListaControlesDDLTablas)
            {
                this.pNuevaTabla.Controls.Add(control.getDDL());
                this.pNuevaTabla.Controls.Add(control.getJUMP());
                this.pNuevaTabla.Controls.Add(control.getBTN());
                this.pNuevaTabla.Controls.Add(control.getSPACE());
                if (ListaControlesDDLTablas.LastOrDefault().getDDL().ID.ToString().Equals(control.getDDL().ID.ToString()))
                {
                    if (control.getDDL().ID.ToString().Equals("ddlTabla1"))
                    {
                        (this.pNuevaTabla.Controls[0] as DropDownList).Enabled = true;
                        (this.pNuevaTabla.Controls[2] as Button).Enabled = true;
                    }
                    else if (control.getDDL().ID.ToString().Equals("ddlTabla2"))
                    {
                        (this.pNuevaTabla.Controls[4] as DropDownList).Enabled = true;
                        (this.pNuevaTabla.Controls[6] as Button).Enabled = true;
                    }
                    else if (control.getDDL().ID.ToString().Equals("ddlTabla3"))
                    {
                        (this.pNuevaTabla.Controls[8] as DropDownList).Enabled = true;
                        (this.pNuevaTabla.Controls[10] as Button).Enabled = true;
                    }
                }
            }

            ListaTablasConstraint = Session["ListaTablasConstraint"] as List<string>;
            if (ListaTablasConstraint.Count == 0)
            {
                btnAgregarTabla.Enabled = false;
            }
        }

        protected void WizardStep3_Load(object sender, EventArgs e)
        {

        }

        protected void btnProbarConexion_Click(object sender, EventArgs e)
        {
            if (Session["ListaObjetosCadeConexion"] != null)
            {
                ListaObjetosCadeConexion = Session["ListaObjetosCadeConexion"] as List<string>;
                if (ListaObjetosCadeConexion.Count ==  5)
                {
                    ListaObjetosCadeConexion.Clear();
                }
                ListaObjetosCadeConexion.Add(ddlConexion.SelectedValue);
                ListaObjetosCadeConexion.Add(txtServidor.Text);
                ListaObjetosCadeConexion.Add(txtBaseDatos.Text);
                ListaObjetosCadeConexion.Add(txtUser.Text);
                ListaObjetosCadeConexion.Add(txtPassword.Text);
                Session["ListaObjetosCadeConexion"] = ListaObjetosCadeConexion;
                clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
                objEsquema.NuevoEsquema();
                if (objEsquema.ProbarConexion(ListaObjetosCadeConexion))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "enable_startbutton", "enable_startbutton();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_True", "alert('Conexion establecida con exito');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_False", "alert('No se ha podido establecer conexion');", true);
                }
            }
        }

        protected void StartNextButton_Click(object sender, EventArgs e)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (objEsquema.getListaTablas().Count == 0)
            {
                ListaTablas = new List<string>();
                Session["ListaTablas"] = ListaTablas;
                CargarTablasBD();
            }
            else
            {
                ListaTablas = Session["ListaTablas"] as List<string>;
                ddlTablas.DataSource = ListaTablas;
                ddlTablas.DataBind();
            }
        }

        private void CargarTablasBD()
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (objEsquema.CargarTablasBD())
            {
                ListaTablas = Session["ListaTablas"] as List<string>;
                ListaTablas = objEsquema.getListaTablas();
                Session["ListaTablas"] = ListaTablas;
                ddlTablas.DataSource = ListaTablas;
                ddlTablas.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error_Tablas.Bind", "alert('No se ha podido cargar las tablas de la BD');", true);
            }
        }

        protected void btnEstablecerTablaPrincipal_Click(object sender, EventArgs e)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (objEsquema.getdtConstraints().Rows.Count > 0)
            {
                ListaTablasConstraint = Session["ListaTablasConstraint"] as List<string>;
                ListaTablasConstraint.Clear();
                foreach (DataRow row in objEsquema.getdtConstraints().Rows)
                {
                    if (row[2].ToString().Equals(ddlTablas.Text))
                    {
                        ListaTablasConstraint.Add(row[0].ToString());
                    }
                }
                Session["ListaTablasConstraint"] = ListaTablasConstraint;
                btnAgregarTabla.Enabled = true;
            }
        }

        protected void btnAgregarTabla_Click(object sender, EventArgs e)
        {
            ListaTablasConstraint = Session["ListaTablasConstraint"] as List<string>;
            ContadorDDLTablas = Convert.ToInt32(Session["ContadorDDLTablas"]);
            ///Arreglar if no se gragan todas las tablas posibles
            if (ContadorDDLTablas <= ListaTablasConstraint.Count)
            {
                if (ContadorDDLTablas <= 3)
                {
                    ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
                    if (ContadorDDLTablas == 1)
                    {
                        ddlTablas.Enabled = false;
                        btnEstablecerTablaPrincipal.Enabled = false;
                        DropDownList ddl = new DropDownList();
                        ddl.ID = "ddlTabla" + ContadorDDLTablas;
                        ddl.DataSource = ListaTablasConstraint;
                        ddl.DataBind();
                        Button btn = new Button();
                        btn.Text = "Quitar Tabla";
                        btn.ID = "btnQuitarTabla" + ContadorDDLTablas;
                        btn.OnClientClick = "javascript:QuitarTabla(" + ContadorDDLTablas + ");";
                        btn.CssClass = "btn btn-default";
                        LiteralControl jump = new LiteralControl("&nbsp;");
                        LiteralControl space = new LiteralControl("<br /><br />");

                        clsControlesDDLTablas newControl = new clsControlesDDLTablas(ddl, jump, btn, space);
                        ListaControlesDDLTablas.Add(newControl);
                    }
                    else if (ContadorDDLTablas == 2)
                    {
                        ListaControlesDDLTablas.Last().setEnabled(false);
                        ListaTablasConstraint.Remove(ListaControlesDDLTablas.Last().getDDL().Text);

                        DropDownList ddl = new DropDownList();
                        ddl.ID = "ddlTabla" + ContadorDDLTablas;
                        ddl.DataSource = ListaTablasConstraint;
                        ddl.DataBind();
                        Button btn = new Button();
                        btn.Text = "Quitar Tabla";
                        btn.ID = "btnQuitarTabla" + ContadorDDLTablas;
                        btn.OnClientClick = "javascript:QuitarTabla(" + ContadorDDLTablas + ");";
                        btn.CssClass = "btn btn-default";
                        LiteralControl jump = new LiteralControl("&nbsp;");
                        LiteralControl space = new LiteralControl("<br /><br />");

                        clsControlesDDLTablas newControl = new clsControlesDDLTablas(ddl, jump, btn, space);
                        ListaControlesDDLTablas.Add(newControl);
                    }
                    else if (ContadorDDLTablas == 3)
                    {
                        ListaControlesDDLTablas.Last().setEnabled(false);
                        ListaTablasConstraint.Remove(ListaControlesDDLTablas.Last().getDDL().Text);

                        DropDownList ddl = new DropDownList();
                        ddl.ID = "ddlTabla" + ContadorDDLTablas;
                        ddl.DataSource = ListaTablasConstraint;
                        ddl.DataBind();
                        Button btn = new Button();
                        btn.Text = "Quitar Tabla";
                        btn.ID = "btnQuitarTabla" + ContadorDDLTablas;
                        btn.OnClientClick = "javascript:QuitarTabla(" + ContadorDDLTablas + ");";
                        btn.CssClass = "btn btn-default";
                        LiteralControl jump = new LiteralControl("&nbsp;");
                        LiteralControl space = new LiteralControl("<br /><br />");

                        clsControlesDDLTablas newControl = new clsControlesDDLTablas(ddl, jump, btn, space);
                        ListaControlesDDLTablas.Add(newControl);
                    }

                    foreach (clsControlesDDLTablas control in ListaControlesDDLTablas)
                    {
                        this.pNuevaTabla.Controls.Add(control.getDDL());
                        this.pNuevaTabla.Controls.Add(control.getJUMP());
                        this.pNuevaTabla.Controls.Add(control.getBTN());
                        this.pNuevaTabla.Controls.Add(control.getSPACE());
                        if (ListaControlesDDLTablas.LastOrDefault().getDDL().ID.ToString().Equals(control.getDDL().ID.ToString()))
                        {
                            if (control.getDDL().ID.ToString().Equals("ddlTabla1"))
                            {
                                (this.pNuevaTabla.Controls[0] as DropDownList).Enabled = true;
                                (this.pNuevaTabla.Controls[2] as Button).Enabled = true;
                            }
                            else if (control.getDDL().ID.ToString().Equals("ddlTabla2"))
                            {
                                (this.pNuevaTabla.Controls[4] as DropDownList).Enabled = true;
                                (this.pNuevaTabla.Controls[6] as Button).Enabled = true;
                            }
                            else if (control.getDDL().ID.ToString().Equals("ddlTabla3"))
                            {
                                (this.pNuevaTabla.Controls[8] as DropDownList).Enabled = true;
                                (this.pNuevaTabla.Controls[10] as Button).Enabled = true;
                            }
                        }
                    }

                    ContadorDDLTablas++;
                    Session["ContadorDDLTablas"] = ContadorDDLTablas;
                    Session["ListaTablasConstraint"] = ListaTablasConstraint;
                    Session["ListaControlesDDLTablas"] = ListaControlesDDLTablas;
                }
            }
        }

        private void QuitarTabla(int index)
        {
            try
            {
                ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
                ListaControlesDDLTablas.RemoveAt(index - 1);
                if (ListaControlesDDLTablas.Count() > 0)
                {
                    ListaTablasConstraint = Session["ListaTablasConstraint"] as List<string>;
                    ListaTablasConstraint.Add(ListaControlesDDLTablas.Last().getDDL().Text);

                    foreach (clsControlesDDLTablas control in ListaControlesDDLTablas)
                    {
                        if (control.getDDL().ID.ToString().Equals(ListaControlesDDLTablas.Last().getDDL().ID.ToString()))
                        {
                            control.setEnabled(true);
                            control.cargarDDL(ListaTablasConstraint);
                        }
                    }
                    Session["ListaTablasConstraint"] = ListaTablasConstraint;
                }
                Session["ListaControlesDDLTablas"] = ListaControlesDDLTablas;
                ContadorDDLTablas = Convert.ToInt32(Session["ContadorDDLTablas"]);
                ContadorDDLTablas--;
                Session["ContadorDDLTablas"] = ContadorDDLTablas;
                Response.Redirect("~/Home.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        private void crearGridViews_Step3()
        {
            ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
            int Contador = 1;
            for (int i = 0; i < ListaControlesDDLTablas.Count + 1; i++)
            {
                if (i == 0)
                {
                    ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
                    DropDownList ddl = new DropDownList();
                    ddl.DataSource = ListaddlColumnCanvas;
                    ddl.DataBind();

                    DataTable dtColumnsxTable = new DataTable();
                    DataRow NewRow = dtColumnsxTable.NewRow();
                    NewRow[0] = "Campos de Tabla";

                    dtCanvas.Rows.Add(NewRow);
                    gvDatos.DataSource = dtCanvas;
                    gvDatos.DataBind();



                    //GridView gv = new GridView();
                    //gv.ID = "gvTabla" + Contador;
                    //TemplateField tfColumnDDL = new TemplateField();
                    //tfColumnDDL.HeaderText = "Campos Canvas";
                    //tfColumnDDL.ItemTemplate = ddl as ITemplate;
                    //gv.Columns.Add(tfColumnDDL);

                }
            }
        }

        /// Lo demas no va
        /// 

        //protected void StepPreviousButton_Click(object sender, EventArgs e)
        //{
        //    lcTablas = Session["lcTablas"] as List<LiteralControl>;
        //    lcTablas.First().ID = "NotReloadWizard2";
        //    Session["lcTablas"] = lcTablas;
        //}

        //No va ha hacer falta

        //No va ha hacer falta
        protected void ddlTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
            if (query.ToList().Count > 0)
            {
                //ddlColumnas.DataSource = query.ToList();
                //ddlColumnas.DataBind();
            }
        }
        
        private void cargarComboBoxListaCanvas()
        {
            //cargarListaCanvas();
            //ddlEnlaceCanvas.DataSource = ListaddlCanvas;
            //ddlEnlaceCanvas.DataBind();
        }

        private void cargarGrid()
        {
            dtCanvas = new DataTable();
            dtCanvas.Columns.Add("Columnas_Tabla", typeof(String));
            //dtCanvas.Columns.Add("Datos_Canvas", typeof(WebControl));
            Session["dtCanvas"] = dtCanvas;
            gvDatos.DataSource = dtCanvas;
            gvDatos.DataBind();
        }

        protected void btnAgregarEnlace_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlEnlaceCanvas.SelectedValue) || !string.IsNullOrEmpty(ddlEnlaceTabla.SelectedValue))
            {
                ///Elimina el campo de la lista
                string campo_cvs = ddlEnlaceCanvas.SelectedValue;
                ListaddlCanvas = Session["ListaCanvas"] as List<string>;
                ListaddlCanvas.Remove(campo_cvs);
                Session["ListaCanvas"] = ListaddlCanvas;
                ddlEnlaceCanvas.DataSource = ListaddlCanvas;
                ddlEnlaceCanvas.DataBind();

                ///Elimina el campo de la lista
                string campo_tbl = ddlEnlaceTabla.SelectedValue;
                ListaddlTablas = Session["ListaTablas"] as List<string>;
                ListaddlTablas.Remove(campo_tbl);
                Session["ListaTablas"] = ListaddlTablas;
                ddlEnlaceTabla.DataSource = ListaddlTablas;
                ddlEnlaceTabla.DataBind();

                ///Carga el row com los campos seleccionados
                dtCanvas = Session["dtCanvas"] as DataTable;
                DataRow NewRow = dtCanvas.NewRow();
                NewRow[0] = campo_tbl;
                //NewRow[1] = ddlEnlaceCanvas;
                dtCanvas.Rows.Add(NewRow);
                Session["dtCanvas"] = dtCanvas;
                gvDatos.DataSource = dtCanvas;
                gvDatos.DataBind();
            }
        }

        protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row

                DropDownList drpdwndec = (DropDownList)e.Row.FindControl("ddlCamposCanvas");

                drpdwndec.DataSource = ddlEnlaceCanvas.DataSource;

                drpdwndec.DataBind();

                //drpdwndec.Items.Insert(0, new ListItem("No Seleccionado"));
            }
        }

        protected void btnAgregarColumnas_Click(object sender, EventArgs e)
        {
            ListaddlTablas = new List<string>();
            ListaTablas = (Session["Tablas"]) as List<string>;
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
            ListaddlTablas = query.ToList();
            Session["ListaTablas"] = ListaddlTablas;
            ddlEnlaceTabla.DataSource = ListaddlTablas;
            ddlEnlaceTabla.DataBind();
            cargarComboBoxListaCanvas();
            cargarGrid();
            ListaTablas.Add(ddlTablas.SelectedValue);
            Session["Tablas"] = ListaTablas;
            btnAgregarEnlace.Enabled = true;
            gvDatos.Enabled = true;
        }

        protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string campo_tbl = gvDatos.Rows[e.RowIndex].Cells[1].Text;
            string campo_cvs = gvDatos.Rows[e.RowIndex].Cells[2].Text;

            ListaddlCanvas = Session["ListaCanvas"] as List<string>;
            ListaddlCanvas.Add(campo_cvs);
            Session["ListaCanvas"] = ListaddlCanvas;
            ddlEnlaceCanvas.DataSource = ListaddlCanvas;
            ddlEnlaceCanvas.DataBind();

            ListaddlTablas = Session["ListaTablas"] as List<string>;
            ListaddlTablas.Add(campo_tbl);
            Session["ListaTablas"] = ListaddlTablas;
            ddlEnlaceTabla.DataSource = ListaddlTablas;
            ddlEnlaceTabla.DataBind();

            dtCanvas = Session["dtCanvas"] as DataTable;
            DataRow Row = dtCanvas.Rows[e.RowIndex];
            dtCanvas.Rows.Remove(Row);
            Session["dtCanvas"] = dtCanvas;
            gvDatos.DataSource = dtCanvas;
            gvDatos.DataBind();
        }

        protected void btnGenrarCodigo_Click(object sender, EventArgs e)
        {
            if ((Session["dtCanvas"] as DataTable).Rows.Count > 0)
            {
                DropDownList drpdwndec = (DropDownList)gvDatos.Rows[0].FindControl("ddlCamposCanvas");
                string test = drpdwndec.SelectedValue;


                //clsFactory objFactory = new clsFactory();
                //objFactory.FactoryMethod(Session["Tabla"].ToString(), Session["dtCanvas"] as DataTable);
                //Limpiar();
                ///Cambiar a MVC
                //Response.Redirect("~/Home/Index");
            }
        }

        private bool ValidarCamposCanvas(string _Campo)
        {
            bool exito = true;
            List<string> ListaEnlacesTablas = Session["ListaEnlacesTablas"] as List<string>;
            if (ListaEnlacesTablas.Count <= 0)
            {
                ListaEnlacesTablas.Add(_Campo);
            }
            else
            {
                foreach (string item in ListaEnlacesTablas)
                {
                    if (item.Equals(_Campo))
                    {
                        exito = false;
                        break;
                    }
                }
                if (exito)
                {
                    ListaEnlacesTablas.Add(_Campo);
                }
            }
            return exito;
        }

        private void Limpiar()
        {
            //ListaObjetosCadeConexion = null;
            //ListaTablas = null;
            //Session["ListaObjetosCadeConexion"] = null;
            //Session["ListaTablas"] = null;
        }
    }
    
    public class clsControlesDDLTablas
    {
        private DropDownList ddl;
        private LiteralControl space;
        private Button btn;
        private LiteralControl jump;

        public DropDownList getDDL()
        {
            return this.ddl;
        }

        public LiteralControl getSPACE()
        {
            return this.space;
        }

        public Button getBTN()
        {
            return this.btn;
        }

        public LiteralControl getJUMP()
        {
            return this.jump;
        }

        public void setEnabled(bool _enabled)
        {
            this.ddl.Enabled = _enabled;
            this.btn.Enabled = _enabled;
        }

        public void cargarDDL(List<string> values)
        {
            this.ddl.DataSource = values;
            this.ddl.DataBind();
        }

        public clsControlesDDLTablas(DropDownList _ddl, LiteralControl _jump, Button _btn, LiteralControl _space)
        {
            this.ddl = _ddl;
            this.jump = _jump;
            this.btn = _btn;
            this.space = _space;
        }
    }
}