using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsFirstProject.AlgorithmStrategy;

public class NeedlemanWunschCalculationStrategy : ISequenceAlignmentStrategy
{
    public void AssignScoreAndDirection(int i, int j, MatrixElement[,] similarityMatrix, int calculatedScore, Direction direction)
    {
        if (calculatedScore >= similarityMatrix[i, j].Score)
        {
            if (calculatedScore == similarityMatrix[i, j].Score)
                similarityMatrix[i, j].Direction |= direction;
            else
            {
                similarityMatrix[i, j].Direction = direction;
                similarityMatrix[i, j].Score = calculatedScore;
            }
        }
    }

    public List<(int i, int j)> GetFirstElementsOfAlignment(MatrixElement[,] similarityMatrix)
    {
        return new List<(int i, int j)> { (similarityMatrix.GetLength(0) - 1, similarityMatrix.GetLength(1) - 1) };
    }

    public bool ShouldStopTracingBack(MatrixElement[,] similarityMatrix, int i, int j)
    {
        return i == 0 && j == 0;
    }
}
