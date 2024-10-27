namespace BioinformaticsFirstProject;

[Flags]
public enum Direction
{
    CROSS = 1,
    LEFT = 2,
    TOP = 4,
}

public struct MatrixElement
{
    public int Score { get; set; }
    public Direction Direction { get; set; }
}
