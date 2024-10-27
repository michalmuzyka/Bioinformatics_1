using BioinformaticsFirstProject.AlgorithmStrategy;
using BioinformaticsFirstProject.ResultsHandler;
using BioinformaticsFirstProject.SequenceLoader;
using System.Text;

namespace BioinformaticsFirstProject;

public class SequenceAlignmentAlgorithmExecutor
{
    readonly ISequenceAlignmentStrategy Strategy;

    public SequenceAlignmentAlgorithmExecutor(ISequenceAlignmentStrategy strategy)
    {
        Strategy = strategy;
    }

    private MatrixElement[,] CreateSimilarityMatrix(int gapPenalty, string sequence1, string sequence2)
    {
        var similarityMatrix = new MatrixElement[sequence1.Length + 1, sequence2.Length + 1];

        similarityMatrix[0, 0].Score = 0;
        for (int i = 0; i < sequence1.Length + 1; ++i)
        {
            for (int j = 0; j < sequence2.Length + 1; ++j)
            {
                if (i == 0 && j == 0)
                    continue;

                similarityMatrix[i, j].Score = int.MinValue;
                similarityMatrix[i, j].Direction = default;

                if (i > 0)
                {
                    var calculated = similarityMatrix[i - 1, j].Score + gapPenalty;
                    Strategy.AssignScoreAndDirection(i, j, similarityMatrix, calculated, Direction.TOP);
                }

                if (j > 0)
                {
                    var calculated = similarityMatrix[i, j - 1].Score + gapPenalty;
                    Strategy.AssignScoreAndDirection(i, j, similarityMatrix, calculated, Direction.LEFT);
                }

                if (i > 0 && j > 0)
                {
                    var calculated = similarityMatrix[i - 1, j - 1].Score + SubstitutionMatrix.Instance[sequence1[i - 1], sequence2[j - 1]];
                    Strategy.AssignScoreAndDirection(i, j, similarityMatrix, calculated, Direction.CROSS);
                }
            }
        }

        return similarityMatrix;
    }

    private SequenceAlignmentResults TraceBackAlignments(
            int maxOptimalAlignments,
            string sequence1, 
            string sequence2, 
            MatrixElement[,] similarityMatrix
        )
    {
        var results = new SequenceAlignmentResults();
        var beginOfAlignments = Strategy.GetFirstElementsOfAlignment(similarityMatrix);
        results.Score = similarityMatrix[beginOfAlignments.First().i, beginOfAlignments.First().j].Score;

        var stack = new Stack<(SequenceAlignmentPairSequence ps, int i, int j)>();
        beginOfAlignments.ForEach(beginOfAlignment =>
            stack.Push((new SequenceAlignmentPairSequence(), beginOfAlignment.i, beginOfAlignment.j))
        );

        while (stack.Count > 0)
        {
            var (ps, i, j) = stack.Pop();

            if (!Strategy.ShouldStopTracingBack(similarityMatrix, i, j))
            {
                var directions = similarityMatrix[i, j].Direction;
                var score = similarityMatrix[i, j].Score;

                bool shouldAddToStack() => stack.Count + results.PropableSequences.Count < maxOptimalAlignments;

                if (directions.HasFlag(Direction.CROSS) && shouldAddToStack())
                    stack.Push((new SequenceAlignmentPairSequence(ps, sequence1[i - 1], sequence2[j - 1]), i - 1, j - 1));

                if (directions.HasFlag(Direction.LEFT) && shouldAddToStack())
                    stack.Push((new SequenceAlignmentPairSequence(ps, null, sequence2[j - 1]), i, j - 1));

                if (directions.HasFlag(Direction.TOP) && shouldAddToStack())
                    stack.Push((new SequenceAlignmentPairSequence(ps, sequence1[i - 1], null), i - 1, j));
            }
            else
                results.PropableSequences.Add(ps);
        }

        return results;
    }

    public void Execute(
            ISequenceLoader Sequence1Loader,
            ISequenceLoader Sequence2Loader,
            IResultHandler resultHandler,
            int maxOptimalAlignments = 1,
            int gapPenalty = -2,
            SimilarityMatrixLogger? matrixLogger = null
        )
    {
        var sequence1 = Sequence1Loader.Load();
        var sequence2 = Sequence2Loader.Load();

        var similarityMatrix = CreateSimilarityMatrix(gapPenalty, sequence1, sequence2);

        if (matrixLogger != null)
           matrixLogger.LogSimilarityMatrix(sequence1, sequence2, similarityMatrix);

        var results = TraceBackAlignments(maxOptimalAlignments, sequence1, sequence2, similarityMatrix);
        resultHandler.HandleResult(results);
    }
}
