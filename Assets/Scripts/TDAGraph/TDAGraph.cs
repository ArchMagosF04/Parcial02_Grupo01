using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TDAGraph<T>
{
    public abstract bool Add(T item);

    public abstract bool Remove(T item);

    public abstract bool Contains(T item);

    public abstract void Show();

    public abstract int Cardinality();

    public abstract bool IsEmpty();

    public abstract TDAGraph<T> Union(TDAGraph<T> otherSet);

    public abstract TDAGraph<T> Intersection(TDAGraph<T> otherSet);

    public abstract TDAGraph<T> Difference(TDAGraph<T> otherSet);

}
