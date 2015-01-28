using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Floreview.Utils
{
    public abstract class GeocodingEngine
    {
        private const String GOOGLE_GEOCODING_URL = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key=AIzaSyAZueUAln4594Z-WVpjrj9CdVeEg7iTjQ4";

        private const int RADIUS_SPHERE = 6371;

        private const int RADIUS_CIRCLE = 15;

        public static DbGeography GetLatLongFromCity(String city)
        {
            try
            {
                WebRequest request = WebRequest.Create(String.Format(GOOGLE_GEOCODING_URL, city));
                WebResponse response = request.GetResponse();

                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);

                    dynamic data = JsonConvert.DeserializeObject(reader.ReadToEnd());
                    String latitude = data["results"][0]["geometry"]["location"]["lat"];
                    String longitude = data["results"][0]["geometry"]["location"]["lng"];

                    reader.Close();
                    stream.Close();
                    response.Close();

                    return DbGeography.FromText(String.Format("POINT({0} {1})", longitude, latitude));
                }

                throw new WebException();
            }
            catch (Exception ex)
            {
                return DbGeography.FromText("POINT(0 0)");
            }
        }

        public static Boolean IsStoreWhitinRange(DbGeography company, DbGeography city)
        {
            double latPos1 = Convert.ToDouble(company.Latitude);
            double lngPos1 = Convert.ToDouble(company.Longitude);
            double latPos2 = Convert.ToDouble(city.Latitude);
            double lngPos2 = Convert.ToDouble(city.Longitude);

            double ΔLatitude = ConvertToRadians(latPos2 - latPos1);
            double ΔLongitude = ConvertToRadians(lngPos2 - lngPos1);

            // formule
            // haversin(d/r) = haversin(ϕ2 - ϕ1) + cos(ϕ1)*cos(ϕ2) * haversin(λ2 - λ1)
            //
            // waar: haversin(θ) = sin²(θ / 2)
            //
            //
            // h = haversin(ϕ2 - ϕ1) + cos(ϕ1)*cos(ϕ2) * haversin(λ2 - λ1)
            //
            // d = r * haversin-1(h) = 2 * r * ArcSin(√h)


            double h = Math.Sin(ΔLatitude / 2) * Math.Sin(ΔLatitude / 2) + Math.Cos(latPos1) * Math.Cos(latPos2) * Math.Sin(ΔLongitude / 2) * Math.Sin(ΔLongitude / 2);
            double d = 2 * RADIUS_SPHERE * Math.Asin(Math.Sqrt(h));

            if (d > 0 && d <= RADIUS_CIRCLE)
            {
                return true;
            }

            return false;
        }

        private static double ConvertToRadians(double d)
        {
            return (Math.PI / 180) * d;
        }
    }
}