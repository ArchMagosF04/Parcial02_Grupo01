using UnityEngine;

public class GraphTest : MonoBehaviour
{
    private Graph<string> graph;

    void Start()
    {
        // Create a new graph
        graph = new Graph<string>();

        // Add nodes and edges
        graph.AddEdge("A", "B", 10);
        graph.AddEdge("A", "C", 5);
        graph.AddEdge("B", "D", 2);
        graph.AddEdge("C", "D", 8);
        graph.AddEdge("D", "A", 1);

        // Visualize the graph
        graph.VisualizeGraph();

        // Test getting neighbors
        var neighborsOfA = graph.GetNeighbors("A");
        if (neighborsOfA != null)
        {
            foreach (var neighbor in neighborsOfA)
            {
                Debug.Log($"A -> {neighbor.Item1} (weight: {neighbor.Item2})");
            }
        }
    }
}

