using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.Strategies
{
    public class LookAheadSolver : ISolver
    {
        private int searchDepth;
        private ISolver fallBackStrat = new RandomSolver();

        public LookAheadSolver()
            : this(1)
        {
        }

        public LookAheadSolver(int searchDepth)
        {
            this.searchDepth = searchDepth;
        }

        public Direction GetNextMove(IGameBoard board, bool lastMoveResult)
        {
            Direction bestDir = fallBackStrat.GetNextMove(board, lastMoveResult);
            int bestScore = Int32.MinValue;

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var candidateScore = ScoreAllPossibleBoards(board, direction, searchDepth);
                if (bestScore < candidateScore)
                {
                    bestDir = direction;
                    bestScore = candidateScore;
                }
            }

            return bestDir;
        }

        private int ScoreAllPossibleBoards(IGameBoard board, Direction direction, int depthToSearch)
        {
            int cumulativeScore = 0;
            int boardsTested = 0;

            var possibleBoardsAfterMove = board.GetAllPossibleResults(direction);

            foreach (IGameBoard gb in possibleBoardsAfterMove)
            {
                cumulativeScore += ScoreSingleBoard(gb);
                boardsTested++;
            }

            if (boardsTested == 0)
            {
                return 0;
            }

            if (depthToSearch <= 1)
            {
                return (cumulativeScore / boardsTested);
            }

            int depthTotal = 0;
            foreach (IGameBoard gb in possibleBoardsAfterMove)
            {
                foreach (Direction d in Enum.GetValues(typeof(Direction)))
                {
                    depthTotal += ScoreAllPossibleBoards(gb, d, depthToSearch - 1);
                }
            }

            return depthTotal;
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
