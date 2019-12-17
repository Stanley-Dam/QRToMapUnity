using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelGrouper {

    private QRHandler main;
    private Dictionary<Vector2, Pixel> pixelMap;
    private List<PixelSquare> pixelSquares;
    private Filter filter;
    private Transform map;

    public PixelGrouper(QRHandler main, Dictionary<Vector2, Pixel> pixelMap, Filter filter, Transform map) {
        this.main = main;
        this.filter = filter;
        this.pixelMap = pixelMap;
        this.pixelSquares = new List<PixelSquare>();
        this.map = map;

        GenerateSquares();
    }

    private void GenerateSquares() {
        foreach(Pixel pixel in pixelMap.Values) {
            bool colided = false;

            foreach(PixelSquare pixelSquare in pixelSquares) {
                if(pixelSquare.colided(pixel.Position))
                    colided = true;
            }

            if(!colided) {
                GetSquareFromStart(pixel);
            }
        }
    }

    private void GetSquareFromStart(Pixel startPixel) {
        PixelSquare pixelSquare = new PixelSquare(new Dictionary<Vector2, Pixel>());
        startPixel.GetConnectedPixels(this, pixelSquare);
        pixelSquares.Add(pixelSquare);

        main.GetQRSquare(pixelSquare);
    }

    //Properties
    public Dictionary<Vector2, Pixel> PixelMap { get { return pixelMap; } }

}
