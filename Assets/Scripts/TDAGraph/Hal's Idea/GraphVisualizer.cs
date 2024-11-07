using System.Collections.Generic;
using UnityEngine;

public class GraphVisualizer : MonoBehaviour
{
    public Graph<string> graph;
    public GameObject nodePrefab; // Assign a prefab for nodes (e.g., a simple sphere)

    void Start()
    {
        VisualizeGraph();
    }

    void VisualizeGraph()
    {
        if (graph == null || nodePrefab == null)
        {
            Debug.LogError("Graph or nodePrefab is not assigned.");
            return;
        }

        // Dictionary to store node positions (you can assign positions based on your needs)
        Dictionary<string, Vector3> nodePositions = new Dictionary<string, Vector3>();

        foreach (var node in graph.GetNeighbors("A"))
        {
            // Create nodes as spheres
            GameObject nodeObject = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity);
            nodeObject.name = node.Item1.ToString();  // Set name of the node as its identifier
            nodePositions[node.Item1] = nodeObject.transform.position;

            // Draw the edges
            var neighbors = graph.GetNeighbors(node.Item1);
            foreach (var neighbor in neighbors)
            {
                GameObject edge = new GameObject("Edge");
                LineRenderer lineRenderer = edge.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, nodePositions[node.Item1]);
                lineRenderer.SetPosition(1, nodePositions[neighbor.Item1]);

                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default")) { color = Color.black }; // Set edge color
            }
        }
    }
}

