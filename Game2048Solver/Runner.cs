using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game2048Solver.Strategies;

namespace Game2048Solver
{
    public class Runner
    {
        ISolver strat;

        public Runner(ISolver strat)
        {
            this.strat = strat;
        }

        public IGameBoard SolveToEnd(IGameBoard board)
        {
            bool lastMove = true;

            while (!board.GameOver)
            {
                lastMove = board.Move(strat.GetNextMove(board, lastMove));
            }

            return board;
        }
    }
}
