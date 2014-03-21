using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.AIStrategies
{
    public class LookAheadAI : ISolver
    {
        private int stepLimit;
        private Random randGen;
        private ISolver fallBackStrat;

        public LookAheadAI(int steps)
        {
            stepLimit = steps;
            randGen = new Random();
            fallBackStrat = new RandomAI(randGen);
        }

        public Direction GetNextMove(IGameBoard board, bool lastMoveResult)
        {
            Direction bestDir = fallBackStrat.GetNextMove(board, lastMoveResult);
            int bestScore = Int32.MinValue;

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (bestScore < ScoreAllPossibleBoards(board, direction))
                {
                    bestDir = direction;
                }
            }

            return bestDir;
        }

        private static int ScoreAllPossibleBoards(IGameBoard board, Direction direction)
        {
            int cumulativeScore = 0;
            int boardsTested = 0;

            foreach (IGameBoard gb in board.GetAllPossibleResults(direction))
            {
                cumulativeScore += ScoreSingleBoard(gb);
                boardsTested++;
            }

            if (boardsTested == 0)
                return Int32.MinValue;

            return (cumulativeScore / boardsTested);
        }

        private static int ScoreSingleBoard(IGameBoard gb)
        {
            int score = gb.Score;

            score += (gb.MaxValue * 4);

            if (gb.GameOver)
                score -= 2000;

            return score;
        }
    }
}
