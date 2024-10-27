namespace BioinformaticsFirstProject.ResultsHandler;

public class ResultHandlerConst : IResultHandler
{
    public SequenceAlignmentResults? Results { get; set; }

    public void HandleResult(SequenceAlignmentResults results)
    {
        Results = results;
    }
}
