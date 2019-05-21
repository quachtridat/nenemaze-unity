using UnityEngine;

public class RotateToCursor : MonoBehaviour {
    float h = 0;
    float v = 0;

    float halfWidth = 0;
    float halfHeight = 0;

    Vector3 mousePos;

    // Start is called before the first frame update
    void Start() {
        halfWidth = Screen.width / 2;
        halfHeight = Screen.height / 2;
    }

    // Update is called once per frame
    void Update() {
        var pos = Input.mousePosition;
        h = (pos.x - halfWidth) / halfWidth * 45;
        v = (pos.y - halfHeight) / halfHeight * 45;
        transform.rotation = Quaternion.Euler(v, 0, -h);
    }
}