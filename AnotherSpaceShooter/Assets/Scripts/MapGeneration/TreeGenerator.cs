using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public int width;
    public int height;

    [Range(0, 100)]
    public int randomFillPercent;       //Some loose guideline on how much to fill. 45 seems to be a good fit.

    int[,] map;                         //0 = Empty, 1 = Tree

    public GameObject BigTree;
    public GameObject SmallTree;


    private void Start()
    {
        GenerateForest();
    }

    void GenerateForest()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 3; i++)
        {
            //Maybe add a way to change the number of iterations on this?
            SmoothForest();
        }

        DrawTrees();
    }

    void RandomFillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Loop through each of the tiles on the map.
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    //If we are on the edge of map, set this to be a tree.
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (Random.Range(0, 100) < randomFillPercent) ? 1 : 0; //If random is lower than fillPercent, go 1, else 0
                }
            }
        }
    }

    void SmoothForest()
    {
        int[,] newMap = map;
        for (int x = 0; x < width; x++)
        {
            //Loop though each of the tiles on the map.
            for (int y = 0; y < height; y++)
            {
                //Look through each tile on a 3x3, count neighbouring trees
                int neighbourTrees = GetSurroundingTrees(x, y);

                if (neighbourTrees > 4)
                    newMap[x, y] = 1;
                else if (neighbourTrees < 4)
                    newMap[x, y] = 0;
            }
        }
        map = newMap;
    }

    int GetSurroundingTrees(int gridX, int gridY)
    {
        int treeCount = 0;
        //Look through all adjacent tiles (3x3)
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                //Make sure we are not looking on tiles outside map.
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    //The tile we use as origin is filtered out.
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        treeCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    //This gets called if we are looking at a tile outside the map; this is considered a tree.
                    treeCount++;
                }
            }
        }

        return treeCount;
    }

    void DrawTrees()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1)
                {
                    int treeType = Random.Range(0, 2);
                    Vector2 rOffset = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)); //Random offset to each tree, to make it look a little bit more natural.
                    Vector3 pos = new Vector3(-width / 2 + x + rOffset.x, -height / 2 + y + .5f + rOffset.y, 0);
                    

                    if (treeType == 0)
                    {
                        GameObject.Instantiate(BigTree, pos / 4, Quaternion.Euler(0,0, Random.Range(0, 360)));
                    }
                    else
                    {
                        GameObject.Instantiate(SmallTree, pos / 4, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    }
                }
            }
        }
    }
}
