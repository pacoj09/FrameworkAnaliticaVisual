using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameworkAnaliticaVisual
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsEsquema objEsquema = new clsEsquema();
            if (objEsquema.CargarEsquema())
            {
                DropDownList1.DataSource = objEsquema.ListaTablas;
                DropDownList1.DataBind();
            }

            //clsGenerador objGenerador = new clsGenerador();
            //objGenerador.linew = "Hola mundo como te va";
            //objGenerador.WriteMethod();
        }
    }
}