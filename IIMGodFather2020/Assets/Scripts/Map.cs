using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Vector2 startPos;
    public int sizeX;
    public int sizeY;

    public float stepPos;

    public List<int[]> map = new List<int[]>();

    public struct MapShard
    {
        public int sizeX;
        public int sizeY;

        public MapShard[] shardsList;

    }

    public struct PlacementPoint
    {
        public int posx;
        public int posy;

        public Vector2 realPos;
    }

    public void CreateMap()
    {
        int stepCreateX = (int)(sizeX / stepPos);
        int stepCreateY = (int)(sizeY / stepPos);

        
    }
}

