using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDALabyrinth
{
    //public List<MazeNode> VertexList;

    //public Dictionary<MazeNode, List<MazeNode>> AdjacencyList;

    //public MazeNode endNode;

    //public TDALabyrinth()
    //{
    //    Initialize();
    //}

    //private void Initialize()
    //{
    //    VertexList = new List<MazeNode>();
    //    AdjacencyList = new Dictionary<MazeNode, List<MazeNode>>();
    //}

    //public bool AddVertex(Vector2 coordinates)
    //{
    //    if (FindVertex(coordinates) == null)
    //    {
    //        MazeNode newVertex = new MazeNode(coordinates);
    //        VertexList.Add(newVertex);
    //        AdjacencyList.Add(newVertex, new List<MazeNode>());
    //        return true;
    //    }

    //    return false;
    //}

    //public MazeNode FindVertex(Vector2 index)
    //{
    //    foreach (var vertex in VertexList)
    //    {
    //        if (index == vertex.GraphCoordinate)
    //        {
    //            return vertex;
    //        }
    //    }

    //    return null;
    //}

    //public void ShowVertices()
    //{
    //    foreach (var vertex in VertexList)
    //    {
    //        Debug.Log($"Vertex Value: ({vertex.GraphCoordinate.x}, {vertex.GraphCoordinate.y})");
    //    }
    //}

    //public void ShowVertexConnections(Vector2 coordinates)
    //{
    //    MazeNode vertex = FindVertex(coordinates);

    //    if (AdjacencyList.ContainsKey(vertex))
    //    {
    //        foreach (var edge in AdjacencyList[vertex])
    //        {
    //            Debug.Log($"({vertex.GraphCoordinate.x}, {vertex.GraphCoordinate.y}) is connected to ({edge.GraphCoordinate.x}, {edge.GraphCoordinate.y}");
    //        }
    //    }
    //}

    //public bool AddEdge(Vector2 origin, Vector2 target)
    //{
    //    if (!DoesEdgeExist(origin, target))
    //    {
    //        AdjacencyList[FindVertex(origin)].Add((FindVertex(target)));
    //    }

    //    return false;
    //}

    //public bool DoesEdgeExist(Vector2 origin, Vector2 target)
    //{
    //    MazeNode current = FindVertex(origin);

    //    if (AdjacencyList.ContainsKey(current))
    //    {
    //        foreach (var edge in AdjacencyList[current])
    //        {
    //            if (edge == FindVertex(target))
    //            {
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}
}
