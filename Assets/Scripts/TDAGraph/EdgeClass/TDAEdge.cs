using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAEdge
{
    public bool isBidirectional {  get; private set; }
    public TDAVertex origin {  get; private set; }
    public TDAVertex target { get; private set; }
    public int Weight {  get; private set; }

    public TDAEdge(bool isBidirectional, TDAVertex origin, TDAVertex target, int weight)
    {
        this.isBidirectional = isBidirectional;
        this.origin = origin;
        this.target = target;
        Weight = weight;
    }
}
