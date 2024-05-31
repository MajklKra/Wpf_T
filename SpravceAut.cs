using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Vozidla;

namespace Wpf_T
{
    /// <summary>
    /// Class for managing collections of vehicles
    /// </summary>
    public class SpravceAut
    {

        public ObservableCollection<Auto> auta = new ObservableCollection<Auto>();
        
        /// <summary>
        /// Collection vozidla2 is used for loading data from XML file 
        /// </summary>

        public ObservableCollection<Vozidlo> vozidla2 = new ObservableCollection<Vozidlo>();

        /// <summary>
        /// Collection vozidla3 is filter only vehicles which had been bought/sold at weekends 
        /// </summary>

        public ObservableCollection<Vozidlo> vozidla3 = new ObservableCollection<Vozidlo>();

        /// <summary>
        /// Collection result bearing prices in a string format
        /// </summary>

        public ObservableCollection<Vysledek> result = new ObservableCollection<Vysledek>();


        /// <summary>
        /// This is list of products sold at weekends
        /// </summary>

        List<string> vyrobky = new List<string>();

        /// <summary>
        /// This dictionary is used for prices for each brand of car without AddedValueTax
        /// </summary>

        Dictionary<string, double> ceny = new Dictionary<string, double>();

        /// <summary>
        /// This dictionary is used for values of AddedValueTax
        /// </summary>

        Dictionary<string, double> dph = new Dictionary<string, double>();

        string path = "soubor.xml";
        
        /// <summary>
        /// Constructor of the class 
        /// </summary>

        public SpravceAut()
        {
            string dir2 = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;

            string cesta = " ";

            cesta = dir2 + "\\soubor3.xml";

            auta.Add(new Auto("Skoda Oktavia", new DateOnly(2010, 12, 2), 500000, 20));
            auta.Add(new Auto("Skoda Felicia", new DateOnly(2000, 12, 3), 21000, 20));
            auta.Add(new Auto("Skoda Fabia", new DateOnly(2010, 12, 4), 350000, 20));
            auta.Add(new Auto("Skoda Oktavia", new DateOnly(2010, 12, 4), 500000, 20));
            auta.Add(new Auto("Skoda Oktavia", new DateOnly(2010, 12, 5), 500000, 20));

            Vozidlo vv = new Vozidlo();

            XmlDocument doc = new XmlDocument();
            
            path = cesta;

            doc.Load(path);

            XmlNode root = doc.DocumentElement;

            Console.WriteLine();
            
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name == "vozidlo")
                {
                    XmlElement vozidlo = (XmlElement)node;

                    string nazev = vozidlo.GetElementsByTagName("nazev")[0].InnerText;

                    vv.Nazev = nazev;

                    DateTime datum = DateTime.Parse(vozidlo.GetElementsByTagName("datum")[0].InnerText);

                    DateOnly d = DateOnly.FromDateTime(datum);

                    vv.Datum = d.ToString();

                    double cena = double.Parse(vozidlo.GetElementsByTagName("cena")[0].InnerText);

                    vv.Cena = cena;

                    double dph = double.Parse(vozidlo.GetElementsByTagName("dph")[0].InnerText);
                    vv.Dph = dph;

                    vozidla2.Add(vv);

                    Vozidlo v = new Vozidlo(nazev, d.ToString(), cena, dph);

                    vv = new Vozidlo();
                }
            }

            foreach (var v in vozidla2)

            {
            
                if ((DateTime.Parse(v.Datum).DayOfWeek == DayOfWeek.Saturday) || (DateTime.Parse(v.Datum).DayOfWeek == DayOfWeek.Sunday))
                {
                    vozidla3.Add(v);
                }
            }

            double sum = 0;
            double sumDPH = 0;

            foreach (var v in vozidla3)
            {
                vyrobky.Add(v.Nazev);
            }

            vyrobky = vyrobky.Distinct().ToList();

            Console.WriteLine();

            foreach (var s in vyrobky)
            {
                sum = 0;
                sumDPH = 0;

                foreach (var v in vozidla3)
                {
                    if (v.Nazev == s)
                    {
                        sum = sum + v.Cena;
                        sumDPH = sumDPH + (v.Cena + (v.Cena * (v.Dph / 100)));
                    }
                }

                ceny.Add(s, sum);
                dph.Add(s, sumDPH);
            }

            Conversion();
        }

        // ---------------------------------

        /// <summary>
        /// Constructo for the class 
        /// </summary>
        /// <param name="p_path">parameter of path </param>

        public SpravceAut(string p_path)
        {
 
            auta.Add(new Auto("Skoda Oktavia", new DateOnly(2010, 12, 2), 500000, 20));
            auta.Add(new Auto("Skoda Felicia", new DateOnly(2000, 12, 3), 21000, 20));
            auta.Add(new Auto("Skoda Fabia", new DateOnly(2010, 12, 4), 350000, 20));
            auta.Add(new Auto("Skoda Oktavia", new DateOnly(2010, 12, 4), 500000, 20));
            auta.Add(new Auto("Skoda Oktavia", new DateOnly(2010, 12, 5), 500000, 20));

            Vozidlo vv = new Vozidlo();

            XmlDocument doc = new XmlDocument();

            doc.Load(p_path);

            XmlNode root = doc.DocumentElement;

            Console.WriteLine();
  
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Name == "vozidlo")
                {
                    XmlElement vozidlo = (XmlElement)node;

                    string nazev = vozidlo.GetElementsByTagName("nazev")[0].InnerText;

                    vv.Nazev = nazev;

                    DateTime datum = DateTime.Parse(vozidlo.GetElementsByTagName("datum")[0].InnerText);

                    DateOnly d = DateOnly.FromDateTime(datum);

                    vv.Datum = d.ToString();

                    double cena = double.Parse(vozidlo.GetElementsByTagName("cena")[0].InnerText);

                    vv.Cena = cena;

                    double dph = double.Parse(vozidlo.GetElementsByTagName("dph")[0].InnerText);
                    vv.Dph = dph;

                    vozidla2.Add(vv);

                    Vozidlo v = new Vozidlo(nazev, d.ToString(), cena, dph);
                    
                    vv = new Vozidlo();
                }
            }

            foreach (var v in vozidla2)
            {

                if ((DateTime.Parse(v.Datum).DayOfWeek == DayOfWeek.Saturday) || (DateTime.Parse(v.Datum).DayOfWeek == DayOfWeek.Sunday))
                {
                    vozidla3.Add(v);
                }
            }

            double sum = 0;
            double sumDPH = 0;

            foreach (var v in vozidla3)
            {
                vyrobky.Add(v.Nazev);
            }

            vyrobky = vyrobky.Distinct().ToList();

            Console.WriteLine();

            foreach (var s in vyrobky)
            {
                sum = 0;
                sumDPH = 0;

                foreach (var v in vozidla3)
                {
                    if (v.Nazev == s)
                    {
                        sum = sum + v.Cena;
                        sumDPH = sumDPH + (v.Cena + (v.Cena * (v.Dph / 100)));
                    }
                }

                ceny.Add(s, sum);
                dph.Add(s, sumDPH);
            }


            Conversion();

        }

        // ------------------------------------------------


        /// <summary>
        /// Conversion prices to string format
        /// </summary>

        public void Conversion()
        {
            Vysledek v = new Vysledek();

            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            string s, s2;

            for (int i = 0; i< vyrobky.Count; i++) 
            {

                sb2.Append("\n");
                sb2.Append(dph.ElementAt(i).Value.ToString());

                sb.Append(vyrobky[i]);
                sb.Append("\n");
                sb.Append(ceny.ElementAt(i).Value.ToString());

                s = sb.ToString();
                s2 = sb2.ToString();

                v.S = s;
                v.Dph = s2;

                result.Add(v);

                v = new Vysledek();

                sb = new StringBuilder();
                s = null;

                sb2 = new StringBuilder();
                s2 = null;
            }

        }

    }
}
