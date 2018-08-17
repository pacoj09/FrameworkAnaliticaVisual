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
        public ActionResult Index(string id1, string types)
        {
            clsStaticDataPoints objStaticDataPoints = clsStaticDataPoints.obtenerclsStaticDataPoints();
            if (string.IsNullOrEmpty(id1) && !objStaticDataPoints.getListasDataPoints().Any())
            {
                return IndexLoad();
            }
            else
            {
                if (string.IsNullOrEmpty(id1) && string.IsNullOrEmpty(types))
                {
                    id1 = "0";
                    types = "bar|bar|bar|bar";
                }
                return IndexParameters(id1, types);
            }
        }

        private ActionResult IndexLoad()
        {
            clsStaticDataPoints objStaticDataPoints = clsStaticDataPoints.obtenerclsStaticDataPoints();
            clsVista objVista = new clsVista();
            objVista.cargarListas();
            int contador = 0;
            ViewBag.DataPoints1 = JsonConvert.SerializeObject("");
            ViewBag.DataPoints2 = JsonConvert.SerializeObject("");
            ViewBag.DataPoints3 = JsonConvert.SerializeObject("");
            ViewBag.DataPoints4 = JsonConvert.SerializeObject("");
            ViewBag.type1 = "bar";
            ViewBag.type2 = "bar";
            ViewBag.type3 = "bar";
            ViewBag.type4 = "bar";
            ViewBag.Enable = 1;
            foreach (DataTable dtEnlaces in objVista.getListadtEnlacesTablas())
            {
                DataPoint objDataPoint = new DataPoint();
                List<DataPoint> dataPoints = objDataPoint.obtenerLista(objVista.getListaEnlaceColumnas().ElementAt(contador).getMaxRows());
                var query = (from dtRow in objVista.getListaEnlaceColumnas() where dtRow.getVista().Equals((contador)) select dtRow.getdtColumnas()).FirstOrDefault();
                for (int i = 0; i < query.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                        {
                            for (int j = 0; j < query.Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setId(query.Rows[j][i].ToString());
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                        {
                            for (int j = 0; j < query.Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setY(Convert.ToDouble(query.Rows[j][i]));
                            }
                        }
                    }
                    else if (i == 2)
                    {
                        if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                        {
                            for (int j = 0; j < query.Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setLabel(query.Rows[j][i].ToString());
                            }
                        }
                    }
                    else if (i == 3)
                    {
                        if (!string.IsNullOrEmpty(query.Rows[0][i].ToString()))
                        {
                            for (int j = 0; j < query.Rows.Count; j++)
                            {
                                dataPoints.ElementAt(j).setIndexLabel(query.Rows[j][i].ToString());
                            }
                        }
                    }
                    else if (i == 4)
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
                objStaticDataPoints.getListasDataPoints().Add(dataPoints);


                switch (contador)
                {
                    case 0:
                        ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints);
                        break;

                    case 1:
                        ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints);
                        break;

                    case 2:
                        ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints);
                        break;

                    case 3:
                        ViewBag.DataPoints4 = JsonConvert.SerializeObject(dataPoints);
                        break;
                }
                contador++;
            }
            ViewBag.GraphCount = (contador);
            return View();
        }

        public ActionResult IndexParameters(string id1, string types)
        {
            string[] chartType = types.Split('|');
            clsStaticDataPoints objStaticDataPoints = clsStaticDataPoints.obtenerclsStaticDataPoints();
            ViewBag.DataPoints1 = JsonConvert.SerializeObject("");
            ViewBag.DataPoints2 = JsonConvert.SerializeObject("");
            ViewBag.DataPoints3 = JsonConvert.SerializeObject("");
            ViewBag.DataPoints4 = JsonConvert.SerializeObject("");
            ViewBag.type1 = "bar";
            ViewBag.type2 = "bar";
            ViewBag.type3 = "bar";
            ViewBag.type4 = "bar";
            ViewBag.Enable = 1;
            if (Convert.ToInt32(id1) == 0)
            {
                for (int i = 0; i < objStaticDataPoints.getListasDataPoints().Count; i++)
                {
                    if (i == 0)
                    {
                        ViewBag.DataPoints1 = JsonConvert.SerializeObject(objStaticDataPoints.getListasDataPoints().ElementAt(i));
                        ViewBag.type1 = chartType[i];
                    }
                    else if (i == 1)
                    {
                        ViewBag.DataPoints2 = JsonConvert.SerializeObject(objStaticDataPoints.getListasDataPoints().ElementAt(i));
                        ViewBag.type2 = chartType[i];
                    }
                    else if (i == 2)
                    {
                        ViewBag.DataPoints3 = JsonConvert.SerializeObject(objStaticDataPoints.getListasDataPoints().ElementAt(i));
                        ViewBag.type3 = chartType[i];
                    }
                    else if (i == 3)
                    {
                        ViewBag.DataPoints4 = JsonConvert.SerializeObject(objStaticDataPoints.getListasDataPoints().ElementAt(i));
                        ViewBag.type4 = chartType[i];
                    }
                }
            }
            else
            {
                if (objStaticDataPoints.getListasDataPoints().Count > 1)
                {
                    for (int i = 0; i < objStaticDataPoints.getListasDataPoints().Count; i++)
                    {
                        if (i == 0)
                        {
                            ViewBag.DataPoints1 = JsonConvert.SerializeObject(objStaticDataPoints.getListasDataPoints().ElementAt(i));
                            ViewBag.type1 = chartType[i];
                        }
                        else if (i > 0)
                        {
                            List<DataPoint> listaDP = objStaticDataPoints.getListasDataPoints().ElementAt(i);
                            List<DataPoint> listaFiltro = new List<DataPoint>();
                            foreach (DataPoint item in listaDP)
                            {
                                if (item.getId().Equals(id1))
                                {
                                    listaFiltro.Add(item);
                                }
                            }
                            if (i == 1)
                            {
                                if (listaFiltro.Count > 0)
                                {
                                    ViewBag.DataPoints2 = JsonConvert.SerializeObject(listaFiltro);
                                    ViewBag.type2 = chartType[i];
                                }
                                else
                                {
                                    ViewBag.Enable = 0;
                                }
                            }
                            else if (i == 2)
                            {
                                if (listaFiltro.Count > 0)
                                {
                                    ViewBag.DataPoints3 = JsonConvert.SerializeObject(listaFiltro);
                                    ViewBag.type3 = chartType[i];
                                }
                                else
                                {
                                    ViewBag.Enable = 0;
                                }
                            }
                            else if (i == 3)
                            {
                                if (listaFiltro.Count > 0)
                                {
                                    ViewBag.DataPoints4 = JsonConvert.SerializeObject(listaFiltro);
                                    ViewBag.type4 = chartType[i];
                                }
                                else
                                {
                                    ViewBag.Enable = 0;
                                }
                            }
                        }
                    }
                }
            }

            ViewBag.GraphCount = (objStaticDataPoints.getListasDataPoints().Count);
            return View();
        }
    }
}