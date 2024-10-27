namespace BioinformaticsFirstProject.SequenceLoader;

public class SequenceLoaderConst : ISequenceLoader
{
    private readonly string value;

    public SequenceLoaderConst(string value) 
    { 
        this.value = value;
    }

    public string Load()
    {
        return value;
    }
}
