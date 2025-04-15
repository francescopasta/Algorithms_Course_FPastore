using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsTileMap : MonoBehaviour
{
    private DungeonGeneratorNew dungeonGenerator;
    private int[,] tileMap;
    private List<RectInt> walls;
    private List<RectInt> doors;

    public GameObject wallPrefabPlain;
    public GameObject wallPrefabCorner;
    public GameObject wallPrefabWallEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dungeonGenerator = GetComponent<DungeonGeneratorNew>();
        walls = dungeonGenerator.GetWalls();
        doors = dungeonGenerator.GetDoors();
    }

    public IEnumerator PlaceAssets()
    {
        yield return new WaitForSeconds(1);
        GenerateTileMap();
    }

    public void GenerateTileMap()
    {
        int[,] _tileMap = new int[dungeonGenerator.GetDungeonBounds().height, dungeonGenerator.GetDungeonBounds().width];
        int rows = _tileMap.GetLength(0);
        int cols = _tileMap.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                _tileMap[i, j] = 0;
            }
        }

        foreach (RectInt wall in walls)
        {
            AlgorithmsUtils.FillRectangleOutline(_tileMap, wall, 1);
        }

        foreach (RectInt door in doors)
        {
            AlgorithmsUtils.FillRectangleOutline(_tileMap, door, 1);
        }

        tileMap = _tileMap;
        DrawWalls();
    }

    public void DrawWalls()
    {
        int rows = tileMap.GetLength(0);
        int cols = tileMap.GetLength(1);

        for (int r = 0; r < rows; r++)
        {
            int y = r;
            for (int c = 0; c < cols; c++)
            {
                int x = c;

                int tileOne = tileMap[r, c];
                int tileTwo = tileMap[r, c + 1];
                int tileThree = tileMap[r + 1, c];
                int tileFour = tileMap[r + 1, c + 1];

                Debug.Log(tileOne);
                Debug.Log(tileTwo);
                Debug.Log(tileThree);
                Debug.Log(tileFour);

                RectInt tileMesh = new(tileOne, tileOne, 4,4);
                AlgorithmsUtils.DebugRectInt(tileMesh, Color.red, float.MaxValue);

                //Instantiate(new RectInt(tileOne,1,1));





                //if (tileMap[r,c] == 1)
                //{
                //    Instantiate(wallPrefabPlain, new(tileMap.Get, wallPosition.y, wallPosition.z + r), Quaternion.identity);
                //}
            }
        }
    }
}
