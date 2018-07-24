using ASPNET_MVC_Samples.Models;
using CanvasViews.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanvasViews.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            clsVista obj = clsVista.obtenerclsVista();
            DataPoint objDataPoint = new DataPoint();
            List<DataPoint> dataPoints = objDataPoint.obtenerLista(obj.obtenerNumeroFilas());
            for (int i = 0; i < obj.getdtColumnas().Rows.Count; i++)
            {
                if (obj.getdtColumnas().Rows[i][1].ToString().Equals("Posicion X"))
                {
                    var query = (from dtRow in obj.getListaEsquema() where dtRow.ColumnaxTabla.StartsWith(obj.getdtColumnas().Rows[i][0].ToString()) select dtRow.ListaDetalleColumnas).FirstOrDefault();
                    for (int j = 0; j < query.ToList().Count; j++)
                    {
                        dataPoints.ElementAt(j).setX(query.ToList().ElementAt(j).Dato);
                    }
                }
                else if (obj.getdtColumnas().Rows[i][1].ToString().Equals("Posicion Y"))
                {
                    var query = (from dtRow in obj.getListaEsquema() where dtRow.ColumnaxTabla.StartsWith(obj.getdtColumnas().Rows[i][0].ToString()) select dtRow.ListaDetalleColumnas).FirstOrDefault();
                    for (int j = 0; j < query.ToList().Count; j++)
                    {
                        dataPoints.ElementAt(j).setY(Convert.ToDouble(query.ToList().ElementAt(j).Dato));
                    }
                }
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.GraphCount = 4;
            return View();
        }
    }
}