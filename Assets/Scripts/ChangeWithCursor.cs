using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWithCursor : MonoBehaviour {
    [SerializeField]
    private Vector3 _minPos;
    [SerializeField]
    private Vector3 _maxPos;

    [SerializeField]
    [Tooltip("Rotation in Euler angles.")]
    private Vector3 _minRotation;
    [SerializeField]
    [Tooltip("Rotation in Euler angles.")]
    private Vector3 _maxRotation;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        var pos = Input.mousePosition;
        float v = pos.y / Screen.height;
        transform.position = Vector3.Lerp(_maxPos, _minPos, v);
        transform.rotation = Quaternion.Euler(Vector3.Lerp(_maxRotation, _minRotation, v));
    }
}