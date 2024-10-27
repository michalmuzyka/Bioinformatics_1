namespace BioinformaticsFirstProject.SequenceLoader;

public class SequenceLoaderFromFile : ISequenceLoader
{
    private readonly string filepath;

    public SequenceLoaderFromFile(string filepath)
    {
        this.filepath = filepath;
    }

    public string Load()
    {
        return File.ReadAllText(filepath);
    }
}
