using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.AIStrategies
{
    public interface IStrategy
    {
        Direction GetNextMove(IGameBoard board, bool lastMoveResult);
    }
}
