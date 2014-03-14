using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver
{
    class Program
    {
        static void Main(string[] args)
        {
            IGameBoard board = new GameBoard();
            Random rand = new Random();
            Array moves = Enum.GetValues(typeof(Direction));

            while (!board.GameOver)
            {
                board.Move((Direction)moves.GetValue(rand.Next(moves.Length)));
            }

            Console.WriteLine("Moves Made:   " + board.Moves);
            Console.WriteLine("Score:        " + board.Score);
            Console.WriteLine("Max Value:    " + board.MaxValue);
            Console.WriteLine();
            Console.Write(board.ToString());

            Console.ReadLine();
        }
    }
}
