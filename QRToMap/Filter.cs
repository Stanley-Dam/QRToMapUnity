using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour {

    [SerializeField] private QRHandler main;
    [SerializeField] private Color color;
    [SerializeField] private float lenience = 0.05f;

    #region Temporary Variables

    [SerializeField] private Sprite QRCode;
    [SerializeField] private Sprite read;
    [SerializeField] private GameObject export;

    #endregion

    // Start is called before the first frame update
    void Awake() {
        export.GetComponent<MeshRenderer>().material.mainTexture = GenerateMapFromImage(read);
    }

    void Start() {
        Render(main.PixelGrouper.PixelMap);
    }

    //This functions starts the map generating proccess and returns the filtered image
    public Texture2D GenerateMapFromImage(Sprite sprite) {
        //Get pixels per x-iteration and put them in a 2d array
        int width = sprite.texture.width;
        int height = sprite.texture.height;

        //The important stuff
        Dictionary<Vector2, Pixel> pixelMap = new Dictionary<Vector2, Pixel>();
        Color[,] colors = new Color[height, width];

        //Just some variables to return (mainly visual stuff) :)
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] newPixels = new Color[colors.Length];

        int index = 0;

        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                colors[y, x] = sprite.texture.GetPixel(x, y);
                newPixels[index] = new Color(0, 0, 0);

                if(ColorInRange(colors[y, x].r, color.r, lenience) &&
                    ColorInRange(colors[y, x].g, color.g, lenience) &&
                    ColorInRange(colors[y, x].b, color.b, lenience)) {
                        
                        //When the color is what we're looking for
                        newPixels[index] = new Color(color.r, color.g, color.b);

                        Vector2 position = new Vector2(x, y);
                        pixelMap.Add(position, new Pixel(position, colors[y, x]));
                }

                index++;
            }
        }

        texture.SetPixels(newPixels);
        texture.Apply();

        //Start the actual image to map conversion process here
        main.PixelGrouper = new PixelGrouper(main, pixelMap, this, export.transform);

        return texture;
    }

    public void Render(Dictionary<Vector2, Pixel> pixelMap) {
        Texture2D texture = new Texture2D((int)read.rect.width, (int)read.rect.height);

        int width = read.texture.width;
        int height = read.texture.height;

        Color[,] colors = new Color[height, width];
        Color[] newPixels = new Color[colors.Length];

        int index = 0;

        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                Pixel pixel;

                if(pixelMap.TryGetValue(new Vector2(x, y), out pixel)) {
                    newPixels[index] = pixel.Color;
                }

                index++;
            }
        }

        texture.SetPixels(newPixels);
        texture.Apply();

        export.GetComponent<MeshRenderer>().material.mainTexture = texture;
    }

    private bool ColorInRange(float color, float index, float lenience) {
        return color == index ||
            (color >= index - lenience && color <= index + lenience);
    }
}
