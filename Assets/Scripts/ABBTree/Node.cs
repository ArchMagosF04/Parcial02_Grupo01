using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Node
{
    public int Value;
    public Node Left;
    public Node Right;
    public int Height;
    public int Depth;

    public Node(int value, int depth)
    {
        Value = value;
        Height = 1;
        Depth = depth;  
    }
}
