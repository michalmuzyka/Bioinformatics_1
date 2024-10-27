using BioinformaticsFirstProject;
using BioinformaticsFirstProject.AlgorithmStrategy;
using BioinformaticsFirstProject.ResultsHandler;
using BioinformaticsFirstProject.SequenceLoader;

namespace BioinformaticsFirstProjectUnitTests;

public class SmithWatermanTests
{
    private void ExecuteWithAlgorithm(Action<SequenceAlignmentAlgorithmExecutor, ResultHandlerConst> function)
    {
        var alg = new SequenceAlignmentAlgorithmExecutor(new SmithWatermanCalculationStrategy());
        var resultHandler = new ResultHandlerConst();
        function(alg, resultHandler);
    }

    [Fact]
    public void SmithWatermanTest_ATAT()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("ATAT"),
                new SequenceLoaderConst("TATA"),
                resultHandler,
                maxOptimalAlignments: 3
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                15,
                new List<(string seq1, string seq2)>{
                    ("TAT", "TAT"),
                    ("ATA", "ATA")
                }
            ));
        });
    }


    [Fact]
    public void SmithWatermanTest_LongSameSequence()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("GTACCTAGCTTACTATGTACACAGTAGCACCCAGTTAGACTAGACTACCATATTTATGAGTATTTAGAGAGATACCACATAGACAGAC"),
                new SequenceLoaderConst("GTACCTAGCTTACTATGTACACAGTAGCACCCAGTTAGACTAGACTACCATATTTATGAGTATTTAGAGAGATACCACATAGACAGAC"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                440,
                new List<(string seq1, string seq2)>{
                    ("GTACCTAGCTTACTATGTACACAGTAGCACCCAGTTAGACTAGACTACCATATTTATGAGTATTTAGAGAGATACCACATAGACAGAC",
                     "GTACCTAGCTTACTATGTACACAGTAGCACCCAGTTAGACTAGACTACCATATTTATGAGTATTTAGAGAGATACCACATAGACAGAC"),
                }
            ));
        });
    }

    [Fact]
    public void SmithWatermanTest_ShortSubSequenceInLongSequence()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("TTTTTTTTTTTAGCATTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT"),
                new SequenceLoaderConst("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                20,
                new List<(string seq1, string seq2)>{
                    ("AGCA", "AGCA"),
                }
            ));
        });
    }

    [Fact]
    public void SmithWatermanTest_TwoShortSubSequencesInLongSequence()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("TTTTTTAGCATTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTACGATTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT"),
                new SequenceLoaderConst("AAAAAAAACGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                20,
                new List<(string seq1, string seq2)>{
                    ("AGCA", "AGCA"),
                    ("ACGA", "ACGA"),
                }
            ));
        });
    }

    [Fact]
    public void SmithWatermanTest_TwoShortOverlappingSubSequencesInLongSequence()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("TTTTTTAGCATTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTACGATTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT"),
                new SequenceLoaderConst("AGCACGA"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                20,
                new List<(string seq1, string seq2)>{
                    ("AGCA", "AGCA"),
                    ("ACGA", "ACGA"),
                }
            ));
        });
    }

    [Fact]
    public void SmithWatermanTest_SubsequenceWithGapInLongSequence()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTAGTTCATTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT"),
                new SequenceLoaderConst("TAGCAT"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                26,
                new List<(string seq1, string seq2)>{
                    ("TAGTTCAT", "TAG--CAT"),
                }
            ));
        });
    }

    [Fact]
    public void SmithWatermanTest_LongTSequenceWithManySubsequences()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("TATATTTTTTTTTTTTTTATGTTTTTTTTTTTTTTTTTTTTTTGTCTTTTTTTTTTTTTTTTTTTTTTTTTCTATTTTTTTTTTTTTATC"),
                new SequenceLoaderConst("AAGCAC"),
                resultHandler,
                maxOptimalAlignments: 10
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                9,
                new List<(string seq1, string seq2)>{
                    ("ATG", "AAG"),
                    ("ATC", "AGC"),
                }
            ));
        });
    }


    [Fact]
    public void SmithWatermanTest_LongASequenceWithOneSubsequences()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("ATATAAAAAATAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"),
                new SequenceLoaderConst("TTGCGTC"),
                resultHandler,
                maxOptimalAlignments: 10
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                9,
                new List<(string seq1, string seq2)>{
                    ("TAG", "TTG"),
                }
            ));
        });
    }


    [Fact]
    public void SmithWatermanTest_LongASequenceWithManySubsequences()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("ATATAAAAAATAGAAAAAAAAAAGACAAAAAACAGAAAAAAAAGAGAAAAAAAAAAAAGATAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"),
                new SequenceLoaderConst("TGCGGT"),
                resultHandler,
                maxOptimalAlignments: 10
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                8,
                new List<(string seq1, string seq2)>{
                    ("TAG", "T-G"),
                    ("GAC", "G-C"),
                    ("CAG", "C-G"),
                    ("GAG", "G-G"),
                    ("GAT", "G-T"),
                }
            ));
        });
    }
}