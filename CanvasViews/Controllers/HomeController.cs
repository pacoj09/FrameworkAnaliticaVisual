using ASPNET_MVC_Samples.Models;
using Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            clsFactory objFactory = new clsFactory();
            DataPoint objDataPoint = new DataPoint();
            List<DataPoint> dataPoints = objDataPoint.obtenerLista(objFactory.getNumeroFilas());
            for (int i = 0; i < objFactory.getEnlaces().Rows.Count; i++)
            {
                if (objFactory.getEnlaces().Rows[i][1].ToString().Equals("Posicion X"))
                {
                    var query = from dtRow in objFactory.getListaEsquema() where dtRow.ColumnaxTabla.StartsWith(objFactory.getEnlaces().Rows[i][0].ToString()) select dtRow.ListaDetalleColumnas;
                    for (int j = 0; j < query.ToList().Count; j++)
                    {
                        objDataPoint = dataPoints.ElementAt(j);
                        objDataPoint.setX(query.ToList().ElementAt(j).ToString());
                        dataPoints.Insert(j, objDataPoint);
                    }
                }
                else if (objFactory.getEnlaces().Rows[i][1].ToString().Equals("Posicion Y"))
                {
                    var query = from dtRow in objFactory.getListaEsquema() where dtRow.ColumnaxTabla.StartsWith(objFactory.getEnlaces().Rows[i][0].ToString()) select dtRow.ListaDetalleColumnas;
                    for (int j = 0; j < query.ToList().Count; j++)
                    {
                        objDataPoint = dataPoints.ElementAt(j);
                        objDataPoint.setY(query.ToList().ElementAt(j).ToString());
                        dataPoints.Insert(j, objDataPoint);
                    }
                }
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}