using UnityEngine;

[System.Serializable]
public class PairMinMax<T> : Pair<T, T> {
    public T Min { get => First; set => First = value; }
    public T Max { get => Second; set => Second = value; }

    public PairMinMax() : base() { }

    public PairMinMax(PairMinMax<T> p) : base(p as Pair<T, T>) { }

    public PairMinMax(T min, T max) : base(min, max) { }

    public new static PairMinMax<T> Create(T min, T max) {
        return new PairMinMax<T>(min, max);
    }
}