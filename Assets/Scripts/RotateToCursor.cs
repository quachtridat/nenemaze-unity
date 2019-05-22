using UnityEngine;

public class RotateToCursor : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        var pos = Input.mousePosition;
        var halfWidth = Screen.width / 2f;
        var halfHeight = Screen.height / 2f;
        var h = (pos.x - halfWidth) / halfWidth * 10;
        var v = (pos.y - halfHeight) / halfHeight * 10;
        transform.rotation = Quaternion.Euler(v, 0, -h);
    }
}