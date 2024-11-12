using System;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class StaticSet<T> : TDAGroup<T>
{
    private T[] elements;

    public StaticSet(int arraySize) //Static sets needs to know how many elements it will store.
    {
        elements = new T[arraySize];
    }

    public override bool Add(T item) //Adds element to group.
    {
        if (Contains(item)) return false;

        for (int i = 0; i < elements.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(elements[i], default(T))) //Only adds it to the array position if its current value is the default/null.
            {
                elements[i] = item;
                return true;
            }
        }
        return false;
    }

    public override int Cardinality() //Returns the amount of elements in the group that aren't null/default.
    {
        int cardinality = 0;

        for (int i = 0; i < elements.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(elements[i], default(T)))
            {
                cardinality++;
            }
        }

        return cardinality;
    }

    public override bool Contains(T item) //Checks if the element is in the group.
    {
        for(int i = 0;i < elements.Length;i++)
        {
            if (elements[i].Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    public override bool IsEmpty() //Checks if the group is empty.
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(elements[i], default(T)))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Remove(T item) //Removes element from group by moving the last element from the array to its position, and making the last position the default/null value.
    {
        if (Contains(item))
        {
            int lastIndex = GetLastElementIndex();

            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].Equals(item))
                {
                    elements[i] = elements[lastIndex];
                    elements[lastIndex] = default;

                    return true;
                }
            }
        }

        return false;
    }

    private int GetLastElementIndex()
    {
        for (int i = elements.Length - 1; i >= 0; i--)
        {
            if (!EqualityComparer<T>.Default.Equals(elements[i], default(T)))
            {
                return i;
            }
        }

        return -1;
    }

    public override string Show() //Fills a string with all the elements in the group.
    {
        string text = "Static Set Contents: \n";

        foreach (var element in elements)
        {
            text += "Element: " + element + "\n";
        }

        return text;
    }

    public override TDAGroup<T> Union(TDAGroup<T> otherSet)
    {
        int newArraySize = 0;

        foreach (var item in elements)
        {
            newArraySize++;
        }
        foreach (var item in otherSet.GetGroup())
        {
            if (!Contains(item)) newArraySize++;
        }

        StaticSet<T> unionSet = new StaticSet<T>(newArraySize);

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

    public override TDAGroup<T> Difference(TDAGroup<T> otherSet)
    {
        int newArraySize = 0; 

        foreach (var item in elements)
        {
            if (!otherSet.Contains(item))
            {
                newArraySize++;
            }
        }

        foreach (var item in otherSet.GetGroup())
        {
            if (!Contains(item))
            {
                newArraySize++;
            }
        }

        StaticSet<T> differenceSet = new StaticSet<T>(newArraySize);

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

    public override TDAGroup<T> Intersection(TDAGroup<T> otherSet)
    {
        int newArraySize = 0;

        foreach (var item in elements)
        {
            if (otherSet.Contains(item))
            {
                newArraySize++;
            }
        }

        StaticSet<T> intersectionSet = new StaticSet<T>(newArraySize);

        foreach (var item in elements)
        {
            if (otherSet.Contains(item))
            {
                intersectionSet.Add(item);
            }
        }
        return intersectionSet;
    }

    public override T[] GetGroup()
    {
        return elements;
    }
}
