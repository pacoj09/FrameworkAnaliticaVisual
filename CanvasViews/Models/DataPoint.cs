using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ASPNET_MVC_Samples.Models
{
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class DataPoint
    {
        private string x, y;

        public void setX(string _x)
        {
            this.x = _x;
        }

        public void setY(string _y)
        {
            this.y = _y;
        }

        public DataPoint() { }

        public List<DataPoint> obtenerLista(int _nObjects)
        {
            List<DataPoint> listaDataPoints = new List<DataPoint>();
            for (int i = 0; i < _nObjects; i++)
            {
                DataPoint objDataPoint = new DataPoint();
                listaDataPoints.Add(objDataPoint);
            }
            return listaDataPoints;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public string X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public string Y = null;
    }
}