using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    private TDAGraphGeneric<TDAVertex> graph;
    [SerializeField] private GameObject[] planets;
    [SerializeField] private GameObject ship;

    private TDAVertex currentVertex;

    private Stack<TDAVertex> nodesToTravel = new Stack<TDAVertex>();
    private List<TDAVertex> nodesExplored = new List<TDAVertex>();

    private int tripCost = 0;

    private bool pathFound = false;

    private Stack<TDAVertex>[] travelPoints;
    private bool[] hasCheckedItsConections;

    private void Awake()
    {
        graph = new TDAGraphGeneric<TDAVertex>();

        for (int i = 0; i < planets.Length; i++)
        {
            int nodeValue = int.Parse(planets[i].GetComponentInChildren<TextMeshProUGUI>().text);
            TDAVertex newVertex = new TDAVertex(nodeValue);
            graph.AddVertex(newVertex);
        }

        travelPoints = new Stack<TDAVertex>[planets.Length];
        hasCheckedItsConections = new bool[planets.Length];

        foreach (var vertex in graph.VertexList)
        {
            Debug.Log(vertex.Id);
        }

        CreateConnections();
    }

    private void Start()
    {
        ship.transform.position = new Vector3(planets[0].transform.position.x, planets[0].transform.position.y + 0.5f, planets[0].transform.position.z);
        currentVertex = graph.FindVertex(graph.VertexList[0]);
    }

    private void Update()
    {
        if (!LevelManager.Instance.isOpen)
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

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetShip();
            }
        }
    }

    private void ResetShip()
    {
        ship.transform.position = new Vector3(planets[0].transform.position.x, planets[0].transform.position.y + 0.5f, planets[0].transform.position.z);
        currentVertex = graph.FindVertex(graph.VertexList[0]);
    }

    private void TravelToPlanet(RaycastHit2D hit)
    {
        GameObject node = hit.collider.gameObject;
        int nodeValue = int.Parse(node.GetComponentInChildren<TextMeshProUGUI>().text);

        tripCost = 0;

        if (graph.DoesEdgeExist(currentVertex, graph.VertexList[nodeValue-1]))
        {
            ship.transform.position = new Vector3(planets[nodeValue - 1].transform.position.x, planets[nodeValue - 1].transform.position.y + 0.5f, planets[nodeValue - 1].transform.position.z);
            tripCost += graph.GetEdgeWeight(currentVertex, graph.VertexList[nodeValue - 1]);

            Debug.Log($"Traveled to {nodeValue}, from {currentVertex.Id}, with a cost of {tripCost}.");

            currentVertex = graph.FindVertex(graph.VertexList[nodeValue - 1]);
        }
        else
        {
            CalculatePath(currentVertex, graph.VertexList[nodeValue - 1]);
            if (pathFound)
            {
                StartCoroutine(ShipTravel(currentVertex.Id));
                pathFound = false;
            }
            else
            {
                Debug.Log($"Couldn't find a path to node {nodeValue} from node {currentVertex.Id}");
            }
        }
    }

    private IEnumerator ShipTravel(int startingPoint)
    {
        int travelNode = 0;

        while (nodesToTravel.Count > 0)
        {
            travelNode = nodesToTravel.Pop().Id;

            ship.transform.position = new Vector3(planets[travelNode - 1].transform.position.x, planets[travelNode - 1].transform.position.y + 0.5f, planets[travelNode - 1].transform.position.z);
            tripCost += graph.GetEdgeWeight(currentVertex, graph.VertexList[travelNode - 1]);
            Debug.Log("Weight added: " + graph.GetEdgeWeight(currentVertex, graph.VertexList[travelNode - 1]));

            currentVertex = graph.FindVertex(graph.VertexList[travelNode - 1]);

            yield return new WaitForSeconds(1f);
        }

        Debug.Log($"Traveled to {travelNode}, from {startingPoint}, with a cost of {tripCost}.");
    }

    private void GetEntryNodes(TDAVertex destiny)
    {
        if (travelPoints[destiny.Id - 1] == null)
        {
            travelPoints[destiny.Id - 1] = new Stack<TDAVertex>();
        }

        if (travelPoints[destiny.Id - 1].Count == 0 && !hasCheckedItsConections[destiny.Id-1])
        {
            foreach (var vertex in graph.VertexList)
            {
                if (graph.DoesEdgeExist(vertex, destiny))
                {
                    travelPoints[destiny.Id - 1].Push(vertex);
                }
            }

            hasCheckedItsConections[destiny.Id - 1] = true;
        }
    }

    private void CalculatePath(TDAVertex origin, TDAVertex destiny)
    {
        ClearData();


        GetEntryNodes(destiny);

        while (!CheckIfAllNodesWereTested())
        {
            if (travelPoints[destiny.Id - 1].Count > 0) 
            {
                nodesToTravel.Push(destiny);
                destiny = travelPoints[destiny.Id - 1].Pop();
            }
            else
            {
                if (!nodesExplored.Contains(destiny))
                {
                    nodesExplored.Add(destiny);
                }

                if (nodesToTravel.Count > 0)
                {
                    destiny = nodesToTravel.Pop();
                }
                else
                {
                    return;
                }
                
            }

            if (destiny == origin)
            {
                pathFound = true;
                return;
            }

            GetEntryNodes(destiny);
        }
    }

    private bool CheckIfAllNodesWereTested()
    {
        int nodesTested = 0;

        foreach (var vertex in graph.VertexList)
        {
            if (nodesExplored.Contains(vertex))
            {
                nodesTested++;
            }
        }

        if (nodesTested == graph.VertexList.Count - 1)
        {
            return true;
        }
        return false;
    }

    private void ClearData()
    {
        nodesExplored.Clear();
        nodesToTravel.Clear();
        for (int i = 0; i < hasCheckedItsConections.Length; i++)
        {
            hasCheckedItsConections[i] = false;
        }
        foreach (var stack in travelPoints)
        {
            if (stack != null) stack.Clear();
        }
    }

    private void ShowPlanetConnections(RaycastHit2D hit)
    {
        GameObject node = hit.collider.gameObject;
        int nodeValue = int.Parse(node.GetComponentInChildren<TextMeshProUGUI>().text);
        ShowVertexConnections(graph.VertexList[nodeValue-1]);
    }

    public void ShowVertexConnections(TDAVertex value)
    {
        if (graph.AdjacencyList.ContainsKey(value))
        {
            foreach (var edge in graph.AdjacencyList[value])
            {
                Debug.Log($"{value.Id} is connected to {edge.Item1.Id}, with a weight of {edge.Item2}");
            }
        }
    }

    private RaycastHit2D CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }

        return hit;
    }

    private void CreateConnections()
    {
        graph.AddEdge(graph.VertexList[0], graph.VertexList[1], 8);
        graph.AddEdge(graph.VertexList[0], graph.VertexList[2], 2);
        graph.AddEdge(graph.VertexList[1], graph.VertexList[3], 4);
        graph.AddEdge(graph.VertexList[1], graph.VertexList[4], 9);
        graph.AddEdge(graph.VertexList[2], graph.VertexList[3], 3);
        graph.AddEdge(graph.VertexList[2], graph.VertexList[5], 5);
        graph.AddEdge(graph.VertexList[3], graph.VertexList[7], 3);
        graph.AddEdge(graph.VertexList[4], graph.VertexList[9], 7);
        graph.AddEdge(graph.VertexList[5], graph.VertexList[6], 1);
        graph.AddEdge(graph.VertexList[5], graph.VertexList[7], 2);
        graph.AddEdge(graph.VertexList[6], graph.VertexList[8], 9);
        graph.AddEdge(graph.VertexList[6], graph.VertexList[10], 15);
        graph.AddEdge(graph.VertexList[7], graph.VertexList[8], 6);
        graph.AddEdge(graph.VertexList[8], graph.VertexList[10], 11);
        graph.AddEdge(graph.VertexList[9], graph.VertexList[8], 1);
        graph.AddEdge(graph.VertexList[9], graph.VertexList[11], 8);
        graph.AddEdge(graph.VertexList[10], graph.VertexList[11], 4);
    }
}
