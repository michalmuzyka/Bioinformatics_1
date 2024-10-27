using System.Text;

namespace BioinformaticsFirstProject;

public class SimilarityMatrixLogger
{
    private readonly string filepathPrefix;

    public SimilarityMatrixLogger(string filepathPrefix)
    {
        this.filepathPrefix = filepathPrefix;
    }

    public void LogSimilarityMatrix(string sequence1, string sequence2, MatrixElement[,] similarityMatrix)
    {
        var sbScore = new StringBuilder();
        var sbDirection = new StringBuilder();

        sbDirection.Append(";;");
        sbScore.Append(";;");
        sbDirection.AppendLine(string.Join(';', sequence1.Select(c => c)));
        sbScore.AppendLine(string.Join(';', sequence1.Select(c => c)));

        for (int i = 0; i < similarityMatrix.GetLength(0); i++)
        {
            var space = i != 0 ? sequence2[i - 1] : ' ';
            sbDirection.Append($"{space};");
            sbScore.Append($"{space};");

            for (int j = 0; j < similarityMatrix.GetLength(1); j++)
            {
                sbScore.Append($"{similarityMatrix[i, j].Score};");
                sbDirection.Append($"{similarityMatrix[i, j].Direction};");
            }

            sbScore.AppendLine();
            sbDirection.AppendLine();
        }

        File.WriteAllText(filepathPrefix + "_score.csv", sbScore.ToString());
        File.WriteAllText(filepathPrefix + "_direction.csv", sbDirection.ToString());
    }
}
