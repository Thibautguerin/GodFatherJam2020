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
    public List<MapShard> allShards = new List<MapShard>();

    public class MapShard
    {
        public int sizeX = 0;
        public int sizeY = 0;

        public MapShard[] shardsList = new MapShard[4];

        public List<PlacementPoint> placementPoints = new List<PlacementPoint>();

        public MapShard(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
        }

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

        //MapShard firstShard =
    }
}

