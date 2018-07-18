﻿using Clases;
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
                ContadorDDLTablas = 0;
                Session["ContadorDDLTablas"] = ContadorDDLTablas;
            }

            string ddlid = Page.Request.QueryString["ddlid"];
            if (!string.IsNullOrEmpty(ddlid))
            {
                QuitarTabla(Convert.ToInt32(ddlid));
            }

                //cargarListasBD();
                //if (Session["dtCanvas"] == null)
                //{
                //    cargarGrid();
                //}

                //if (Session["Tablas"] == null)
                //{
                //    ListaTablas = new List<string>();
                //    Session["Tablas"] = ListaTablas;
                //}

                //if (Session["ListaEnlacesTablas"] == null)
                //{
                //    ListaEnlacesTablas = new List<string>();
                //    Session["ListaEnlacesTablas"] = ListaEnlacesTablas;
                //}
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
                if (objEsquema.ProbarConexion(ListaObjetosCadeConexion))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "enable_button", "enable_startbutton();", true);
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
            ListaTablas = new List<string>();
            Session["ListaTablas"] = ListaTablas;
            CargarTablasBD();

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
            //if (string.IsNullOrEmpty(ddlTablas.SelectedValue))
            //{
            //    clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            //    if (objEsquema.getListaTablas().Count <= 0 && objEsquema.getListaColumnas().Count <= 0)
            //    {
            //        if (objEsquema.CargarTablasBD())
            //        {
            //            ddlTablas.DataSource = objEsquema.getListaTablas();
            //            ddlTablas.DataBind();
            //            var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
            //            if (query.ToList().Count > 0)
            //            {
            //                //ddlColumnas.DataSource = query.ToList();
            //                //ddlColumnas.DataBind();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        ddlTablas.DataSource = objEsquema.getListaTablas();
            //        ddlTablas.DataBind();
            //        var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
            //        if (query.ToList().Count > 0)
            //        {
            //            //ddlColumnas.DataSource = query.ToList();
            //            //ddlColumnas.DataBind();
            //        }
            //    }
            //}
        }

        protected void btnEstablecerTablaPrincipal_Click(object sender, EventArgs e)
        {
            ///BloquearDDLtablaPirncipal y eliminar la tabla de la lista del Session
            ///Enable button agregar tabla
            //ScriptManager.RegisterStartupScript(this, GetType(), "enable_button", "enable_startbutton();", true);
        }

        protected void btnAgregarTabla_Click(object sender, EventArgs e)
        {
            ContadorDDLTablas = Convert.ToInt32(Session["ContadorDDLTablas"]);
            if (ContadorDDLTablas <= 3)
            {
                string NewControl = string.Empty;
                if (ContadorDDLTablas == 0)
                {
                    NewControl = "<select name='ctl00$ContentPlaceHolder1$Wizard1$DropDownList" + ContadorDDLTablas + "' id='ContentPlaceHolder1_Wizard1_DropDownList" + ContadorDDLTablas + "'></ select>" +
                        "<input name='ctl00$ContentPlaceHolder1$Wizard1$btnQuitarTabla"+ ContadorDDLTablas + "' id='ContentPlaceHolder1_Wizard1_btnQuitarTabla" + ContadorDDLTablas + "' class='btn btn-default' onclick='javascript:QuitarTabla(" + ContadorDDLTablas + ");' type='submit' value='Quitar Tabla'>" +
                        "<br /> ";
                }
                else if (ContadorDDLTablas == 1)
                {
                    NewControl = "<select name='ctl00$ContentPlaceHolder1$Wizard1$DropDownList" + ContadorDDLTablas + "' id='ContentPlaceHolder1_Wizard1_DropDownList" + ContadorDDLTablas + "'></ select>" +
                        "<input name='ctl00$ContentPlaceHolder1$Wizard1$btnQuitarTabla" + ContadorDDLTablas + "' id='ContentPlaceHolder1_Wizard1_btnQuitarTabla" + ContadorDDLTablas + "' class='btn btn-default' onclick='javascript:QuitarTabla(" + ContadorDDLTablas + ");' type='submit' value='Quitar Tabla'>" +
                        "<br /> ";
                }
                else if (ContadorDDLTablas == 2)
                {
                    NewControl = "<select name='ctl00$ContentPlaceHolder1$Wizard1$DropDownList" + ContadorDDLTablas + "' id='ContentPlaceHolder1_Wizard1_DropDownList" + ContadorDDLTablas + "'></ select>" +
                        "<input name='ctl00$ContentPlaceHolder1$Wizard1$btnQuitarTabla" + ContadorDDLTablas + "' id='ContentPlaceHolder1_Wizard1_btnQuitarTabla" + ContadorDDLTablas + "' class='btn btn-default' onclick='javascript:QuitarTabla(" + ContadorDDLTablas + ");' type='submit' value='Quitar Tabla'>" +
                        "<br /> ";
                }
                else if (ContadorDDLTablas == 3)
                {
                    NewControl = "<select name='ctl00$ContentPlaceHolder1$Wizard1$DropDownList" + ContadorDDLTablas + "' id='ContentPlaceHolder1_Wizard1_DropDownList" + ContadorDDLTablas + "'></ select>" +
                        "<input name='ctl00$ContentPlaceHolder1$Wizard1$btnQuitarTabla" + ContadorDDLTablas + "' id='ContentPlaceHolder1_Wizard1_btnQuitarTabla" + ContadorDDLTablas + "' class='btn btn-default' onclick='javascript:QuitarTabla(" + ContadorDDLTablas + ");' type='submit' value='Quitar Tabla'>" +
                        "<br /> ";
                }
                LiteralControl ddlNewTable = new LiteralControl(NewControl);
                pNuevaTabla.Controls.Add(ddlNewTable);
                //Cargar el ddl en la posicion contador
                ContadorDDLTablas++;
                Session["ContadorDDLTablas"] = ContadorDDLTablas;
            }
            else
            {
                btnAgregarTabla.Enabled = false;
            }
        }

        private void QuitarTabla(int index)
        {
            pNuevaTabla.Controls.RemoveAt(index);
        }

        //No va ha hacer falta
        private void cargarListaCanvas()
        {
            ListaddlCanvas = new List<string>();
            ListaddlCanvas.Add("Color");
            ListaddlCanvas.Add("Label");
            ListaddlCanvas.Add("Index Label");
            ListaddlCanvas.Add("Posicion X");
            ListaddlCanvas.Add("Posicion Y");
            Session["ListaCanvas"] = ListaddlCanvas;
        }

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
            cargarListaCanvas();
            ddlEnlaceCanvas.DataSource = ListaddlCanvas;
            ddlEnlaceCanvas.DataBind();
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
            ListaObjetosCadeConexion = null;
            ListaTablas = null;

            Session["ListaObjetosCadeConexion"] = null;
            Session["ListaTablas"] = null;
        }

        private void AddPanel()
        {
            //clsUsuario usuario = (clsUsuario)Session["Usuario"];
            ////clsFotografia fotos = new clsFotografia(usuario.Cedula);
            ////DataTable imagenes = fotos.consultaFotoUsuario();
            //int fila = 0;
            //foreach (DataRow item in imagenes.Rows)
            //{
            //    DateTime Fecha = (DateTime)imagenes.Rows[fila][3];

            //    string path = @"" + imagenes.Rows[fila][2].ToString();
            //    LiteralControl objeto = new LiteralControl("<br /> <center> <div style='font-style: normal;background-color:#cfe9f2; overflow: auto; list-style-position: inside; border-right:1px solid #838282; border-left:1px solid #838282;'>" +
            //    "<table>" +
            //    "<tr align='center'>" +
            //    "<td><h5 style='color:#106bc2'>Foto Publicada por " + imagenes.Rows[fila][8].ToString() + "</h5></td>" +
            //    "<td><h5 style='color:#106bc2'>" + Fecha.ToString("dd/MM/yyyy") + "</h5>" +
            //    "</td>" +
            //    "</tr>" +
            //    "<tr align='center'>" +
            //    "<td colspan='3'><h6 style='color:#106bc2'>Cantidad de Visitas: " + imagenes.Rows[fila][1].ToString() + "</h6></td>" +
            //    "</tr>" +
            //    "<tr align='center'>" +
            //    "<td colspan = '3'>" +
            //    "<h2 style='color:#106bc2'>" + imagenes.Rows[fila][4].ToString() + "</h2>" +
            //    "</td>" +
            //    "</tr>" +
            //    "</table>" +
            //    "</div>" +
            //    "<div style='font-style: normal;background-color:#ecfaff; overflow: auto; list-style-position: inside; border-right:1px solid #838282; border-left:1px solid #838282;'>" +
            //    "<table>" +
            //    "<tr align='center'>" +
            //    "<td colspan='3'><img src='" + path + "' alt='no se pudo mostrar' Height='350px' Width='300px' /> </td>" +
            //    "</tr>" +
            //    "<tr align='center'>" +
            //    "<td colspan='3'><h4 style='text-align:center;color:#106bc2'>" + imagenes.Rows[fila][5].ToString() + "</h4></td> " +
            //    "</tr>" +
            //    "<tr align='center'>" +
            //    "<td colspan='3'>" +
            //    "<a href='javascript:abrirComentario(" + imagenes.Rows[fila][0].ToString() + ");'>Comentar</a>&nbsp;&nbsp;" +
            //    "<a href='javascript:EliminarFoto(" + imagenes.Rows[fila][0].ToString() + ");'>Eliminar</a>" +
            //    "</td>" +
            //    "</tr>" +
            //    "</table>" +
            //    "</div>" +
            //    "</center>");
            //    this.PanelPrincipal.Controls.Add(objeto);

            //    clsComentario comenatrioFotografia = new clsComentario();
            //    DataTable comentarios = comenatrioFotografia.consultaComentariosFoto(int.Parse(imagenes.Rows[fila][0].ToString()));
            //    foreach (DataRow valor in comentarios.Rows)
            //    {
            //        LiteralControl comentarioPorfoto = new LiteralControl("<center><div style='font-style: normal;background-color:#ecfaff; z-index: auto; width: auto; height: auto; overflow: auto; list-style-position: inside; border-right:1px solid #838282; border-left:1px solid #838282;'>" +
            //        "<div style='margin:5px;'>" +
            //        "<label style='color:#000000'>" + valor[0] + "</label>&nbsp;<input id='Text1' type='text' style='color:#000000; width: 492px;' ReadOnly='true' value='" + valor[1] + "' />" +
            //        "</div> </div> </center>");
            //        this.PanelPrincipal.Controls.Add(comentarioPorfoto);
            //    }
            //    fila++;
            //}
        }
    }
}