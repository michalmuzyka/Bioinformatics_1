namespace BioinformaticsFirstProject.AlgorithmStrategy;

public class SmithWatermanCalculationStrategy : ISequenceAlignmentStrategy
{
    public void AssignScoreAndDirection(int i, int j, MatrixElement[,] similarityMatrix, int calculatedScore, Direction direction)
    {
        calculatedScore = calculatedScore < 0 ? 0 : calculatedScore;
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
        int maxV = int.MinValue;
        var maximumElements = new List<(int i, int j)>();

        for (int _i = 0; _i < similarityMatrix.GetLength(0); _i++)
            for (int _j = 0; _j < similarityMatrix.GetLength(1); _j++)
            {
                var element = similarityMatrix[_i, _j].Score;
                if (element >= maxV)
                {
                    if (element == maxV)
                        maximumElements.Add((_i, _j));
                    else
                        maximumElements = new List<(int i, int j)>() { (_i, _j) };

                    maxV = similarityMatrix[_i, _j].Score;
                }
            }

        return maximumElements;
    }

    public bool ShouldStopTracingBack(MatrixElement[,] similarityMatrix, int i, int j)
    {
       return similarityMatrix[i, j].Score == 0;
    }
}
