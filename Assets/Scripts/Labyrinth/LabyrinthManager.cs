using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class LabyrinthManager : MonoBehaviour
{
    private TDAGraphGeneric<MazeNode> labyrinth = new TDAGraphGeneric<MazeNode>();

    private Stack<MazeNode> nodesToTravel = new Stack<MazeNode>();
    private List<MazeNode> nodesExplored = new List<MazeNode>();

    [SerializeField] private GameObject mazeNodePrefab;
    private GameObject[,] nodeArray;

    private int colums = 6;
    private int rows = 5;

    private MazeNode endNode;
    private MazeNode startNode;

    [SerializeField] private Transform pointer;

    [SerializeField] private Vector2 startCoordinates = new Vector2(4, 0);
    [SerializeField] private Vector2 endCoordinates = new Vector2(4, 5);

    private void Awake()
    {
        nodeArray = new GameObject[rows, colums];

        CreateMazeNodes(); 
        AddMazeNodesToGraph();
        AddEdgesToGraph();

        endNode = FindVertexWithValue((int)endCoordinates.x, (int)endCoordinates.y);
        nodeArray[(int)endCoordinates.x, (int)endCoordinates.y].GetComponent<SpriteRenderer>().color = Color.red;
        startNode = FindVertexWithValue((int)startCoordinates.x, (int)startCoordinates.y);
        nodeArray[(int)startCoordinates.x, (int)startCoordinates.y].GetComponent<SpriteRenderer>().color = Color.green;

        pointer.position = nodeArray[(int)startNode.Row, (int)startNode.Column].transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindExit();
        }
    }

    private void FindExit()
    {
        if (startNode == null || endNode == null)
        {
            Debug.LogError("Start or/and End not assigned.");
            return;
        }

        MazeNode currentNode = startNode;
        pointer.position = nodeArray[(int)startNode.Row, (int)startNode.Column].transform.position;

        CatalogNode(currentNode);
        StartCoroutine(Pathing(currentNode));
    }

    private IEnumerator Pathing(MazeNode currentNode)
    {
        while (currentNode != endNode && nodesToTravel.Count > 0)
        {
            currentNode = nodesToTravel.Pop();
            pointer.position = nodeArray[(int)currentNode.Row, (int)currentNode.Column].transform.position;

            MazeNode BottomNode = FindVertexWithValue(currentNode.Row + 1, currentNode.Column);
            MazeNode RightNode = FindVertexWithValue(currentNode.Row, currentNode.Column + 1);
            MazeNode TopNode = FindVertexWithValue(currentNode.Row - 1, currentNode.Column);
            MazeNode LeftNode = FindVertexWithValue(currentNode.Row, currentNode.Column - 1);

            if (BottomNode != null)
            {
                if (labyrinth.DoesEdgeExist(currentNode, BottomNode) && !nodesExplored.Contains(BottomNode))
                {
                    CatalogNode(BottomNode);
                }
            }
            if (RightNode != null)
            {
                if (labyrinth.DoesEdgeExist(currentNode, RightNode) && !nodesExplored.Contains(RightNode))
                {
                    CatalogNode(RightNode);
                }
            }
            if (TopNode != null)
            {
                if (labyrinth.DoesEdgeExist(currentNode, TopNode) && !nodesExplored.Contains(TopNode))
                {
                    CatalogNode(TopNode);
                }
            }
            if (LeftNode != null)
            {
                if (labyrinth.DoesEdgeExist(currentNode, LeftNode) && !nodesExplored.Contains(LeftNode))
                {
                    CatalogNode(LeftNode);
                }
            }
            
            Debug.Log($"({currentNode.Row}, {currentNode.Column})");

            yield return new WaitForSeconds(0.7f);
        }

        if (currentNode == endNode)
        {
            Debug.Log("Exit Reached");
        }
        else
        {
            Debug.Log("Couldn't find Exit");
        }

    }

    private void CatalogNode(MazeNode node)
    {
        nodesToTravel.Push(node);
        nodesExplored.Add(node);
    }

    private void CreateMazeNodes()
    {
        for (int i = 0; i < colums; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject node = Instantiate(mazeNodePrefab, transform);
                node.transform.position += new Vector3(i, -j, 0);
                nodeArray[j, i] = node;
            }
        }
    }

    private void AddMazeNodesToGraph()
    {
        for (int i = 0;i < colums; i++)
        {
            for (int  j = 0; j < rows; j++)
            {
                MazeNode newNode = new MazeNode(j, i);
                labyrinth.AddVertex(newNode);
            }
        }
    }

    private void AddEdgesToGraph()
    {
        AddBidirectionalEdge(0, 0, 1, 0);
        AddBidirectionalEdge(0, 0, 0, 1);

        AddBidirectionalEdge(0, 1, 1, 1);

        AddBidirectionalEdge(1, 1, 1, 2);

        AddBidirectionalEdge(1, 2, 0, 2);
        AddBidirectionalEdge(1, 2, 1, 3);

        AddBidirectionalEdge(0, 2, 0, 3);

        AddBidirectionalEdge(0, 3, 0, 4);

        AddBidirectionalEdge(1, 3, 1, 4);
        AddBidirectionalEdge(1, 3, 2, 3);

        AddBidirectionalEdge(2, 3, 2, 2);

        AddBidirectionalEdge(2, 2, 2, 1);
        AddBidirectionalEdge(2, 2, 3, 2);

        AddBidirectionalEdge(2, 1, 3, 1);

        AddBidirectionalEdge(3, 1, 3, 0);

        AddBidirectionalEdge(3, 0, 2, 0);

        AddBidirectionalEdge(3, 2, 3, 3);
        AddBidirectionalEdge(3, 2, 4, 2);

        AddBidirectionalEdge(4, 2, 4, 1);
        AddBidirectionalEdge(4, 2, 4, 3);

        AddBidirectionalEdge(4, 1, 4, 0);

        AddBidirectionalEdge(4, 3, 4, 4);

        AddBidirectionalEdge(4, 4, 3, 4);

        AddBidirectionalEdge(3, 4, 2, 4);

        AddBidirectionalEdge(2, 4, 2, 5);

        AddBidirectionalEdge(2, 5, 1, 5);
        AddBidirectionalEdge(2, 5, 3, 5);

        AddBidirectionalEdge(1, 5, 0, 5);

        AddBidirectionalEdge(3, 5, 4, 5);
    }

    private void AddBidirectionalEdge(int node1Row, int node1Column, int node2Row, int node2Column)
    {
        labyrinth.AddEdge(FindVertexWithValue(node1Row, node1Column), FindVertexWithValue(node2Row, node2Column), 0);
        labyrinth.AddEdge(FindVertexWithValue(node2Row, node2Column), FindVertexWithValue(node1Row, node1Column), 0);
    }

    public MazeNode FindVertexWithValue(int row, int column)
    {
        foreach (var vertex in labyrinth.VertexList)
        {
            if (row == vertex.Row && column == vertex.Column)
            {
                return vertex;
            }
        }

        return null;
    }
}
