using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRSkeletonSquare : MonoBehaviour {

    [SerializeField] private Transform center;

    [SerializeField] private Transform tL;
    [SerializeField] private Transform tR;
    [SerializeField] private Transform bL;
    [SerializeField] private Transform bR;

    private float xDistance = 1;
    private float yDistance = 1;

    public void Init() {
        xDistance = (((tR.position.x - tL.position.x) + (bR.position.x - bL.position.x)) / 2);
        yDistance = (((bL.position.y - tL.position.y) + (bR.position.y - tR.position.y)) / 2);
    }

    public float XDistance { get { return this.xDistance; } }

    public float YDistance { get { return this.yDistance; } }

    public Vector3 Center { get { return this.center.position; } }
}
