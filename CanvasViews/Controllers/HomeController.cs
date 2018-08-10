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
        public ActionResult Index(string id1, string id2, string id3, string id4)
        {
            if (string.IsNullOrEmpty(id1) && string.IsNullOrEmpty(id2) && string.IsNullOrEmpty(id3) && string.IsNullOrEmpty(id4))
            {
                return IndexLoad();
            }
            else
            {
                return IndexParameters(id1, id2, id3, id4);
            }
        }

        private ActionResult IndexLoad()
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
                    for (int i = 0; i < query.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setY(Convert.ToDouble(query.Rows[j][i]));
                                }
                            }
                        }
                        else if (i == 1)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                if (Double.TryParse(query.Rows[0][i].ToString(), out double n))
                                {
                                    for (int j = 0; j < query.Rows.Count; j++)
                                    {
                                        dataPoints.ElementAt(j).setX(Convert.ToDouble(query.Rows[j][i]));
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < query.Rows.Count; j++)
                                    {
                                        dataPoints.ElementAt(j).setLabel(query.Rows[j][i].ToString());
                                    }
                                }
                            }
                        }
                        else if (i == 2)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setIndexLabel(query.Rows[j][i].ToString());
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setName(query.Rows[j][i].ToString());
                                }
                            }
                        }
                    }
                    ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints);
                }
                else if (contador == 2)
                {
                    var query = (from dtRow in objVista.getListaEnlaceColumnas() where dtRow.getVista().Equals((contador - 1)) select dtRow.getdtColumnas()).FirstOrDefault();
                    for (int i = 0; i < query.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setY(Convert.ToDouble(query.Rows[j][i]));
                                }
                            }
                        }
                        else if (i == 1)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                if (Double.TryParse(query.Rows[0][i].ToString(), out double n))
                                {
                                    for (int j = 0; j < query.Rows.Count; j++)
                                    {
                                        dataPoints.ElementAt(j).setX(Convert.ToDouble(query.Rows[j][i]));
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < query.Rows.Count; j++)
                                    {
                                        dataPoints.ElementAt(j).setLabel(query.Rows[j][i].ToString());
                                    }
                                }
                            }
                        }
                        else if (i == 2)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setIndexLabel(query.Rows[j][i].ToString());
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setName(query.Rows[j][i].ToString());
                                }
                            }
                        }
                    }
                    ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints);
                }
                else if (contador == 3)
                {
                    var query = (from dtRow in objVista.getListaEnlaceColumnas() where dtRow.getVista().Equals((contador - 1)) select dtRow.getdtColumnas()).FirstOrDefault();
                    for (int i = 0; i < query.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setY(Convert.ToDouble(query.Rows[j][i]));
                                }
                            }
                        }
                        else if (i == 1)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                if (Double.TryParse(query.Rows[0][i].ToString(), out double n))
                                {
                                    for (int j = 0; j < query.Rows.Count; j++)
                                    {
                                        dataPoints.ElementAt(j).setX(Convert.ToDouble(query.Rows[j][i]));
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < query.Rows.Count; j++)
                                    {
                                        dataPoints.ElementAt(j).setLabel(query.Rows[j][i].ToString());
                                    }
                                }
                            }
                        }
                        else if (i == 2)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setIndexLabel(query.Rows[j][i].ToString());
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setName(query.Rows[j][i].ToString());
                                }
                            }
                        }
                    }
                    ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints);
                }
                else if (contador == 4)
                {
                    var query = (from dtRow in objVista.getListaEnlaceColumnas() where dtRow.getVista().Equals((contador - 1)) select dtRow.getdtColumnas()).FirstOrDefault();
                    for (int i = 0; i < query.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setY(Convert.ToDouble(query.Rows[j][i]));
                                }
                            }
                        }
                        else if (i == 1)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                if (Double.TryParse(query.Rows[0][i].ToString(), out double n))
                                {
                                    for (int j = 0; j < query.Rows.Count; j++)
                                    {
                                        dataPoints.ElementAt(j).setX(Convert.ToDouble(query.Rows[j][i]));
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < query.Rows.Count; j++)
                                    {
                                        dataPoints.ElementAt(j).setLabel(query.Rows[j][i].ToString());
                                    }
                                }
                            }
                        }
                        else if (i == 2)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setIndexLabel(query.Rows[j][i].ToString());
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                            {
                                for (int j = 0; j < query.Rows.Count; j++)
                                {
                                    dataPoints.ElementAt(j).setName(query.Rows[j][i].ToString());
                                }
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

        public ActionResult IndexParameters(string id1, string id2, string id3, string id4)
        {
            clsVista objVista = new clsVista();
            return View();
        }
    }
}