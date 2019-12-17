using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRHandler : MonoBehaviour {

    /// <summary>
    /// This class acts like the QR to map hub.
    /// It connects all the QR related stuff.
    /// </summary>

    [SerializeField] private QRSkeleton qrSkeleton;
    [SerializeField] private Filter filter;
    [SerializeField] private Transform map;

    private PixelGrouper pixelGrouper;

    private void Awake() {
        qrSkeleton.Init();
    }

    public QRSquare GetQRSquare(PixelSquare pixelSquare) {
        QRSquare square = new QRSquare(map, pixelSquare.Corners);
        return square;
    }

    public PixelGrouper PixelGrouper { 
        set { this.pixelGrouper = value; }
        get { return pixelGrouper; } 
    }
}
