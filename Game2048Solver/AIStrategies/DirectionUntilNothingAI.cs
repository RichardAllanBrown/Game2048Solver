using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.AIStrategies
{
    public class DirectionUntilNothingAI : IStrategy
    {
        private Direction currentDirection = Direction.Up;

        public Direction GetNextMove(IGameBoard board, bool lastMoveResult)
        {
            if (lastMoveResult)
                return currentDirection;

            switch (currentDirection)
            {
                case Direction.Up:
                    currentDirection = Direction.Left;
                    break;

                case Direction.Left:
                    currentDirection = Direction.Down;
                    break;

                case Direction.Down:
                    currentDirection =  Direction.Right;
                    break;

                case Direction.Right:
                    currentDirection =  Direction.Up;
                    break;

                default:
                    throw new Exception("TILT - Cannot get here, what happened?");
            }

            return currentDirection;
        }
    }
}
