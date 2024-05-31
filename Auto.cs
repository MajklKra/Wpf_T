using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_T
{
    /// <summary>
    ///  Class auto which states attributes for a vehicle
    /// </summary>
    public class Auto
    {
        private string nazev;

        private DateOnly datum;

        private double cena;

        private double dph;

        public string Nazev { get => nazev; set => nazev = value; }
        
        public DateOnly Datum { get => datum; set => datum = value; }
        public double Cena { get => cena; set => cena = value; }
        public double Dph { get => dph; set => dph = value; }

        public Auto(string nazev, DateOnly datum, double cena, double dph)
        {
            Nazev = nazev;
            Datum = datum;
            Cena = cena;
            Dph = dph;
        }

    }
}
