using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSet<T> : TDAGraph<T>
{
    private List<T> elements;

    public DynamicSet()
    {
        elements = new List<T>();
    }

    public override bool Add(T item)
    {
        if (!Contains(item))
        {
            elements.Add(item);
            return true;
        }
        return false;
    }

    public override bool Remove(T item)
    {
        return elements.Remove(item);
    }

    public override bool Contains(T item)
    {
        return elements.Contains(item);
    }

    public override void Show()
    {
        foreach (var elemento in elements)
        {
            Console.WriteLine(elemento);
        }
    }

    public override int Cardinality()
    {
        return elements.Count;
    }

    public override bool IsEmpty()
    {
        return elements.Count == 0;
    }

    public override TDAGraph<T> Union(TDAGraph<T> otherSet)
    {
        DynamicSet<T> unionSet = new DynamicSet<T>();
        foreach (var item in elements)
        {
            unionSet.Add(item);
        }
        //foreach (var item in otherSet)
        //{
        //    unionSet.Add(item);
        //}
        return unionSet;
    }

    public override TDAGraph<T> Intersection(TDAGraph<T> otherSet)
    {
        DynamicSet<T> intersectionSet = new DynamicSet<T>();
        foreach (var item in elements)
        {
            if (otherSet.Contains(item))
            {
                intersectionSet.Add(item);
            }
        }
        return intersectionSet;
    }

    public override TDAGraph<T> Difference(TDAGraph<T> otherSet)
    {
        DynamicSet<T> differenceSet = new DynamicSet<T>();
        foreach (var item in elements)
        {
            if (!otherSet.Contains(item))
            {
                differenceSet.Add(item);
            }
        }
        return differenceSet;
    }
}
