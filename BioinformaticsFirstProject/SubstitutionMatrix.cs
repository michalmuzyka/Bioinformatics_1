namespace BioinformaticsFirstProject;

public class SubstitutionMatrix
{
    private const string CSV_PATH = "DataFiles\\SubstitutionMatrix.csv";

    private readonly Dictionary<char, int> GENOME_TO_IDX = new Dictionary<char, int>()
    {
        { 'A', 0 },
        { 'G', 1 },
        { 'C', 2 },
        { 'T', 3 },
    };

    private int[][] InternalSubstitutionMatrix {get; set;}

    public static SubstitutionMatrix Instance { get; } = new SubstitutionMatrix();

    private SubstitutionMatrix() 
    {
        var contents = File.ReadAllText(CSV_PATH).Split('\n');
        var csv = from line in contents
                  select line.Split(',').ToArray();

        InternalSubstitutionMatrix = csv.Select(s => s.Select(x => int.Parse(x)).ToArray()).ToArray();
    }

    public int this[char x, char y]
    {
        get => InternalSubstitutionMatrix[GENOME_TO_IDX[x]][GENOME_TO_IDX[y]];
    }

}
