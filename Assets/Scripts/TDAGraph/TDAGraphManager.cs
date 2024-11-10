using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TDAGraphManager : MonoBehaviour
{
    private TDAGraph_DictionaryVer graph;
    [SerializeField] private GameObject[] planets;
    [SerializeField] private GameObject ship;

    private TDAVertex currentVertex;

    private Stack<int> travelRoute = new Stack<int>();

    int tripCost = 0;

    private void Awake()
    {
        graph = new TDAGraph_DictionaryVer();

        for (int i = 0; i < planets.Length; i++)
        {
            int nodeValue = int.Parse(planets[i].GetComponentInChildren<TextMeshProUGUI>().text);
            graph.AddVertex(nodeValue);
        }

        graph.ShowVertices();

        CreateConnections();
    }

    private void Start()
    {
        ship.transform.position = new Vector3 (planets[0].transform.position.x, planets[0].transform.position.y + 0.5f, planets[0].transform.position.z);
        currentVertex = graph.FindVertex(1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = CastRay();
            if (hit.collider == null) return;

            TravelToPlanet(hit);
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = CastRay();
            if (hit.collider == null) return;

            ShowPlanetConnections(hit);
        }
    }

    private void TravelToPlanet(RaycastHit2D hit)
    {
        GameObject node = hit.collider.gameObject;
        int nodeValue = int.Parse(node.GetComponentInChildren<TextMeshProUGUI>().text);

        tripCost = 0;

        if (graph.DoesEdgeExist(currentVertex.Id, nodeValue))
        {
            ship.transform.position = new Vector3(planets[nodeValue-1].transform.position.x, planets[nodeValue - 1].transform.position.y + 0.5f, planets[nodeValue - 1].transform.position.z);
            tripCost += graph.GetEdgeWeight(currentVertex.Id, nodeValue);

            Debug.Log($"Traveled to {nodeValue}, from {currentVertex.Id}, with a cost of {tripCost}.");

            currentVertex = graph.FindVertex(nodeValue);
        }
        else
        {
            travelRoute.Push(nodeValue);
            DesignPath(currentVertex.Id, nodeValue);
            if (travelRoute.Count > 1) 
            {
                StartCoroutine(ShipTravel(currentVertex.Id));
            }
            else
            {
                travelRoute.Clear();
                Debug.Log("Couldn't find a path to the node.");
            }
        }
    }

    private IEnumerator ShipTravel(int startingPoint)
    {
        int travelNode = 0;

        while (travelRoute.Count > 0)
        {
            travelNode = travelRoute.Pop();

            ship.transform.position = new Vector3(planets[travelNode - 1].transform.position.x, planets[travelNode - 1].transform.position.y + 0.5f, planets[travelNode - 1].transform.position.z);
            tripCost += graph.GetEdgeWeight(currentVertex.Id, travelNode);
            Debug.Log("Weight added:" + graph.GetEdgeWeight(currentVertex.Id, travelNode));

            currentVertex = graph.FindVertex(travelNode);

            yield return new WaitForSeconds(1f);
        }

        Debug.Log($"Traveled to {travelNode}, from {startingPoint}, with a cost of {tripCost}.");
    }

    private void DesignPath(int origin, int destiny)
    {
        foreach (var connections in graph.AdjacencyList)
        {
            foreach (var edge in connections.Value)
            {
                if (edge.Item1 == graph.FindVertex(destiny))
                {
                    if (connections.Key.Id == origin)
                    {
                        Debug.Log("Doing1");
                        return;
                    }
                    else
                    {
                        Debug.Log("Doing2");
                        travelRoute.Push(connections.Key.Id);
                        DesignPath(origin, connections.Key.Id);
                        return;
                    }
                }
            }
        }


    }

    private void ShowPlanetConnections(RaycastHit2D hit)
    {
        GameObject node = hit.collider.gameObject;
        int nodeValue = int.Parse(node.GetComponentInChildren<TextMeshProUGUI>().text);
        graph.ShowVertexConnections(nodeValue);
    }

    private RaycastHit2D CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
        }

        return hit;
    }

    private void CreateConnections()
    {
        graph.AddEdge(1, 2, 8);
        graph.AddEdge(1, 3, 2);
        graph.AddEdge(2, 4, 4);
        graph.AddEdge(2, 5, 9);
        graph.AddEdge(3, 4, 3);
        graph.AddEdge(3, 6, 5);
        graph.AddEdge(4, 8, 3);
        graph.AddEdge(5, 10, 7);
        graph.AddEdge(6, 7, 1);
        graph.AddEdge(6, 8, 2);
        graph.AddEdge(7, 9, 9);
        graph.AddEdge(7, 11, 15);
        graph.AddEdge(8, 9, 6);
        graph.AddEdge(9, 11, 11);
        graph.AddEdge(10, 9, 1);
        graph.AddEdge(10, 12, 8);
        graph.AddEdge(11, 12, 4);
    }
}
