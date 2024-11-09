using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAGraph_DictionaryVer
{
    public List<TDAVertex> VertexList;

    public Dictionary<TDAVertex, List<(TDAVertex, int)>> AdjacencyList;

    public TDAGraph_DictionaryVer()
    {
        Initialize();
    }

    private void Initialize()
    {
        VertexList = new List<TDAVertex>();
        AdjacencyList = new Dictionary<TDAVertex, List<(TDAVertex, int)>>();
    }

    public bool AddVertex(int value)
    {
        if (FindVertex(value) == null)
        {
            TDAVertex newVertex = new TDAVertex(value);
            VertexList.Add(newVertex);
            AdjacencyList.Add(newVertex, new List<(TDAVertex, int)>());
            return true;
        }

        return false;
    }

    public TDAVertex FindVertex(int index)
    {
        foreach (var vertex in VertexList)
        {
            if (index == vertex.Id)
            {
                return vertex;
            }
        }

        return null;
    }

    public void ShowVertices()
    {
        foreach (var vertex in VertexList)
        {
            Debug.Log("Vertex Value: " + vertex.Id);
        }
    }

    public void ShowVertexConnections(int value)
    {
        TDAVertex vertex = FindVertex(value);

        if (AdjacencyList.ContainsKey(vertex))
        {
            foreach(var edge in AdjacencyList[vertex])
            {
                Debug.Log($"{vertex.Id} is connected to {edge.Item1.Id}, with a weight of {edge.Item2}");
            }
        }
    }

    public bool AddEdge(int origin, int target, int weight)
    {
        if (!DoesEdgeExist(origin, target))
        {
            AdjacencyList[FindVertex(origin)].Add((FindVertex(target), weight));
        }

        return false;
    }

    public bool DoesEdgeExist(int origin, int target) 
    {
        TDAVertex current = FindVertex(origin);

        if (AdjacencyList.ContainsKey(current))
        {
            foreach (var edge in AdjacencyList[current])
            {
                if (edge.Item1 == FindVertex(target))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int GetEdgeWeight(int origin, int target)
    {
        if (DoesEdgeExist(origin, target))
        {
            foreach (var edge in AdjacencyList[FindVertex(origin)])
            {
                if (edge.Item1 == FindVertex(target))
                {
                    return edge.Item2;
                }
            }
        }

        return -1;
    }
}
