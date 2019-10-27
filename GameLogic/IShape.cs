using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public interface IShape
    {
        // IFigure(Ga)
        TypeShape typeShape { get; }
        int MinRelationCount{ get; }
        List<Step> GetNextSteps(Step curStep);
        List<ShapeLocation> GetNeighbours(GameBoard board, ShapeLocation curLocation);
        bool IsMustDie(GameBoard board, ShapeLocation curLocation);
    }
}
