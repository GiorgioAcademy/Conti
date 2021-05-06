using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conti
{
    class Conto
    {
        public decimal Saldo { get; private set; }   // proprietà implcitamente definita
        public string Intestatario { get; }
        public int Numero { get; }  // il numero del conto in questione

        //private decimal _saldo;

        //public decimal Saldo
        //{
        //    get { return _saldo; }
        //    set { _saldo = value; }
        //}

        // costruisce un oggetto conto recuperando le sue informazioni
        // dalla stringa passata in formato Numero\tIntestatario\tSaldo
        public Conto(string dati)
        {
            string[] parti = dati.Split('\t');

            Numero = Convert.ToInt32(parti[0]);
            Intestatario = parti[1];
            Saldo = Convert.ToDecimal(parti[2]);
        }

        public Conto(int num, string intestatario)
        {
            if (intestatario == null || intestatario.Length == 0)
                throw new ArgumentNullException();

            Numero = num;

            Intestatario = intestatario;
        }

        public void Versa(decimal importo)
        {
            if (importo < 0)
                throw new ArgumentOutOfRangeException(nameof(importo),
                    "L'importo non può essere negativo");

            Saldo += importo;
        }

        public void Preleva(decimal importo)
        {
            if (importo < 0)
                throw new ArgumentOutOfRangeException(nameof(importo),
                    "L'importo non può essere negativo");

            Saldo -= importo;
        }

        public string OttieniDati(Formato formato)
        {
            switch (formato)
            {
                case Formato.Normale:
                    return $"Conto {Numero} intestato a {Intestatario}, saldo {Saldo:c}";
                case Formato.CSV:
                    return $"{Numero}\t{Intestatario}\t{Saldo}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
