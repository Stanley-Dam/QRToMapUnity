using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRSkeleton : MonoBehaviour {

    [SerializeField] private QRSkeletonSquare topRight;
    [SerializeField] private QRSkeletonSquare bottomRight;
    [SerializeField] private QRSkeletonSquare bottomLeft;

    private float avarageSize;
    private float squareDistance;

    public void Init() {

        topRight.Init();
        bottomRight.Init();
        bottomLeft.Init();

        //Get the avarage square distance
        squareDistance = (Vector3.Distance(topRight.Center, bottomRight.Center) +
                          Vector3.Distance(bottomLeft.Center, bottomRight.Center)) / 2;

        float avarageSquareWidth = (topRight.XDistance + bottomRight.XDistance + bottomLeft.XDistance) / 3;
        float avarageSquareHeight = (topRight.YDistance + bottomRight.YDistance + bottomLeft.YDistance) / 3;
        avarageSize = (avarageSquareWidth + avarageSquareHeight) / 2;
    }

    public float GetSquareDistanceFromSize(float width, float height) {
        float size = (width + height / 2);

        return (avarageSize / size) * squareDistance;
    }

}
