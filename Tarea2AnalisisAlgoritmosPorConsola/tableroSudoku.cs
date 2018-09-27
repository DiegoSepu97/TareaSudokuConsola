using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea2AnalisisAlgoritmosPorConsola
{
    class tableroSudoku
    {
        int[,] matriz;
        int dimension;
        int dimensioncaja;

        public tableroSudoku()
        {
            this.matriz = new int[9, 9];
            this.dimension = 9;
            this.dimensioncaja = 3;
            muestra();
            
        }

        public tableroSudoku(int n)
        {
            this.matriz = new int[9, 9];
            this.dimension = 9;
            this.dimensioncaja = 3;

            int cantidad = 10;

            for (int i = 1; i <= cantidad; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Intento " + i);
                Console.WriteLine();
                muestra();
                casillasInversas();
                if(cantSoluciones(0, 0, 0) == 1)
                {
                    imprimeTablero(); //Tablero con ceros
                    if (volverALlenar(0, 0))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Proceso Exitoso");
                        Console.WriteLine();
                    }
                }
                imprimeTablero();
            }
        }

        private void muestra()
        {
            llenarConCeros();
            llenarDiagonales();
            llenarResto(0, 3);
            imprimeTablero();
        }

        private void casillasInversas()
        {
            int cantidadEnBlanco = 20;
            Random r = new Random();
            int i, j, k, inversoI, inversoJ;

            for (k = 0; k < cantidadEnBlanco; k++)
            {
                i = r.Next(0, 8);
                j = r.Next(0, 8);

                inversoI = 8 - i;
                inversoJ = 8 - j;

                this.matriz[i, j] = 0;
                this.matriz[inversoI, inversoJ] = 0;

                //Console.Write("Posición (" + i + ", " + j +")\n");
            }
            // Console.WriteLine("La cantidad de comparaciones fueron : " + cont); Muestra la cantidad de comparaciones, en el peor caso debería ser 
        }

        private int cantSoluciones(int i, int j, int contador)
        {
            if(i == this.dimension)
            {
                i = 0;
                if (++j == this.dimension) return 1 + contador;
            }
            if (this.matriz[i, j] != 0) return cantSoluciones(i + 1, j, contador);

            for (int num = 1; (num <= this.dimension) && (contador < 2); num++)
            {
                if(sePuede(i,j,num))
                {
                    this.matriz[i, j] = num;

                    contador = cantSoluciones(i + 1, j, contador);
                }
            }
            this.matriz[i, j] = 0;
            return contador;
        }

        private void llenarConCeros()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    this.matriz[i, j] = 0;
        }

        private void llenarDiagonales()
        {
            for (int i = 0; i < this.dimension; i=i+this.dimensioncaja)
            {
                llenarCaja(i, i);
            }
        }

        private void llenarCaja(int fila, int columna)
        {
            int num;
            Random r = new Random();

            for (int i = 0; i < this.dimensioncaja; i++)
            {
                for (int j = 0; j < this.dimensioncaja; j++)
                {
                    do
                    {
                        num = r.Next(1, 10);
                    } while (!noEstaEnCaja(fila, columna, num));

                    this.matriz[i+fila, j+columna] = num;
                }
            }
        }

        private bool noEstaEnCaja(int filainicio, int columnainicio, int num)
        {
            for (int i = 0; i < this.dimensioncaja; i++)
                for (int j = 0; j < this.dimensioncaja; j++)
                    if (num == this.matriz[i + filainicio, j + columnainicio])
                        return false;
            return true;
        }

        private bool sePuede(int fila, int columna, int num)
        {
            return (noEstaEnFila(fila, num) && noEstaEnColumna(columna, num) && noEstaEnCaja(fila - (fila % 3), columna -(columna%3), num));
        }

        private bool noEstaEnFila(int fila, int num)
        {
            for (int columna = 0; columna < this.dimension; columna++)
                if (this.matriz[fila, columna] == num)
                    return false;
            return true;
        }

        private bool noEstaEnColumna(int columna, int num)
        {
            for (int fila = 0; fila < this.dimension; fila++)
                if (this.matriz[fila, columna] == num)
                    return false;
            return true;
        }

        private bool llenarResto(int i, int j)
        {
            if (j >= this.dimension && i < this.dimension - 1)
            {
                i = i + 1;
                j = 0;
            }
            if (i >= this.dimension && j >= this.dimension)
                return true;

            if (i < this.dimensioncaja)
            {
                if (j < this.dimensioncaja)
                    j = this.dimensioncaja;
            }
            else if (i < this.dimension - this.dimensioncaja)
            {
                if (j == (i / this.dimensioncaja) * this.dimensioncaja)
                    j = j + this.dimensioncaja;
            }
            else
            {
                if (j == this.dimension - this.dimensioncaja)
                {
                    i = i + 1;
                    j = 0;
                    if (i >= this.dimension)
                        return true;
                }
            }

            for (int num = 1; num <= this.dimension; num++)
            {
                if (sePuede(i, j, num))
                {
                    this.matriz[i,j] = num;
                    if (llenarResto(i, j + 1))
                        return true;

                    this.matriz[i, j] = 0;
                }
            }
            return false;

        }

        private bool volverALlenar(int i, int j)
        {
            if ((i < this.dimension) && (j < this.dimension))
            {
                if (this.matriz[i, j] != 0)
                {
                    if (j + 1 < this.dimension) return volverALlenar(i, j + 1);
                    else if (i + 1 < this.dimension) return volverALlenar(i + 1, 0);
                    else return true;
                }
                else
                {
                    for (int k = 1; k <= this.dimension; k++)
                    {
                        if(sePuede(i, j, k))
                        {
                            this.matriz[i, j] = k;
                            if (j + 1 < this.dimension)
                            {
                                if (volverALlenar(i, j + 1)) return true;
                                else this.matriz[i, j] = 0;
                            }
                            else if (i + 1 < this.dimension)
                            {
                                if (volverALlenar(i + 1, 0)) return true;
                                else this.matriz[i, j] = 0;
                            }
                            else return true;
                        }
                    }
                    return false;
                }
            }
            else return true;
        }

        public void imprimeTablero()
        {
            Console.WriteLine("##################################################");
            Console.WriteLine("");

            for (int i = 0; i < this.dimension; i++)
            {
                for (int j = 0; j < this.dimension; j++)
                {
                    if(j != 0)
                    {
                        Console.Write(" | ");
                    }
                    if(this.matriz[i, j] != 0)
                        Console.Write(" "+ this.matriz[i, j] +" ");
                    else
                        Console.Write(" " + "-" + " ");
                }
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------");
            }
            Console.WriteLine("");
            Console.WriteLine("##################################################");
        }
    }
}
