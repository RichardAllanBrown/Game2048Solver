using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Solver.Strategies
{
    public interface ISolver
    {
        Direction GetNextMove(IGameBoard board, bool lastMoveResult);
    }
}
