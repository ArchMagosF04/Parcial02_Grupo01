using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

public class DynamicSet<T> : TDAGroup<T>
{
    private List<T> elements = new List<T>();

    public DynamicSet()
    {

    }

    public override bool Add(T item) //Adds element to group.
    {
        if (!Contains(item))
        {
            elements.Add(item);
            return true;
        }
        return false;
    }

    public override bool Remove(T item) //Removes element from group.
    {
        if (Contains(item))
        {
            elements.Remove(item);
            return true;
        }
        return false;
    }

    public override bool Contains(T item) //Checks if the element is in the group.
    {
        return elements.Contains(item);
    }

    public override string Show() //Fills a string with all the elements in the group.
    {
        string text = "Dynamic Set Contents: \n";

        foreach (var element in elements)
        {
            text += "Element: " + element + "\n";
        }

        return text;
    }

    public override int Cardinality() //Returns the amount of elements in the group.
    {
        return elements.Count;
    }

    public override bool IsEmpty() //Checks if the group is empty.
    {
        if (elements.Count == 0)
        {
            return true;
        }
        return false;
    }

    public override TDAGroup<T> Union(TDAGroup<T> otherSet)
    {
        DynamicSet<T> unionSet = new DynamicSet<T>();

        foreach (var item in elements)
        {
            unionSet.Add(item);
        }
        foreach (var item in otherSet.GetGroup())
        {
            unionSet.Add(item);
        }
        return unionSet;
    }

    public override TDAGroup<T> Intersection(TDAGroup<T> otherSet)
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

    public override TDAGroup<T> Difference(TDAGroup<T> otherSet)
    {
        DynamicSet<T> differenceSet = new DynamicSet<T>();
        foreach (var item in elements)
        {
            if (!otherSet.Contains(item))
            {
                differenceSet.Add(item);
            }
        }
        foreach (var item in otherSet.GetGroup())
        {
            if (!Contains(item))
            {
                differenceSet.Add(item);
            }
        }
        return differenceSet;
    }

    public override T[] GetGroup()
    {
        return elements.ToArray();
    }
}
