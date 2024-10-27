using BioinformaticsFirstProject.AlgorithmStrategy;
using BioinformaticsFirstProject.ResultsHandler;
using BioinformaticsFirstProject.SequenceLoader;

namespace BioinformaticsFirstProject;

internal class Program
{
    static void Main(string[] args)
    {
        var alg = new SequenceAlignmentAlgorithmExecutor(new SmithWatermanCalculationStrategy());
              
        alg.Execute(
            new SequenceLoaderFromFile("TATA.txt"),
            new SequenceLoaderFromFile("ATAT.txt"),
            new ResultHandlerFile("res_ATAT.txt"),
            maxOptimalAlignments: 5,
            matrixLogger: new SimilarityMatrixLogger("ATAT")
        );
    }
}
