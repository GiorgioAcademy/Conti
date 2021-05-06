using System;

namespace Conti
{
    class Program
    {
        static void Main(string[] args)
        {
            Conto c1 = new Conto("Pippo");
            Conto c2 = new Conto("Pluto");

            Conto[] conti = new Conto[100];

            conti[0] = c1;
            conti[1] = c2;
            conti[2] = new Conto("Pinco");

            c1.Versa(100.50m);
            c1.Versa(150);

            c2.Versa(500);
            c1.Preleva(100);

            c2.Preleva(50);

            // c1.Saldo = 1000000;

            Console.WriteLine($"Il saldo del conto di {c1.Intestatario} è {c1.Saldo}");
            Console.WriteLine($"Il saldo del conto di {c2.Intestatario} è {c2.Saldo}");
        }
    }
}
