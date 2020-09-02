using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController instance = null;

    public Geometry2D geometry2D;
    public GameObject centerOfTheMap;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);

        geometry2D = GetComponent<Geometry2D>();
    }

    public bool CheckPosition(Vector2 positionToGo, Vector2 positionPlayer)
    {

        float Xa, Xb, Xc, Xd, Ya, Yb, Yc, Yd;
        Xa = positionPlayer.x;
        Xb = positionToGo.x;
        Ya = positionPlayer.y;
        Yb = positionToGo.y;

        for (int i = 0; i < geometry2D.points.Length-1; i++)
        {
            Xc = geometry2D.points[i].x;
            Yc = geometry2D.points[i].y;
            Xd = geometry2D.points[i+1].x;
            Yd = geometry2D.points[i+1].y;

            float X = ((Xd - Xc) * (Ya - Yc) - (Yd - Yc) * (Xa - Xc));
            float Y = ((Yd - Yc) * (Xb - Xa) - (Xd - Xc) * (Yb - Ya));

            float X1 = ((Xb - Xa) * (Ya - Yc) - (Yb - Ya) * (Xa - Xc));
            float Y1 = ((Yd - Yc) * (Xb - Xa) - (Xd - Xc) * (Yb - Ya));

            if ((X/Y < 1 && X/Y > 0) && X1 / Y1 < 1 && X1 / Y1 > 0)
            {
                Debug.Log(X/Y+"  Stop   "+X1/Y1);
                return false;
            }
        }

        return true;
    }
}
