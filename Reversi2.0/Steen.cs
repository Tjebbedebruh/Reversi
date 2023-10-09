using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{

    internal class Steen
    {
        public Point Locatie;
        public int Kleur = 2; //0 = rood, 1 = blauw, 2 = undefined
        public bool Bezet;
        public bool Plaatsbaar;


        public Steen()
        {

        }

        public Steen(Point aLocatie, int aKleur, bool aBezet, bool aPlaatsbaar)
        {
            Locatie = aLocatie;
            Kleur = aKleur;
            Bezet = aBezet;
            Plaatsbaar = aPlaatsbaar;
        }
    }
}
