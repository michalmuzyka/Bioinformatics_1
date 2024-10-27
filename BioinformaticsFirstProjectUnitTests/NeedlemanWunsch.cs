using BioinformaticsFirstProject.AlgorithmStrategy;
using BioinformaticsFirstProject.ResultsHandler;
using BioinformaticsFirstProject.SequenceLoader;
using BioinformaticsFirstProject;

namespace BioinformaticsFirstProjectUnitTests;

public class NeedlemanWunschTests
{
    private void ExecuteWithAlgorithm(Action<SequenceAlignmentAlgorithmExecutor, ResultHandlerConst> function)
    {
        var alg = new SequenceAlignmentAlgorithmExecutor(new NeedlemanWunschCalculationStrategy());
        var resultHandler = new ResultHandlerConst();
        function(alg, resultHandler);
    }

    [Fact]
    public void NeedlemanWunschTest_TAT()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("TAT"),
                new SequenceLoaderConst("TT"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                8,
                new List<(string seq1, string seq2)>{
                    ("TAT", "T-T")
                }
            ));
        });
    }

    [Fact]
    public void NeedlemanWunschTest_ATAT()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("ATAT"),
                new SequenceLoaderConst("TATA"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                11,
                new List<(string seq1, string seq2)>{
                    ("-ATAT", "TATA-"),
                    ("ATAT-", "-TATA")
                }
            ));
        });
    }


    [Fact]
    public void NeedlemanWunschTest_LongSameSequence()
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
    public void NeedlemanWunschTest_GCAT()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("GCAT"),
                new SequenceLoaderConst("GACT"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                11,
                new List<(string seq1, string seq2)>{
                    ("G-CAT", "GAC-T"),
                    ("GCA-T", "G-ACT"),
                }
            ));
        });
    }

    [Fact]
    public void NeedlemanWunschTest_GCAAT()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("GCAAT"),
                new SequenceLoaderConst("GCAT"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                18,
                new List<(string seq1, string seq2)>{
                    ("GCAAT", "GCA-T"),
                    ("GCAAT", "GC-AT"),
                }
            ));
        });
    }

    [Fact]
    public void NeedlemanWunschTest_CAAAAAAAAAAAT_ExtendsMaxOptimal()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("CAAAAAAAAAAAT"),
                new SequenceLoaderConst("CAAAAAAAT"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                37,
                new List<(string seq1, string seq2)>{
                    ("CAAAAAAAAAAAT", "CAAAAAAA----T"),
                    ("CAAAAAAAAAAAT", "C-AAAAAAA---T"),
                    ("CAAAAAAAAAAAT", "C--AAAAAAA--T"),
                    ("CAAAAAAAAAAAT", "C---AAAAAAA-T"),
                    ("CAAAAAAAAAAAT", "C----AAAAAAAT"),
                }
            ));
        });
    }

    [Fact]
    public void NeedlemanWunschTest_TwoDifferentWithT()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("TT"),
                new SequenceLoaderConst("AA"),
                resultHandler,
                maxOptimalAlignments: 5
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                -2,
                new List<(string seq1, string seq2)>{
                    ("TT", "AA"),
                }
            ));
        });
    }

    [Fact]
    public void NeedlemanWunschTest_TwoDifferentWithoutT()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("GG"),
                new SequenceLoaderConst("AA"),
                resultHandler,
                maxOptimalAlignments: 15
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                -8,
                new List<(string seq1, string seq2)>{
                    ("--GG", "AA--"),
                    ("-G-G", "A-A-"),
                    ("G--G", "-AA-"),
                    ("G-G", "AA-"),
                    ("-GG", "AA-"),
                    ("-GG-", "A--A"),
                    ("G-G-", "-A-A"),
                    ("GG-", "A-A"),
                    ("GG--", "--AA"),
                    ("GG-", "-AA"),
                    ("-GG", "A-A"),
                    ("G-G", "-AA"),
                    ("GG", "AA"),
                }
            ));
        });
    }

    [Fact]
    public void NeedlemanWunschTest_ThreeDifferentWithT()
    {
        ExecuteWithAlgorithm((alg, resultHandler) =>
        {
            alg.Execute(
                new SequenceLoaderConst("GTT"),
                new SequenceLoaderConst("AAA"),
                resultHandler,
                maxOptimalAlignments: 15
            );

            Assert.NotNull(resultHandler.Results);
            Assert.True(ResultsComparer.CompareResults(
                resultHandler.Results,
                -6,
                new List<(string seq1, string seq2)>{
                    ("GTT-", "-AAA"),
                    ("GT-T", "-AAA"),
                    ("-GTT", "A-AA"),
                    ("G-TT", "-AAA"),
                    ("GTT", "AAA"),
                }
            ));
        });
    }
}