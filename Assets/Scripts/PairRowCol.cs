using System;

public struct PairRowCol : IEquatable<PairRowCol> {
    public int Row { get; set; }
    public int Col { get; set; }

    public static PairRowCol Create(int row, int col) {
        return new PairRowCol { Row = row, Col = col };
    }


    public bool Equals(PairRowCol p) {
        return Row == p.Row && Col == p.Col;
    }

    public static bool operator ==(PairRowCol p1, PairRowCol p2) {
        return p1.Equals(p2);
    }

    public static bool operator !=(PairRowCol p1, PairRowCol p2) {
        return !p1.Equals(p2);
    }

    public override bool Equals(object obj) {
        if (obj is null) return false;
        if (obj is PairRowCol) return Equals((PairRowCol)obj);
        return base.Equals(obj);
    }

    public override int GetHashCode() {
        return Tuple.Create(Row, Col).GetHashCode();
    }

    public override string ToString() {
        return String.Format("({0}, {1})", Row, Col);
    }
}