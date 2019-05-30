using System;
using UnityEngine;

public class Pair {
    protected Pair() { }
    public static Pair Create<T1, T2>(T1 item1, T2 item2) {
        return Pair<T1, T2>.Create(item1, item2);
    }
}

[Serializable]
public class Pair<T1, T2> : Pair, IEquatable<Pair<T1, T2>> {
    [SerializeField]
    private T1 _first;
    [SerializeField]
    private T2 _second;

    public T1 First { get => _first; set => _first = value; }
    public T2 Second { get => _second; set => _second = value; }

    public Pair() { }

    public Pair(Pair<T1, T2> p) {
        First = p.First;
        Second = p.Second;
    }

    public Pair(T1 item1, T2 item2) {
        First = item1;
        Second = item2;
    }

    public static Pair<T1, T2> Create(T1 item1, T2 item2) {
        return new Pair<T1, T2> { First = item1, Second = item2 };
    }

    public bool Equals(Pair<T1, T2> other) {
        if (other is null) return false;
        bool null11 = this.First == null;
        bool null12 = this.Second == null;
        bool null21 = other.First == null;
        bool null22 = other.Second == null;
        bool null1 = null11 && null12;
        bool null2 = null21 && null22;
        if (null1 && null2) return true;
        if (!null1 && !null2) return (this.First.Equals(other.First)) && (this.Second.Equals(other.Second));
        if (!null1) return null11 ? other.First.Equals(this.First) : this.First.Equals(other.First);
        if (!null2) return null12 ? other.Second.Equals(this.Second) : this.Second.Equals(other.Second);
        return false;
    }

    public static bool operator ==(Pair<T1, T2> p1, Pair<T1, T2> p2) {
        return p1.Equals(p2);
    }

    public static bool operator !=(Pair<T1, T2> p1, Pair<T1, T2> p2) {
        return !p1.Equals(p2);
    }

    public override bool Equals(object obj) {
        if (obj is null) return false;
        if (obj is Pair<T1, T2>) return Equals(obj as Pair<T1, T2>);
        return base.Equals(obj);
    }

    public override int GetHashCode() {
        return Tuple.Create(First, Second).GetHashCode();
    }

    public override string ToString() {
        return $"({First}, {Second})";
    }
}