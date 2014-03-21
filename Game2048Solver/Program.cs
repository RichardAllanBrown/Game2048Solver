using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game2048Solver.Strategies;

namespace Game2048Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner runner = new Runner(new LookAheadSolver(4));

            DateTime start = DateTime.Now;
            var endBoard = runner.SolveToEnd(new GameBoard());

            TimeSpan timeTook = DateTime.Now - start;
            Console.WriteLine("Solver took:        " + timeTook.ToString());
            Console.WriteLine("Maximum Value:      " + endBoard.MaxValue);
            Console.WriteLine("Score:              " + endBoard.Score);
            Console.WriteLine("Moves Made:         " + endBoard.Moves);
            Console.Write(endBoard.ToString());

            Console.ReadLine();
        }
    }
}
