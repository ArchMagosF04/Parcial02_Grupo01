using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAVertex
{
    public int Value { get; private set; }
    //public List<TDAEdge> Edges;

    public TDAVertex(int value)
    {
        this.Value = value;
        //Edges = new List<TDAEdge>();
    }
}
