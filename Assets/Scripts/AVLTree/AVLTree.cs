using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AVLTree
{
    public Node root { get; private set; }

    private Node selectedParentNode;

    public void Insert(int value)
    {
        root = Insert(root, value, 0);
    }

    private Node Insert(Node node, int value, int depth)
    {
        if (node == null)
        {
            return new Node(value,depth);
        }

        if (value < node.Value)
        {
            depth++;
            node.Left = Insert(node.Left, value, depth);
        }
        else if (value > node.Value)
        {
            depth++;
            node.Right = Insert(node.Right, value, depth);
        }
        else if (value == node.Value)
        {
            return node;
        }

        node.Height = 1 + Mathf.Max(GetHeight(node.Left), GetHeight(node.Right));

        return Balance(node);
    }

    private Node Balance(Node node)
    {
        int balanceFactor = GetBalance(node);

        if (balanceFactor > 1 && GetBalance(node.Left) >= 0)
        {
            return RightRotate(node);
        }

        if (balanceFactor < -1 && GetBalance(node.Right) <= 0)
        {
            return LeftRotate(node);
        }

        if (balanceFactor > 1 && GetBalance(node.Left) < 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        if (balanceFactor < -1 && GetBalance(node.Right) > 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    private int GetBalance(Node node)
    {
        if (node == null) return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    private int GetHeight(Node node)
    {
        if (node == null) return 0;
        return node.Height;
    }

    private Node RightRotate(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Mathf.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
        x.Height = Mathf.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

        return x;
    }

    private Node LeftRotate(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Mathf.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
        y.Height = Mathf.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

        return y;
    }
}
