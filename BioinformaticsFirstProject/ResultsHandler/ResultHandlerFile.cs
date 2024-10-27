namespace BioinformaticsFirstProject.ResultsHandler;

public class ResultHandlerFile : IResultHandler
{
    private readonly string filename;

    public ResultHandlerFile(string outputFilename)
    {
        filename = outputFilename;
    }

    public void HandleResult(SequenceAlignmentResults results)
    {
        File.WriteAllText(filename, results.ToString());
    }
}
