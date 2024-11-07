using System.Collections.Generic;
using UnityEngine;

public class Graph<T> : MonoBehaviour
{
    // Dictionary to store the adjacency list: node -> list of (neighbor, weight)
    private Dictionary<T, List<(T, int)>> adjacencyList;

    // Constructor
    public Graph()
    {
        adjacencyList = new Dictionary<T, List<(T, int)>>();
    }

    // Add a node to the graph
    public void AddNode(T node)
    {
        if (!adjacencyList.ContainsKey(node))
        {
            adjacencyList[node] = new List<(T, int)>();
        }
    }

    // Add an edge between two nodes with a weight
    public void AddEdge(T from, T to, int weight)
    {
        // Ensure both nodes exist in the graph
        AddNode(from);
        AddNode(to);

        // Add the edge to the adjacency list (directed graph)
        adjacencyList[from].Add((to, weight));
    }

    // Get the neighbors of a node
    public List<(T, int)> GetNeighbors(T node)
    {
        if (adjacencyList.ContainsKey(node))
        {
            return adjacencyList[node];
        }
        return null;
    }

    // Visualize the graph (just a simple printout for now)
    public void VisualizeGraph()
    {
        foreach (var node in adjacencyList)
        {
            string nodeRepresentation = node.Key.ToString();
            foreach (var neighbor in node.Value)
            {
                nodeRepresentation += $" -> {neighbor.Item1} (weight: {neighbor.Item2})";
            }
            Debug.Log(nodeRepresentation);
        }
    }
}

