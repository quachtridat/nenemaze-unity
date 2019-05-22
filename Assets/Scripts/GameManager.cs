using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Maze Area")]

    [Range(1, 1000)]
    public int MazeRows;
    [Range(1, 1000)]
    public int MazeColumns;

    [Header("Maze Walls")]

    [SerializeField]
    private GameObject _horizontalWallPrefab;
    [SerializeField]
    private GameObject _verticalWallPrefab;
    [SerializeField]
    private GameObject _wallParent;

    [Header("Maze Balls")]
    [SerializeField]
    private GameObject _ballPrefab;
    [SerializeField]
    private GameObject _ballParent;
    public int InitialBallHeight;

    [Header("Misc.")]

    MazeGenerator _mg;

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
            PairRowCol ballPos;
            int iterCnt = 0;
            int iterMax = MazeRows * MazeColumns;

            do {
                ballPos = PairRowCol.Create(Random.Range(0, MazeRows), Random.Range(0, MazeColumns));
            } while (iterCnt++ < iterMax && _mg.GetCell(ballPos).WallState.HasFlag(MazeGenerator.MazeCell.WallStates.All));

            SpawnBall(new Vector3(ballPos.Col - halfWidth + 0.5f, InitialBallHeight, ballPos.Row - halfHeight + 0.5f));
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void SpawnBall(Vector3 pos) {
        GameObject g = Instantiate(_ballPrefab, _ballParent is null ? this.transform : _ballParent.transform);
        g.name = $"Ball";
        g.transform.position = pos;
    }
}