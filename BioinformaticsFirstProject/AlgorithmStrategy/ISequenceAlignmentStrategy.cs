namespace BioinformaticsFirstProject.AlgorithmStrategy;

public interface ISequenceAlignmentStrategy
{
    void AssignScoreAndDirection(int i, int j, MatrixElement[,] similarityMatrix, int calculatedScore, Direction direction);

    List<(int i, int j)> GetFirstElementsOfAlignment(MatrixElement[,] similarityMatrix);

    bool ShouldStopTracingBack(MatrixElement[,] similarityMatrix, int i, int j);

}
