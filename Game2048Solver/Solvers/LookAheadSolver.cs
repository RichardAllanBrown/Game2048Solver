using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.Strategies
{
    public class LookAheadSolver : ISolver
    {
        private readonly int scoreMult = 1;
        private readonly int maxValMult = 10;
        private readonly int gameOverPen = -10000;
        private readonly int emptySquareMult = 100;

        private readonly int searchDepth;
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
            int bestScore = 0;

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
            int boardsExplored = 0;
            foreach (IGameBoard gb in possibleBoardsAfterMove)
            {
                foreach (Direction d in Enum.GetValues(typeof(Direction)))
                {
                    depthTotal += ScoreAllPossibleBoards(gb, d, depthToSearch - 1);
                    boardsExplored++;
                }
            }

            return depthTotal / boardsExplored;
        }

        private int ScoreSingleBoard(IGameBoard gb)
        {
            int score = gb.Score * scoreMult;
            score += gb.MaxValue * maxValMult;
            score += gb.GetEmptySquareCount() * emptySquareMult;

            if (gb.IsGameOver())
                score += gameOverPen;

            return score;
        }
    }
}
