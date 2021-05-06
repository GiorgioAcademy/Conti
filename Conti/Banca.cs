using System;
using System.Collections.Generic;

namespace Conti
{
    class Banca
    {
        private Dictionary<int, Conto> _conti = new Dictionary<int, Conto>();
        private static int _num;    // per attribuire il numero al nuovo conto

        // prospetto che contiene tutti i dati di tutti i conti
        public string OttieniProspetto(Formato formato)
        {
            string s = "";

            foreach (Conto c in _conti.Values)
                s += c.OttieniDati(formato) + '\n';

            return s;
        }

        public bool Esiste(int numero)
        {
            return _conti.ContainsKey(numero);
        }

        public Conto CreaConto(string intestatario)
        {
            Conto conto = new Conto(++_num, intestatario);
            _conti.Add(conto.Numero, conto);

            return conto;
        }

        // se il conto non esiste verrà generata un'eccezione
        public void VersaSuConto(int numero, decimal importo)
        {
            _conti[numero].Versa(importo);
        }

        // con controllo interno dell'esistenza del conto
        // e conseguente restituzione di un valore booleano
        internal bool PrelevaDaConto(int numero, decimal importo)
        {
            if (Esiste(numero))
            {
                _conti[numero].Preleva(importo);
                return true;
            }
            return false;
        }

        internal decimal OttieniSaldo(int numero)
        {
            return _conti[numero].Saldo;
        }

        internal string OttieniDatiConto(int numero)
        {
            return _conti[numero].OttieniDati(Formato.Normale);
        }

        internal bool EliminaConto(int numero)
        {
            return _conti.Remove(numero);
        }

        internal void Carica(string contenuto)
        {
            _conti.Clear(); // rimuovo gli eventuali conti esistenti

            string[] righe = contenuto.Split('\n');
            foreach(string r in righe)
            {
                if (r == "")
                    continue;

                Conto c = new Conto(r);
                _conti.Add(c.Numero, c);
                _num = c.Numero;
            }
        }
    }
}
