using System.Text;

namespace BioinformaticsFirstProject;

public class SequenceAlignmentPairSequence
{
    public List<char?> FirstSequence { get; set; }

    public List<char?> SecondSequence { get; set; }

    public SequenceAlignmentPairSequence()
    {
        FirstSequence = new List<char?>();
        SecondSequence = new List<char?>();
    }

    public SequenceAlignmentPairSequence(SequenceAlignmentPairSequence ps, char? first, char? second)
    {
        FirstSequence = new List<char?>() { first };
        FirstSequence.AddRange(ps.FirstSequence);
        SecondSequence = new List<char?>() { second };
        SecondSequence.AddRange(ps.SecondSequence);
    }

    public static string SequenceToString(List<char?> sequence)
    {
        return new string(sequence.Select(c => c == null ? '-' : c.Value).ToArray());
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine(SequenceToString(FirstSequence));
        sb.AppendLine(SequenceToString(SecondSequence));

        return sb.ToString();
    }
}

public class SequenceAlignmentResults
{
    public int Score { get; set; }

    public List<SequenceAlignmentPairSequence> PropableSequences { get; set; }

    public SequenceAlignmentResults()
    {
        PropableSequences = new List<SequenceAlignmentPairSequence>();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        int no = 1;
        foreach (var sequence in PropableSequences)
        {
            sb.AppendLine($"Global alignment no. {no}: ");
            sb.Append(sequence.ToString());
            sb.AppendLine($"Score: {Score}");
            sb.AppendLine();
            ++no;
        }

        return sb.ToString();
    }
}
