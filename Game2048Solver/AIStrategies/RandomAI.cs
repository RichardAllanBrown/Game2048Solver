﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.AIStrategies
{
    public class RandomAI : ISolver
    {
        private Random rand;
        private Array directions = Enum.GetValues(typeof(Direction));

        public RandomAI(Random rand)
        {
            this.rand = rand;
        }

        public Direction GetNextMove(IGameBoard board, bool lastMoveResult)
        {
            return (Direction)directions.GetValue(rand.Next(directions.Length));
        }
    }
}
