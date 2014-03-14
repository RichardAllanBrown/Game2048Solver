using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game2048Solver.AIStrategies;

namespace Game2048Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner runner = new Runner(new DirectionUntilNothingAI());
            var result = runner.SolveToEnd(new GameBoard());

            Console.WriteLine("Maximum Value:      " + result.MaxValue);
            Console.WriteLine("Score:              " + result.Score);
            Console.WriteLine("Moves Made:         " + result.Moves);
            Console.Write(result.ToString());

            Console.ReadLine();
        }
    }
}
