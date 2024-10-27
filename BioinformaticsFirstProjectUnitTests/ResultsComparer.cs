using BioinformaticsFirstProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsFirstProjectUnitTests;

internal static class ResultsComparer
{
    public static bool CompareSequences(SequenceAlignmentPairSequence ps, string desiredSeq1, string desiredSeq2)
    {
        if (ps.FirstSequence.Count != desiredSeq1.Length || ps.SecondSequence.Count != desiredSeq2.Length)
            return false;

        var seq1 = SequenceAlignmentPairSequence.SequenceToString(ps.FirstSequence);
        if(!string.Equals(seq1, desiredSeq1))
            return false;

        var seq2 = SequenceAlignmentPairSequence.SequenceToString(ps.SecondSequence);
        if (!string.Equals(seq2, desiredSeq2))
            return false;

        return true;
    }

    public static bool CompareResults(SequenceAlignmentResults results, int desiredScore, List<(string seq1, string seq2)> desiredSequences)
    {
        if (results.Score != desiredScore)
            return false;

        if (results.PropableSequences == null)
            return false;

        if (results.PropableSequences.Count != desiredSequences.Count)
            return false;

        foreach (var seq in desiredSequences)
        {
           var sequencesInResults = results.PropableSequences.Where(ps => CompareSequences(ps, seq.seq1, seq.seq2)).ToList();
           if (sequencesInResults.Count != 1)
                return false;
        }

        return true;
    }


}
