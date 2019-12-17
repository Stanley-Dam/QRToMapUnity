using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel {

    private Vector2 position;
    private Color color;
    private bool isCorner = true;

    public Pixel(Vector2 position, Color color) {
        this.position = position;
        this.color = color;
    }

    public void GetConnectedPixels(PixelGrouper grouper, PixelSquare pixelSquare) {
        List<Pixel> connectedPixels = new List<Pixel>();
        Pixel foundPixel;

        if(grouper.PixelMap.TryGetValue(position + new Vector2(-1, 0), out foundPixel))
            connectedPixels.Add(foundPixel);

        if(grouper.PixelMap.TryGetValue(position + new Vector2(1, 0), out foundPixel))
            connectedPixels.Add(foundPixel);

        if(grouper.PixelMap.TryGetValue(position + new Vector2(0, -1), out foundPixel))
            connectedPixels.Add(foundPixel);

        if(grouper.PixelMap.TryGetValue(position + new Vector2(0, 1), out foundPixel))
            connectedPixels.Add(foundPixel);

        pixelSquare.AddPixel(this);
        this.color = pixelSquare.Color;

        //Spread :P
        if(connectedPixels.Count > 0) {

            isCorner = connectedPixels.Count <= 2;

            foreach(Pixel pixel in connectedPixels) {

                if(!pixelSquare.colided(pixel.position))
                    pixel.GetConnectedPixels(grouper, pixelSquare);

            }
        }
    }

    public Vector2 Position { get { return this.position; } }
    public Color Color { get { return this.color; } }

    public bool IsCorner { get { return this.isCorner; } }
}
