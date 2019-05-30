public class PairRowCol : Pair<int, int> {
    public int Row { get => First; set => First = value; }
    public int Col { get => Second; set => Second = value; }

    public PairRowCol() : base() { }

    public PairRowCol(PairRowCol p) : base(p as Pair<int, int>) { }

    public PairRowCol(int row, int col) : base(row, col) { }

    public new static PairRowCol Create(int row, int col) {
        return new PairRowCol(row, col);
    }
}