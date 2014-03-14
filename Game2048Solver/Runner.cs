using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game2048Solver.AIStrategies;

namespace Game2048Solver
{
    public class Runner
    {
        IStrategy strat;

        public Runner(IStrategy strat)
        {
            this.strat = strat;
        }

        public IGameBoard SolveToEnd(IGameBoard board)
        {
            while (!board.GameOver)
            {
                bool lastMove = false;
                lastMove = board.Move(strat.GetNextMove(board, lastMove));
            }

            return board;
        }
    }
}
