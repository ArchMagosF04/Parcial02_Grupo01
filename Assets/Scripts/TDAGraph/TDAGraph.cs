using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAGraph
{
    public List<TDAVertex> vertexList;

    public Dictionary<TDAVertex, List<(TDAVertex, int)>> adjacencyList;

    public List<TDAEdge> edgesList;

    public TDAGraph()
    {
        Initialize();
    }

    public void Initialize()
    {
        vertexList = new List<TDAVertex>();
        adjacencyList = new Dictionary<TDAVertex, List<(TDAVertex, int)>>();
        edgesList = new List<TDAEdge>();
    }

    public bool AddVertex(int value)
    {
        foreach (var vertex in vertexList)
        {
            if (vertex.Value == value)
            {
                return false;
            }
        }

        TDAVertex newVertex = new TDAVertex(value);
        vertexList.Add(newVertex);

        return true;
    }

    public bool RemoveVertex(int value)
    {
        foreach (var vertex in vertexList)
        {
            if (vertex.Value == value)
            {
                vertexList.Remove(vertex);
                adjacencyList.Remove(vertex);
                return true;
            }
        }
        return false;
    }

    public TDAVertex FindVertex(int index) 
    {
        foreach (var vertex in vertexList)
        {
            if (index == vertex.Value)
            {
                return vertex;
            }
        }

        return null;
    }

    public void ShowVertices()
    {
        foreach (var vertex in vertexList)
        {
            Debug.Log("Vertex Value: " + vertex.Value);
        }
    }

    public bool AddEdge(int origin, int target, int weight)
    {
        if (!FindEdge(origin, target))
        {
            TDAEdge newEdge = new TDAEdge(false, FindVertex(origin), FindVertex(target), weight);
            return true;
        }
        return false;
    }

    public bool RemoveEdge(int origin, int target)
    {
        if (FindEdge(origin, target))
        {
            edgesList.Remove(GetEdge(origin, target));
        }
        return false;
    }

    public bool FindEdge(int origin, int target)
    {
        if (GetEdge(origin, target) != null) return true;

        return false;
    }

    public TDAEdge GetEdge(int origin, int target)
    {
        foreach (var edge in edgesList)
        {
            if (edge.origin.Value == origin && edge.target.Value == target)
            {
                return edge;
            }
        }

        return null;
    }

    public int GetEdgeWeight(int origin, int target)
    {
        if (FindEdge(origin, target))
        {
            return GetEdge(origin, target).Weight;
        }

        return -1;
    }   
}
