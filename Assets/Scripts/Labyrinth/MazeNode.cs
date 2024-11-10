using System.Collections;
using System.Collections.Generic;

public class MazeNode
{
    public int Row { get; private set; }
    public int Column { get; private set; }

    public MazeNode(int row, int column)
    {
        Row = row;
        Column = column;
    }
}
