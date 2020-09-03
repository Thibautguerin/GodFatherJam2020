using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public int[] mousePos = { 3, 3 };
    public int[] playerPos = { 0, 0 };
    public List<int[]> map = new List<int[]> { new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 } };
    public List<List<int[]>> paths = new List<List<int[]>>();

    void Start()
    {
        PathFindingTest(mousePos, playerPos, map, paths);
    }

    void Update()
    {
    }

    void PathFindingTest(int[] mousePos, int[] playerPos, List<int[]> map, List<List<int[]>> paths)
    {
        int actualPath = 0;
        int bestPathSize = int.MaxValue;
        paths.Add(new List<int[]>());

        // recursive pathfinding
        checkAroundPos(playerPos, mousePos, map, ref actualPath, ref paths, ref bestPathSize);
        paths.RemoveAt(paths.Count - 1);

        // display all paths found (debug)

        for (int i = 0; i < paths.Count; i++)
        {
            Debug.Log(i.ToString() + ": ");
            for (int j = 0; j < paths[i].Count; j++)
                Debug.Log("[" + paths[i][j][0].ToString() + ", " + paths[i][j][1].ToString() + "], ");
            Debug.Log("\n");
        }

        if (paths.Count != 0)
        {
            // path(s) found
            int bestPath = paths.Count - 1;
            Debug.Log("BestPath: " + bestPath.ToString());
            Debug.Log("\n");

            for (int i = 0; i < paths[bestPath].Count; i++)
                Debug.Log("[" + paths[bestPath][i][0].ToString() + ", " + paths[bestPath][i][1].ToString() + "], ");
        }
    }

    void checkAroundPos(int[] pos, int[] mousePos, List<int[]> map, ref int actualPath, ref List<List<int[]>> paths, ref int bestPathSize)
    {
        paths[actualPath].Add(pos);

        // check right
        /*Debug.Log("Check Right: " + pos[0].ToString() + "|" + pos[1].ToString());*/
        if (pos[0] + 1 < map[pos[1]].Length && map[pos[1]][pos[0] + 1] == 0
            && !alreadyInPath(new int[] { pos[0] + 1, pos[1] }, ref actualPath, ref paths))
        {
            /*Debug.Log("Right\n");*/
            if (pos[0] + 1 == mousePos[0] && pos[1] == mousePos[1] && paths[actualPath].Count + 1 < bestPathSize)
            {
                /*Debug.Log("OK!\n");*/
                paths[actualPath].Add(new int[] { pos[0] + 1, pos[1] });
                bestPathSize = paths[actualPath].Count;
                paths.Add(new List<int[]>(paths[actualPath]));
                actualPath++;
                paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
            }
            else if (paths[actualPath].Count + 1 < bestPathSize)
                checkAroundPos(new int[]{pos[0] + 1, pos[1]}, mousePos, map, ref actualPath, ref paths, ref bestPathSize);
        }

        // check right/bottom
        /*Debug.Log("Check Right/Bottom: " + pos[0].ToString() + "|" + pos[1].ToString());*/
        if (pos[0] + 1 < map[pos[1]].Length && pos[1] + 1 < map.Count && map[pos[1] + 1][pos[0] + 1] == 0
            && !alreadyInPath(new int[] { pos[0] + 1, pos[1] + 1 }, ref actualPath, ref paths))
        {
            /*Debug.Log("Right/Bottom\n");*/
            if (pos[0] + 1 == mousePos[0] && pos[1] + 1 == mousePos[1] && paths[actualPath].Count + 1 < bestPathSize)
            {
                /*Debug.Log("OK!\n");*/
                paths[actualPath].Add(new int[] { pos[0] + 1, pos[1] + 1 });
                bestPathSize = paths[actualPath].Count;
                paths.Add(new List<int[]>(paths[actualPath]));
                actualPath++;
                paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
            }
            else if (paths[actualPath].Count + 1 < bestPathSize)
                checkAroundPos(new int[] { pos[0] + 1, pos[1] + 1 }, mousePos, map, ref actualPath, ref paths, ref bestPathSize);
        }

        // check bottom
        /*Debug.Log("Check Bottom: " + pos[0].ToString() + "|" + pos[1].ToString());*/
        if (pos[1] + 1 < map.Count && map[pos[1] + 1][pos[0]] == 0
            && !alreadyInPath(new int[] { pos[0], pos[1] + 1 }, ref actualPath, ref paths))
        {
            /*Debug.Log("Bottom\n");*/
            if (pos[0] == mousePos[0] && pos[1] + 1 == mousePos[1] && paths[actualPath].Count + 1 < bestPathSize)
            {
                /*Debug.Log("OK!\n");*/
                paths[actualPath].Add(new int[] { pos[0], pos[1] + 1 });
                bestPathSize = paths[actualPath].Count;
                paths.Add(new List<int[]>(paths[actualPath]));
                actualPath++;
                paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
            }
            else if (paths[actualPath].Count + 1 < bestPathSize)
                checkAroundPos(new int[]{pos[0], pos[1] + 1}, mousePos, map, ref actualPath,ref paths, ref bestPathSize);
        }

        // check bottom/left
        /*Debug.Log("Check Bottom/Left: " + pos[0].ToString() + "|" + pos[1].ToString());*/
        if (pos[1] + 1 < map.Count && pos[0] - 1 >= 0 && map[pos[1] + 1][pos[0] - 1] == 0
            && !alreadyInPath(new int[] { pos[0] - 1, pos[1] + 1 }, ref actualPath, ref paths))
        {
            /*Debug.Log("Bottom/Left\n");*/
            if (pos[0] - 1 == mousePos[0] && pos[1] + 1 == mousePos[1] && paths[actualPath].Count + 1 < bestPathSize)
            {
                /*Debug.Log("OK!\n");*/
                paths[actualPath].Add(new int[] { pos[0] - 1, pos[1] + 1 });
                bestPathSize = paths[actualPath].Count;
                paths.Add(new List<int[]>(paths[actualPath]));
                actualPath++;
                paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
            }
            else if (paths[actualPath].Count + 1 < bestPathSize)
                checkAroundPos(new int[] { pos[0] - 1, pos[1] + 1 }, mousePos, map, ref actualPath, ref paths, ref bestPathSize);
        }

        // check left
        /*Debug.Log("Check Left: " + pos[0].ToString() + "|" + pos[1].ToString());*/
        if (pos[0] - 1 >= 0 && map[pos[1]][pos[0] - 1] == 0
            && !alreadyInPath(new int[] { pos[0] - 1, pos[1] }, ref actualPath, ref paths))
        {
            /*Debug.Log("Left\n");*/
            if (pos[0] - 1 == mousePos[0] && pos[1] == mousePos[1] && paths[actualPath].Count + 1 < bestPathSize)
            {
                /*Debug.Log("OK!\n");*/
                paths[actualPath].Add(new int[] { pos[0] - 1, pos[1] });
                bestPathSize = paths[actualPath].Count;
                paths.Add(new List<int[]>(paths[actualPath]));
                actualPath++;
                paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
            }
            else if (paths[actualPath].Count + 1 < bestPathSize)
                checkAroundPos(new int[]{pos[0] - 1, pos[1]}, mousePos, map, ref actualPath, ref paths, ref bestPathSize);
        }

        // check left/top
        /*Debug.Log("Check Left/Top: " + pos[0].ToString() + "|" + pos[1].ToString());*/
        if (pos[0] - 1 >= 0 && pos[1] - 1 >= 0 && map[pos[1] - 1][pos[0] - 1] == 0
            && !alreadyInPath(new int[] { pos[0] - 1, pos[1] - 1 }, ref actualPath, ref paths))
        {
            /*Debug.Log("Left/Top\n");*/
            if (pos[0] - 1 == mousePos[0] && pos[1] - 1 == mousePos[1] && paths[actualPath].Count + 1 < bestPathSize)
            {
                /*Debug.Log("OK!\n");*/
                paths[actualPath].Add(new int[] { pos[0] - 1, pos[1] - 1 });
                bestPathSize = paths[actualPath].Count;
                paths.Add(new List<int[]>(paths[actualPath]));
                actualPath++;
                paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
            }
            else if (paths[actualPath].Count + 1 < bestPathSize)
                checkAroundPos(new int[] { pos[0] - 1, pos[1] - 1 }, mousePos, map, ref actualPath, ref paths, ref bestPathSize);
        }

        // check top
        /*Debug.Log("Check Top: " + pos[0].ToString() + "|" + pos[1].ToString());*/
        if (pos[1] - 1 >= 0 && map[pos[1] - 1][pos[0]] == 0
            && !alreadyInPath(new int[] { pos[0], pos[1] - 1 }, ref actualPath, ref paths))
        {
            /*Debug.Log("Top\n");*/
            if (pos[0] == mousePos[0] && pos[1] - 1 == mousePos[1] && paths[actualPath].Count + 1 < bestPathSize)
            {
                /*Debug.Log("OK!\n");*/
                paths[actualPath].Add(new int[] { pos[0], pos[1] - 1 });
                bestPathSize = paths[actualPath].Count;
                paths.Add(new List<int[]>(paths[actualPath]));
                actualPath++;
                paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
            }
            else if (paths[actualPath].Count + 1 < bestPathSize)
                checkAroundPos(new int[]{ pos[0], pos[1] - 1}, mousePos, map, ref actualPath, ref paths, ref bestPathSize);
        }

        // check top/right
        /*Debug.Log("Check Top/Right: " + pos[0].ToString() + "|" + pos[1].ToString());*/
        if (pos[1] - 1 >= 0 && pos[0] + 1 < map[pos[1]].Length && map[pos[1] - 1][pos[0] + 1] == 0
            && !alreadyInPath(new int[] { pos[0] + 1, pos[1] - 1 }, ref actualPath, ref paths))
        {
            /*Debug.Log("Top/Right\n");*/
            if (pos[0] + 1 == mousePos[0] && pos[1] - 1 == mousePos[1] && paths[actualPath].Count + 1 < bestPathSize)
            {
                /*Debug.Log("OK!\n");*/
                paths[actualPath].Add(new int[] { pos[0] + 1, pos[1] - 1 });
                bestPathSize = paths[actualPath].Count;
                paths.Add(new List<int[]>(paths[actualPath]));
                actualPath++;
                paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
            }
            else if (paths[actualPath].Count + 1 < bestPathSize)
                checkAroundPos(new int[] { pos[0] + 1, pos[1] - 1 }, mousePos, map, ref actualPath, ref paths, ref bestPathSize);
        }

        paths[actualPath].RemoveAt(paths[actualPath].Count - 1);
    }

    bool alreadyInPath(int[] pos, ref int actualPath, ref List<List<int[]>> paths)
    {
        for (int i = 0; i < paths[actualPath].Count; i++)
            if (paths[actualPath][i][0] == pos[0] && paths[actualPath][i][1] == pos[1])
                    return true;
        return false;
    }
}
