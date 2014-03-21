using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.Strategies
{
    public class RandomSolver : ISolver
    {
        private Random rand;
        private Array directions = Enum.GetValues(typeof(Direction));

        public RandomSolver()
            : this(new Random())
        {
        }

        public RandomSolver(Random rand)
        {
            this.rand = rand;
        }

        public Direction GetNextMove(IGameBoard board, bool lastMoveResult)
        {
            return (Direction)directions.GetValue(rand.Next(directions.Length));
        }
    }
}
