using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vozidla
{
    /// <summary>
    /// Class vozidlo which defines attributes for vehicle
    /// </summary>
    public class Vozidlo
    {
        private string nazev;

        private string datum;

        private Double cena;

        private Double dph;

        public Vozidlo()
        {
            Nazev = "";
       
            Datum = "202,10,2";

            Cena = 1000;
            Dph = 50;
        }


        public Vozidlo(string nazev, string datum, double cena, double dph)
        {
            Nazev = nazev;
            Datum = datum;
            Cena = cena;
            Dph = dph;
        }

        public string Nazev { get => nazev; set => nazev = value; }
 
        public string  Datum { get => datum; set => datum = value; }
        public double Cena { get => cena; set => cena = value; }
        public double Dph { get => dph; set => dph = value; }
    }
}
