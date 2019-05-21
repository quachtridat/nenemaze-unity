using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public int MazeRows;
    public int MazeColumns;

    [SerializeField]
    private GameObject _horizontalWallPrefab;
    [SerializeField]
    private GameObject _verticalWallPrefab;

    [SerializeField]
    private GameObject _wallContainer;

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

        for (int row = 0; row < MazeRows; ++row)
            for (int col = 0; col < MazeColumns; ++col) {
                Vector3 cellPos = new Vector3(col - halfWidth + 0.5f, 0, row - halfHeight + 0.5f);
                var cell = _mg.GetCell(row, col);

                if (cell.WallState.HasFlag(MazeGenerator.MazeCell.WallStates.Top)) {
                    GameObject g = Instantiate(_horizontalWallPrefab, this.transform);
                    g.name = $"Horizontal Wall (Top) ({row}, {col})";
                    g.transform.position = cellPos + new Vector3(0, g.transform.localScale.y / 2, +0.5f);
                }
                if (cell.WallState.HasFlag(MazeGenerator.MazeCell.WallStates.Bottom)) {
                    GameObject g = Instantiate(_horizontalWallPrefab, this.transform);
                    g.name = $"Horizontal Wall (Bottom) ({row}, {col})";
                    g.transform.position = cellPos + new Vector3(0, g.transform.localScale.y / 2, -0.5f);

                }

                if (cell.WallState.HasFlag(MazeGenerator.MazeCell.WallStates.Left)) {
                    GameObject g = Instantiate(_verticalWallPrefab, this.transform);
                    g.name = $"Vertical Wall (Left) ({row}, {col})";
                    g.transform.position = cellPos + new Vector3(-0.5f, g.transform.localScale.y / 2, 0);
                }
                if (cell.WallState.HasFlag(MazeGenerator.MazeCell.WallStates.Right)) {
                    GameObject g = Instantiate(_verticalWallPrefab, this.transform);
                    g.name = $"Vertical Wall (Left) ({row}, {col})";
                    g.transform.position = cellPos + new Vector3(+0.5f, g.transform.localScale.y / 2, 0);
                }
            }
    }

    // Update is called once per frame
    void Update() {

    }
}