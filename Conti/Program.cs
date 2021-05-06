using System;
using System.IO;

namespace Conti
{
    enum Formato
    {
        Normale,
        CSV
    }


    class Program
    {
        private static Banca banca = new Banca();
        private static string fileName = @"banca.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("Benvenuta alla mia Banca");

            Carica();

            bool finito = false;

            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Crea un nuovo conto");
                Console.WriteLine("2. Versa su conto");
                Console.WriteLine("3. Preleva da conto");
                Console.WriteLine("4. Visualizza saldo di un conto");
                Console.WriteLine("5. Visualizza dati di un conto");
                Console.WriteLine("6. Visualizza prospetto completo");
                Console.WriteLine("7. Elimina conto");
                Console.WriteLine("8. Salva");
                Console.WriteLine("9. Carica");
                Console.WriteLine("0. Esci");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        CreaConto();
                        break;
                    case '2':
                        VersaSuConto();
                        break;
                    case '3':
                        PrelevaDaConto();
                        break;
                    case '4':
                        VisualizzaSaldo();
                        break;
                    case '5':
                        VisualizzaDatiConto();
                        break;
                    case '6':
                        VisualizzaProspetto();
                        break;
                    case '7':
                        EliminaConto();
                        break;
                    case '8':
                        Salva();
                        break;
                    case '9':
                        Carica();
                        break;
                    case '0':
                        finito = true;
                        break;
                    default:
                        Console.WriteLine("\nScelta errata");
                        break;
                }
            } while (!finito);

            Salva();
        }

        private static void Carica()
        {
            using (StreamReader sw = new StreamReader(fileName))
            {
                string contenuto = sw.ReadToEnd();
                banca.Carica(contenuto);
            }
        }

        private static void Salva()
        {
            using (StreamWriter sw = new StreamWriter(fileName))
                sw.Write(banca.OttieniProspetto(Formato.CSV));
        }

        private static void EliminaConto()
        {
            int numero;
            do
                Console.Write("Conto da eliminare: ");
            while (!int.TryParse(Console.ReadLine(), out numero));

            if (banca.EliminaConto(numero))
                Console.WriteLine($"Il conto {numero} è stato eliminato");
            else
                Console.WriteLine($"Conto {numero} non esistente");
        }

        private static void VisualizzaProspetto()
        {
            Console.WriteLine();
            Console.WriteLine(banca.OttieniProspetto(Formato.Normale));
        }

        private static void VisualizzaDatiConto()
        {
            int numero;
            do
                Console.Write("Conto di cui vedere i dati: ");
            while (!int.TryParse(Console.ReadLine(), out numero));

            if (banca.Esiste(numero))
                Console.WriteLine(banca.OttieniDatiConto(numero));
            else
                Console.WriteLine($"Conto {numero} non esistente");
        }

        private static void VisualizzaSaldo()
        {
            int numero;
            do
                Console.Write("Conto di cui vedere il saldo: ");
            while (!int.TryParse(Console.ReadLine(), out numero));

            if (banca.Esiste(numero))
                Console.WriteLine($"Il saldo del conto {numero} è {banca.OttieniSaldo(numero)}");
            else
                Console.WriteLine($"Conto {numero} non esistente");
        }

        private static void PrelevaDaConto()
        {
            int numero;

            do
                Console.Write("Conto da cui prelevare: ");
            while (!int.TryParse(Console.ReadLine(), out numero));

            decimal importo;
            do
                Console.Write("Importo da prelevare: ");
            while (!decimal.TryParse(Console.ReadLine(), out importo) ||
                    importo < 0);

            if (!banca.PrelevaDaConto(numero, importo))
                Console.WriteLine($"Conto {numero} non esistente");
        }

        private static void VersaSuConto()
        {
            int numero;

            do
                Console.Write("Conto su cui versare: ");
            while (!int.TryParse(Console.ReadLine(), out numero));

            if (banca.Esiste(numero))
            {
                decimal importo;
                do
                    Console.Write("Importo da versare: ");
                while (!decimal.TryParse(Console.ReadLine(), out importo) ||
                        importo < 0);

                banca.VersaSuConto(numero, importo);
            }
            else
                Console.WriteLine($"Conto {numero} non esistente");
        }

        static void CreaConto()
        {
            Console.WriteLine();
            // ottieni nominativo e crea un conto associato a esso
            string intestatario;
            do
            {
                Console.Write("Nome dell'intestatario: ");
                intestatario = Console.ReadLine();
            }
            while (intestatario.Length == 0);

            Conto conto = banca.CreaConto(intestatario);

            Console.WriteLine($"Conto numero {conto.Numero} creato per cliente {conto.Intestatario}");
        }
    }
}
