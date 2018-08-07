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
            clsVista objVista = new clsVista();
            objVista.cargarListas();
            int contador = 1;
            foreach (DataTable dtEnlaces in objVista.getListadtEnlacesTablas())
            {
                DataPoint objDataPoint = new DataPoint();
                List<DataPoint> dataPoints = objDataPoint.obtenerLista(objVista.getListaEnlaceColumnas().ElementAt(contador - 1).getMaxRows());
                if (contador == 1)
                {
                    var query = (from dtRow in objVista.getListaEnlaceColumnas() where dtRow.getVista().Equals((contador - 1)) select dtRow.getdtColumnas()).FirstOrDefault();
                    for (int i = 0; i < dtEnlaces.Rows.Count; i++)
                    {
                        if (dtEnlaces.Rows[i][2].ToString().Equals("Posicion X"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setX(query.ElementAt(i).Rows[j][0].ToString());
                            }
                        }
                        else if (dtEnlaces.Rows[i][2].ToString().Equals("Posicion Y"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setY(Convert.ToInt32(query.ElementAt(i).Rows[j][0]));
                            }
                        }
                        else if (dtEnlaces.Rows[i][2].ToString().Equals("IndexLabel"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setLabel(query.ElementAt(i).Rows[j][0].ToString());
                            }
                        }
                    }
                    ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints);
                }
                else if (contador == 2)
                {
                    var query = (from dtRow in objVista.getListaEnlaceColumnas() where dtRow.getVista().Equals((contador - 1)) select dtRow.getdtColumnas()).FirstOrDefault();
                    for (int i = 0; i < dtEnlaces.Rows.Count; i++)
                    {
                        if (dtEnlaces.Rows[i][2].ToString().Equals("Posicion X"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setX(query.ElementAt(i).Rows[j][0].ToString());
                            }
                        }
                        else if (dtEnlaces.Rows[i][2].ToString().Equals("Posicion Y"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setY(Convert.ToInt32(query.ElementAt(i).Rows[j][0]));
                            }
                        }
                        else if (dtEnlaces.Rows[i][2].ToString().Equals("IndexLabel"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setLabel(query.ElementAt(i).Rows[j][0].ToString());
                            }
                        }
                    }
                    ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints);
                }
                else if (contador == 3)
                {
                    var query = (from dtRow in objVista.getListaEnlaceColumnas() where dtRow.getVista().Equals((contador - 1)) select dtRow.getdtColumnas()).FirstOrDefault();
                    for (int i = 0; i < dtEnlaces.Rows.Count; i++)
                    {
                        if (dtEnlaces.Rows[i][2].ToString().Equals("Posicion X"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setX(query.ElementAt(i).Rows[j][0].ToString());
                            }
                        }
                        else if (dtEnlaces.Rows[i][2].ToString().Equals("Posicion Y"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setY(Convert.ToInt32(query.ElementAt(i).Rows[j][0]));
                            }
                        }
                        else if (dtEnlaces.Rows[i][2].ToString().Equals("IndexLabel"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setLabel(query.ElementAt(i).Rows[j][0].ToString());
                            }
                        }
                    }
                    ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints);
                }
                else if (contador == 4)
                {
                    var query = (from dtRow in objVista.getListaEnlaceColumnas() where dtRow.getVista().Equals((contador - 1)) select dtRow.getdtColumnas()).FirstOrDefault();
                    for (int i = 0; i < dtEnlaces.Rows.Count; i++)
                    {
                        if (dtEnlaces.Rows[i][2].ToString().Equals("Posicion X"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setX(query.ElementAt(i).Rows[j][0].ToString());
                            }
                        }
                        else if (dtEnlaces.Rows[i][2].ToString().Equals("Posicion Y"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setY(Convert.ToInt32(query.ElementAt(i).Rows[j][0]));
                            }
                        }
                        else if (dtEnlaces.Rows[i][2].ToString().Equals("IndexLabel"))
                        {
                            for (int j = 0; j < query.ElementAt(i).Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setLabel(query.ElementAt(i).Rows[j][0].ToString());
                            }
                        }
                    }
                    ViewBag.DataPoints4 = JsonConvert.SerializeObject(dataPoints);
                }
                contador++;
            }
            if ((contador - 1) == 1)
            {
                ViewBag.DataPoints2 = JsonConvert.SerializeObject("");
                ViewBag.DataPoints3 = JsonConvert.SerializeObject("");
                ViewBag.DataPoints4 = JsonConvert.SerializeObject("");
            }
            else if ((contador - 1) == 2)
            {
                ViewBag.DataPoints3 = JsonConvert.SerializeObject("");
                ViewBag.DataPoints4 = JsonConvert.SerializeObject("");
            }
            else if ((contador - 1) == 3)
            {
                ViewBag.DataPoints4 = JsonConvert.SerializeObject("");
            }
            ViewBag.GraphCount = (contador - 1);
            return View();
        }
    }
}