using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRCode {

    private List<QRSquare> squares;

    public QRCode(QRSquare square) {
        this.squares = new List<QRSquare>();
        this.squares.Add(square);

        ScanSurrounding(square);
    }

    private void ScanSurrounding(QRSquare square) {
    }

}
