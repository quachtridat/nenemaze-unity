using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A DFS-based maze generator.
/// </summary>
public class MazeGenerator {
    public int Width { get; set; }
    public int Height { get; set; }

    public bool InvertX { get; set; }
    public bool InvertY { get; set; }

    public MazeGenerator() : base() { }

    public MazeGenerator(int width, int height) : this() {
        Width = width;
        Height = height;
    }

    private MazeCell[,] _map;

    private void InitializeMap() {
        _map = new MazeCell[Height, Width];
        for (int row = 0; row < Height; ++row)
            for (int col = 0; col < Width; ++col)
                _map[row, col] = new MazeCell();
    }

    private void ClearMap() {
        for (int row = 0; row < Height; ++row)
            for (int col = 0; col < Width; ++col) {
                _map[row, col].Visited = false;
                _map[row, col].WallState = MazeCell.WallStates.All;
            }
    }

    public MazeCell GetCell(int row, int col) => _map[row, col];

    public MazeCell GetCell(PairRowCol p) => GetCell(p.Row, p.Col);

    private void MarkVisited(int row, int col) {
        MarkVisited(_map[row, col]);
    }

    private void MarkVisited(PairRowCol p) {
        MarkVisited(p.Row, p.Col);
    }

    private void MarkVisited(MazeCell cell) {
        cell.Visited = true;
    }

    private bool ValidPos(int row, int col) {
        return 0 <= row && row < Width && 0 <= col && col < Height;
    }

    private bool ValidPos(PairRowCol p) {
        return ValidPos(p.Row, p.Col);
    }

    private IEnumerable<PairRowCol> GetNeighbors(PairRowCol p) {
        var l = new PairRowCol { Row = p.Row, Col = p.Col - 1 }; // left
        var r = new PairRowCol { Row = p.Row, Col = p.Col + 1 }; // right
        var a = new PairRowCol { Row = p.Row - 1, Col = p.Col }; // above
        var b = new PairRowCol { Row = p.Row + 1, Col = p.Col }; // below
        return new[] { l, r, a, b }.Where(ValidPos);
    }

    private IEnumerable<PairRowCol> GetUnvisitedNeighbors(PairRowCol p) {
        return GetNeighbors(p).Where(_p => !_map[_p.Row, _p.Col].Visited);
    }

    private void Connect(PairRowCol p1, PairRowCol p2) {
        if (p1.Row == p2.Row)
            if (p1.Col < p2.Col) {
                _map[p1.Row, p1.Col].WallState ^= InvertX ? MazeCell.WallStates.Left : MazeCell.WallStates.Right;
                _map[p2.Row, p2.Col].WallState ^= InvertX ? MazeCell.WallStates.Right : MazeCell.WallStates.Left;
            } else if (p1.Col > p2.Col) {
                _map[p1.Row, p1.Col].WallState ^= InvertX ? MazeCell.WallStates.Right : MazeCell.WallStates.Left;
                _map[p2.Row, p2.Col].WallState ^= InvertX ? MazeCell.WallStates.Left : MazeCell.WallStates.Right;
            }
        if (p1.Col == p2.Col)
            if (p1.Row < p2.Row) {
                _map[p1.Row, p1.Col].WallState ^= InvertY ? MazeCell.WallStates.Top : MazeCell.WallStates.Bottom;
                _map[p2.Row, p2.Col].WallState ^= InvertY ? MazeCell.WallStates.Bottom : MazeCell.WallStates.Top;
            } else if (p1.Row > p2.Row) {
                _map[p1.Row, p1.Col].WallState ^= InvertY ? MazeCell.WallStates.Bottom : MazeCell.WallStates.Top;
                _map[p2.Row, p2.Col].WallState ^= InvertY ? MazeCell.WallStates.Top : MazeCell.WallStates.Bottom;
            }
    }

    public void Generate() {
        InitializeMap();
        ClearMap();

        Stack<PairRowCol> st = new Stack<PairRowCol>();
        System.Random rng = new System.Random();

        var currentPos = new PairRowCol { Row = rng.Next(Height), Col = rng.Next(Width) };
        st.Push(currentPos);
        MarkVisited(currentPos);

        while (st.Count > 0) {
            var uns = GetUnvisitedNeighbors(currentPos).ToArray();
            if (uns.Length > 0) {
                var nextPos = uns[rng.Next(uns.Length)];
                Connect(currentPos, nextPos);
                st.Push(nextPos);
                MarkVisited(nextPos);
                currentPos = nextPos;
            } else {
                currentPos = st.Pop();
            }
        }

        CleanWalls();
    }

    private void CleanWalls() {
        for (int row = 1; row < Height; ++row)
            for (int col = 1; col < Width; ++col) {
                var current = GetCell(PairRowCol.Create(row, col));
                var left = GetCell(PairRowCol.Create(row, col - 1));
                var above = GetCell(PairRowCol.Create(row - 1, col));
                if (left.WallState.HasFlag(MazeCell.WallStates.Right) && current.WallState.HasFlag(MazeCell.WallStates.Left))
                    left.WallState ^= MazeCell.WallStates.Right;
                if (above.WallState.HasFlag(MazeCell.WallStates.Bottom) && current.WallState.HasFlag(MazeCell.WallStates.Top))
                    above.WallState ^= MazeCell.WallStates.Bottom;
            }
    }

    public class MazeCell {
        public bool Visited { get; set; }

        public WallStates WallState { get; set; }

        [Flags]
        public enum WallStates {
            None = 0,
            Top = 1 << 0,
            Bottom = 1 << 1,
            Left = 1 << 2,
            Right = 1 << 3,
            Horizontal = Top | Bottom,
            Vertical = Left | Right,
            All = Top | Bottom | Left | Right
        }
    }
}