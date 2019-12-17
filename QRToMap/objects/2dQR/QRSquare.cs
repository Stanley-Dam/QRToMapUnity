using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRSquare {

    private Transform map;
    private Vector3 center;
    private List<Vector2> corners;

    public QRSquare(Transform map, List<Vector2> corners) {
        this.map = map;
        this.corners = corners;

        FilterCorners();
    }
    
    public List<Vector3> GetScreenCorners() {
        List<Vector3> worldPointCorners = new List<Vector3>();

        foreach(Vector2 corner in corners) {
            Vector3 worldPointCorner = new Vector3(0, 0);

            Bounds bounds = map.gameObject.GetComponent<Renderer>().bounds;
            float width = map.gameObject.GetComponent<Renderer>().material.mainTexture.width;
            float height = map.gameObject.GetComponent<Renderer>().material.mainTexture.height;

            worldPointCorner.x = bounds.min.x + (bounds.size.x / width) * corner.x;
            worldPointCorner.z = bounds.min.z + (bounds.size.z / height) * corner.y;

            worldPointCorners.Add(-worldPointCorner);
        }

        return worldPointCorners;
    }

    public Vector3 GetScreenSquareCenter() {
        Vector3 worldPointCenter = new Vector3(0, 0);

        Bounds bounds = map.gameObject.GetComponent<Renderer>().bounds;
        float width = map.gameObject.GetComponent<Renderer>().material.mainTexture.width;
        float height = map.gameObject.GetComponent<Renderer>().material.mainTexture.height;

        worldPointCenter.x = bounds.min.x + (bounds.size.x / width) * center.x;
        worldPointCenter.z = bounds.min.z + (bounds.size.z / height) * center.z;

        return -worldPointCenter;
    }

    /*
     * Since we only need 4 corners we'll need to filter 
     * them from a possibly bigger list.
     * We do this by checking for the biggest and smallest x and y coords
     */
    private void FilterCorners() {
        /* tL = top left
         * bL = bottom left
         * 
         * Doing this instead of an array to make it more readable :)
         */

        List<Vector2> newCorners = new List<Vector2>();

        Vector2 tL = corners[0];
        Vector2 tR = corners[0];
        Vector2 bL = corners[0];
        Vector2 bR = corners[0];

        foreach(Vector2 corner in corners) {
            if(corner.x <= tL.x && corner.y <= tL.y)
                tL = corner;

            if(corner.x >= tR.x && corner.y <= tR.y)
                tR = corner;

            if(corner.x <= bL.x && corner.y >= bL.y)
                bL = corner;

            if(corner.x >= bR.x && corner.y >= bR.y)
                bR = corner;
        }

        newCorners.Add(tL);
        newCorners.Add(tR);
        newCorners.Add(bL);
        newCorners.Add(bR);

        //First get average width and height and divide them by 2 to get the center :)
        float centerX = (((tR.x - tL.x) + (bR.x - bL.x)) / 2) / 2;
        float centerY = (((bL.y - tL.y) + (bR.y - tR.y)) / 2) / 2;

        this.center = new Vector3(tL.x + centerX, 0, tL.y + centerY);

        this.corners = newCorners;
        Debug();
    }

    private void Debug() {
        string debug = "QRSquare: \n";

        foreach(Vector3 corner in GetScreenCorners()) {
            debug += corner.x + " " + corner.y + "\n";
            MonoBehaviour.Instantiate(Resources.Load("test"), corner, map.rotation, null);
        }

        MonoBehaviour.Instantiate(Resources.Load("test"), GetScreenSquareCenter(), map.rotation, null);
        debug += "center: " + GetScreenSquareCenter().x + " " + GetScreenSquareCenter().y + "\n";

        MonoBehaviour.print(debug + "\n " + corners.Count);
    }

}
