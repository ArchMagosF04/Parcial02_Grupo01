using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TDAGraph_ClassVer
{
    public List<TDAVertex> vertexList;

    public List<TDAEdge> edgesList;

    public TDAGraph_ClassVer()
    {
        Initialize();
    }

    public void Initialize()
    {
        vertexList = new List<TDAVertex>();
        edgesList = new List<TDAEdge>();
    }

    public bool AddVertex(int value)
    {
        if (FindVertex(value) == null)
        {
            TDAVertex newVertex = new TDAVertex(value);
            vertexList.Add(newVertex);
            return true;
        }

        return false;
    }

    public bool RemoveVertex(int value)
    {
        if (FindVertex(value) != null)
        {
            vertexList.Remove(FindVertex(value));

            foreach (var edge in edgesList)
            {
                if (edge.origin.Id == value || edge.target.Id == value)
                {
                    edgesList.Remove(edge);
                }
            }

            return true;
        }

        return false;
    }

    public TDAVertex FindVertex(int index) 
    {
        foreach (var vertex in vertexList)
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
        foreach (var vertex in vertexList)
        {
            Debug.Log("Vertex Value: " + vertex.Id);
        }
    }

    public bool AddEdge(int origin, int target, int weight)
    {
        if (GetEdge(origin, target) == null)
        {
            TDAEdge newEdge = new TDAEdge(false, FindVertex(origin), FindVertex(target), weight);
            return true;
        }
        return false;
    }

    public bool RemoveEdge(int origin, int target)
    {
        TDAEdge edge = GetEdge(origin, target);

        if (edge != null)
        {
            edgesList.Remove(edge);
        }
        return false;
    }

    public TDAEdge GetEdge(int origin, int target)
    {
        foreach (var edge in edgesList)
        {
            if (edge.origin.Id == origin && edge.target.Id == target)
            {
                return edge;
            }
        }

        return null;
    }


    public int GetEdgeWeight(int origin, int target)
    {
        TDAEdge edge = GetEdge(origin, target);

        if (edge != null)
        {
            return edge.Weight;
        }

        return -1;
    }   
}
