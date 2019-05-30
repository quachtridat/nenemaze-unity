using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Maze Area")]

    [Range(1, 1000)]
    public int MazeRows;
    [Range(1, 1000)]
    public int MazeColumns;

    [Header("Maze Walls")]

    [SerializeField]
    GameObject _horizontalWallPrefab;
    [SerializeField]
    GameObject _verticalWallPrefab;
    [SerializeField]
    GameObject _wallParent;

    [Header("Maze Balls")]
    [SerializeField]
    GameObject _ballPrefab;
    [SerializeField]
    GameObject _ballParent;
    public int InitialBallHeight;

    [Header("Maze Objects")]
    [SerializeField]
    GameObject _objectsParent;
    [SerializeField]
    GameObject _winningSpotPrefab;

    [Header("Misc.")]
    MazeGenerator _mg;
    Vector3 _winPos;
    [SerializeField]
    bool _debug;

    void Awake() {
        _mg = new MazeGenerator();
    }

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Confined;

        _mg.Height = MazeRows;
        _mg.Width = MazeColumns;
        _mg.InvertY = true;

        _mg.Generate();

        var halfHeight = MazeRows / 2f;
        var halfWidth = MazeColumns / 2f;

        for (int row = 0; row < MazeRows; ++row) {
            for (int col = 0; col < MazeColumns; ++col) {
                Vector3 cellPos = new Vector3(col - halfWidth + 0.5f, 0, row - halfHeight + 0.5f);
                var cell = _mg.GetCell(row, col);

                if (cell.WallState.HasFlag(MazeGenerator.MazeCell.WallStates.Top)) {
                    GameObject g = Instantiate(_horizontalWallPrefab, _wallParent is null ? this.transform : _wallParent.transform);
                    g.name = $"Wall (N) ({row}, {col})";
                    g.transform.position = cellPos + new Vector3(0, g.transform.localScale.y / 2, +0.5f);
                }
                if (cell.WallState.HasFlag(MazeGenerator.MazeCell.WallStates.Bottom)) {
                    GameObject g = Instantiate(_horizontalWallPrefab, _wallParent is null ? this.transform : _wallParent.transform);
                    g.name = $"Wall (S) ({row}, {col})";
                    g.transform.position = cellPos + new Vector3(0, g.transform.localScale.y / 2, -0.5f);
                }

                if (cell.WallState.HasFlag(MazeGenerator.MazeCell.WallStates.Left)) {
                    GameObject g = Instantiate(_verticalWallPrefab, _wallParent is null ? this.transform : _wallParent.transform);
                    g.name = $"Wall (W) ({row}, {col})";
                    g.transform.position = cellPos + new Vector3(-0.5f, g.transform.localScale.y / 2, 0);
                }
                if (cell.WallState.HasFlag(MazeGenerator.MazeCell.WallStates.Right)) {
                    GameObject g = Instantiate(_verticalWallPrefab, _wallParent is null ? this.transform : _wallParent.transform);
                    g.name = $"Wall (E) ({row}, {col})";
                    g.transform.position = cellPos + new Vector3(+0.5f, g.transform.localScale.y / 2, 0);
                }
            }
        }

        {
            var ballPos = GetRandomNonBlockedCell();
            var ballLocalPos = CellToLocal(ballPos);

            SpawnBall(CellToLocal(ballPos) + new Vector3(0, InitialBallHeight, 0));

            PairRowCol winPos = PairRowCol.Create(0, 0);
            var dist = (CellToLocal(winPos) - ballLocalPos).sqrMagnitude;
            for (int row = 0; row < MazeRows; ++row)
                for (int col = 0; col < MazeColumns; ++col) {
                    PairRowCol pos = PairRowCol.Create(row, col);
                    if (_mg.GetCell(pos).WallState.HasFlag(MazeGenerator.MazeCell.WallStates.All)) continue;
                    var d = (CellToLocal(pos) - ballLocalPos).sqrMagnitude;
                    if (d > dist) {
                        dist = d;
                        winPos = pos;
                    }
                }

            if (_debug)
                SpawnWinningSpot(CellToLocal(PairRowCol.Create(ballPos.Row, ballPos.Col - 1)) + new Vector3(0, _winningSpotPrefab.transform.localScale.y / 2, 0));
            else
                SpawnWinningSpot(CellToLocal(winPos) + new Vector3(0, _winningSpotPrefab.transform.localScale.y / 2, 0));
        }
    }

    // Update is called once per frame
    void Update() {
        if (WinBlock.BallCollided) {
            Debug.Log("You Won!");
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    PairRowCol GetRandomNonBlockedCell(int nRandom = 1) {
        PairRowCol pos = PairRowCol.Create(0, 0);
        int iterCnt = 0;

        do {
            pos.Row = Random.Range(0, MazeRows);
            pos.Col = Random.Range(0, MazeColumns);
        } while (iterCnt++ < nRandom && _mg.GetCell(pos).WallState.HasFlag(MazeGenerator.MazeCell.WallStates.All));

        return pos;
    }

    Vector3 CellToLocal(PairRowCol p) {
        return new Vector3(p.Col - MazeColumns / 2f + 0.5f, 0, p.Row - MazeRows / 2f + 0.5f);
    }

    void SpawnBall(Vector3 pos, string name = null) {
        GameObject g = Instantiate(_ballPrefab, _ballParent is null ? this.transform : _ballParent.transform);
        g.name = name is null ? "Ball" : name;
        g.transform.position = pos;
    }

    void SpawnWinningSpot(Vector3 pos, string name = null) {
        GameObject g = Instantiate(_winningSpotPrefab, _objectsParent is null ? this.transform : _objectsParent.transform);
        g.name = name is null ? "Winning Spot" : name;
        g.transform.position = pos;
    }
}