using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAGraphManager : MonoBehaviour
{
    private TDAGraph graph;

    [SerializeField] private GameObject nodeObjects;

    private void Awake()
    {
        
    }

    private void Start()
    {
        graph = new TDAGraph();
    }


}
