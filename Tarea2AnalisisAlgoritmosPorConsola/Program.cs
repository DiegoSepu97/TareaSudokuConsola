using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea2AnalisisAlgoritmosPorConsola
{
    class Program
    {
        static void Main(string[] args)
        { 
            //tableroSudoku s = new tableroSudoku();

            tableroSudoku mask = new tableroSudoku(5);


            Console.WriteLine();
            Console.Write("Press enter to exit... ");
            Console.ReadLine();
        }
    }
}
