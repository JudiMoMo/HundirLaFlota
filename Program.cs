using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace HundirLaFlota
{
    internal class Program
    {
        static char[,] tablero = { { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                              { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.'},
                            };

        static int[] cabecera = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        static char[] lateral = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        
        static char[] T = { 'T', 'T', 'T' };
        static char[] P = { 'P', 'P', 'P', 'P', 'P' };

        //static int[] coordenada = new int[2];
        static int[] coordenada = {0,0};


        static void Main(string[] args)
        {

            //HORA DE AVERIGUAR SI LE HAS DADO
            //JugarFacil(tablero, cabecera, lateral);

            JugarDificil();
        }

        public static void JugarDificil()
        {


            Random rnd = new Random();

            Mostrar();
            MeterBarco(T);
            MeterBarco(P);
            Console.Clear();
            Mostrar();

            bool todoHundido = false;
            while (todoHundido != true)
            {
                Console.WriteLine("Dime una coordenada a disparar. EJEMPLO C3");
                string coordenadas = Console.ReadLine();
                Disparar(coordenadas);
                todoHundido = ComprobarTodoHundido();
                Console.Clear();
                Mostrar();

            }
            
        }

        public static void Disparar(string coordenadas)
        {
            int letraFila = Convert.ToInt32(Convert.ToChar(coordenadas.Substring(0,1))) - Convert.ToInt32('A');
            int columna = Convert.ToInt32(coordenadas.Substring(1, coordenadas.Length -1));
            Console.WriteLine($"FILA: {letraFila} COLUMNA:{columna}");
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j=0; j< tablero.GetLength(1); j++)
                {
                    if (i==letraFila && j==columna)
                    {
                        tablero[i, j-1] = '*';
                    }
                }
            }
        }

        public static bool ComprobarTodoHundido()
        {
            for(int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j=0; j<tablero.GetLength(1); j++)
                {
                    if (tablero[i,j]=='T' || tablero[i,j]=='P')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void Mostrar()
        {
            Console.Write("\t");
            foreach (int number in cabecera)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();

            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                Console.Write(lateral[i] + " -");
                Console.Write("\t");
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    Console.Write($"{tablero[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        public static void MeterBarco(char[] barco)
        {
            if (CrearCoordenada(barco) == 0)
            {

                int i = coordenada[0];
                int j = coordenada[1];
                for (int k = 0;k < barco.Length; k++)
                {
                    tablero[i,j] = barco[k];
                    j++;
                }
            }
            else
            {
                int i = coordenada[0];
                int j = coordenada[1];
                for (int k = 0; k < barco.Length; k++)
                {
                    tablero[i, j] = barco[k];
                    i++;
                }

            }
        }

        public static int CrearCoordenada(char[] barco)
        {
            Random rnd = new Random();
            int fila = rnd.Next(0, 10);
            int columna = rnd.Next(0, 10);


            while (ComprobarSiSePuede(fila, columna, barco) == -1)
            {
                fila = rnd.Next(0, 10);
                columna = rnd.Next(0, 10);
            }



            coordenada[0] = fila;
            coordenada[1] = columna;
            return ComprobarSiSePuede(fila, columna, barco);
        }

        public static int ComprobarSiSePuede(int fila, int columna, char[] barco)
        {
            int sePuede = -1;
            if (CabeHorizontal(columna, barco))
            {
                if (!ContieneBarcoHorizontal(fila, columna, barco))
                {
                    sePuede = 0;
                    Console.WriteLine(sePuede);
                }
                
            }
            else
            {
                if (CabeVertical(fila, barco))
                {
                    if (!ContieneBarcoVertical(fila, columna, barco))
                    {
                        sePuede = 1;
                        Console.WriteLine(sePuede);
                    }
                        
                }
            }

            return sePuede;
            


        }

        public static bool CabeVertical( int columna, char[] barco)
        {
            Console.WriteLine("CABEVERTICAL");
            int cont = 0;
            for (int i = columna; i < tablero.GetLength(1); i++)
            {
                cont++;
            }

            if (cont >= barco.Length)
            {
                Console.WriteLine("CABE Y NO CONTIENE OTRO BARCO");
                return true;
            }
            else
            {
                Console.WriteLine("NO CABE O CONTIENE OTRO BARCO");
                return false;
            }
        }

        public static bool CabeHorizontal(int fila, char[] barco)
        {
            Console.WriteLine("CABEHORIZONTAL");
            int cont = 0;
            for (int i = fila; i < tablero.GetLength(0); i++)
            {
                cont++;
            }

            if (cont >= barco.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ContieneBarcoHorizontal(int fila, int columna, char[] barco)
        {
            //COMPROBACION POR FILAS

            int columnMod = columna;
            for (int i = 0; i < barco.Length; i++)
            {
                if (tablero[fila, columnMod] != '.')
                {
                    return true;
                }
                columnMod++;
            }
            return false;
        }

        public static bool ContieneBarcoVertical(int fila, int columna, char[] barco)
        {
            int filasMod = fila;
            for (int i = 0; i < barco.Length; i++)
            {
                if (tablero[filasMod, columna] != '.')
                {
                    return true;
                }
                filasMod++;
            }

            return false;


        }


    }
}