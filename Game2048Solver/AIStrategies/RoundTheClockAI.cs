using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.AIStrategies
{
    class RoundTheClockAI : IStrategy
    {
        private int call = 1;

        public Direction GetNextMove(IGameBoard board, bool lastMoveResult)
        {
            call++;
            if (call > 4)
                call = 1;

            switch (call)
            {
                case 1:
                    return Direction.Up;

                case 2:
                    return Direction.Left;

                case 3:
                    return Direction.Down;

                case 4:
                    return Direction.Right;

                default:
                    throw new Exception("TILT - Unable to get here, investigate");
            }
        }
    }
}
