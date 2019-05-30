using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBlock : MonoBehaviour {
    public static bool BallCollided;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other) {
        if (other is null) return;

        BallCollided = other.gameObject.tag == @"Ball";
    }

    private void OnTriggerExit(Collider other) {
        if (other is null) return;

        BallCollided = !(other.gameObject.tag == @"Ball");
    }
}