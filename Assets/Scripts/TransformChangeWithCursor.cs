using UnityEngine;

/// <summary>
/// Change the game-object's transform in accordance with the mouse position on the screen.
/// </summary>
public class TransformChangeWithCursor : MonoBehaviour {
    [SerializeField]
    [Tooltip("Horizontal configuration of the cursor. As the cursor moves from left to right, parameters are linearly interpolated up.")]
    TransformMinMax _mouseX;
    [SerializeField]
    [Tooltip("Vertical configuration of the cursor. As the cursor moves from bottom to top, parameters are linearly interpolated up.")]
    TransformMinMax _mouseY;

    void Update() {
        var pos = Input.mousePosition;
        var pX = pos.x / Screen.width;
        var pY = pos.y / Screen.height;
        this.transform.position = (Vector3.Lerp(_mouseX.Position.First, _mouseX.Position.Second, pX) + Vector3.Lerp(_mouseY.Position.First, _mouseY.Position.Second, pY)) / 2;
        this.transform.rotation = Quaternion.Euler((Vector3.Lerp(_mouseX.Rotation.First, _mouseX.Rotation.Second, pX) + Vector3.Lerp(_mouseY.Rotation.First, _mouseY.Rotation.Second, pY)) / 2);
        this.transform.localScale = (Vector3.Lerp(_mouseX.Scale.First, _mouseX.Scale.Second, pX) + Vector3.Lerp(_mouseY.Scale.First, _mouseY.Scale.Second, pY)) / 2;
    }
}