using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqLiteBeolvas
{
    internal class Games
    {
        string nev;
        double ertekeles;
        int ar;
        string kiado;

        public Games(string nev, double ertekeles, int ar, string kiado)
        {
            this.nev = nev;
            this.ertekeles = ertekeles;
            this.ar = ar;
            this.kiado = kiado;
        }

        public string Nev { get => nev; set => nev = value; }
        public double Ertekeles { get => ertekeles; set => ertekeles = value; }
        public int Ar { get => ar; set => ar = value; }
        public string Kiado { get => kiado; set => kiado = value; }
    }
}
