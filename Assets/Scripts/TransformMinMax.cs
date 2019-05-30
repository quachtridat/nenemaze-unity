using UnityEngine;

[System.Serializable]
public class TransformMinMax {
    [System.Serializable]
    public class PairMinMaxVector3 : PairMinMax<Vector3> { } // because Unity cannot serialize generic fields
    public PairMinMaxVector3 Position;
    public PairMinMaxVector3 Rotation;
    public PairMinMaxVector3 Scale;
}