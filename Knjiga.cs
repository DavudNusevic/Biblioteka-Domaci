using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteka
{
    class Knjiga
    {
        public string Sifra;
        public string Naziv;
        public string Zanr;
        public int BrojPrimeraka;

        public Knjiga() { }
        public Knjiga(string s, string n, string z, int b)
        {
            Sifra = s;
            Naziv = n;
            Zanr = z;
            BrojPrimeraka = b;
        }
    }
}
