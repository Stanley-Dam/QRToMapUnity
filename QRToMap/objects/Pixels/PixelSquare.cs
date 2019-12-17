using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelSquare {

    private Dictionary<Vector2, Pixel> pixels;
    private Color debugColor;

    public PixelSquare(Dictionary<Vector2, Pixel> pixels) {
        this.pixels = pixels;
        this.debugColor = new Color(Random.value, Random.value, Random.value);
    }

    public bool colided(Vector2 position) {
        if(pixels.ContainsKey(position))
            return true;

        return false;
    }

    //Adder
    public void AddPixel(Pixel pixel) {
        this.pixels.Add(pixel.Position, pixel);
    }

    public Color Color { get { return this.debugColor; } }

    public List<Vector2> Corners { 
        get {
            List<Vector2> cornerPixels = new List<Vector2>();

            foreach(Pixel pixel in pixels.Values) {
                if(pixel.IsCorner)
                    cornerPixels.Add(pixel.Position);
            }

            return cornerPixels;
        } 
    }

}
