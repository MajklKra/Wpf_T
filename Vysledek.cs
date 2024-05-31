using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_T
{

    /// <summary>
    /// Class being used for final display in the tables
    /// </summary>

    public class Vysledek
    {

        private string s;

        private string dph;

        public string S { get => s; set => s = value; }

        public string Dph { get => dph; set => dph = value; }

        public Vysledek(string s, string dph)
        {
            S = s;
            Dph = dph;
        }

        public Vysledek()
        {
            
        }
    }
}
