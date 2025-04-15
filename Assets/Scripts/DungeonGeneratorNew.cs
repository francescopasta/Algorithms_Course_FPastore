using System.Collections;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.Rendering.DebugUI.Table;

public class DungeonGeneratorNew : MonoBehaviour
{
    private RectInt roomFirst = new (0, 0, 100, 50);
    private RectInt roomFirstOutline = new (-1, -1, 102, 52);
    private List<RectInt> rooms;
    private readonly List<RectInt> roomsUsed = new();
    private readonly List<RectInt> walls = new();
    private List<RectInt> doors = new();

    public float animationTimeRooms;
    public float animationTimeDoors;
    public int limit = 10;

    public WallsTileMap wallAssetsGenerator;

    //Awake is called when the scene start
    private void Awake()
    {
        wallAssetsGenerator = GetComponent<WallsTileMap>();
    }

    void Start()
    {
        AlgorithmsUtils.DebugRectInt(roomFirst, Color.blue, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomFirstOutline, Color.blue, float.MaxValue);
        StartCoroutine(AnimateCut(roomFirst));
    }

    IEnumerator AnimateCut(RectInt roomAlpha)
    {
        yield return new WaitForSeconds(animationTimeRooms);

        if (Random.Range(2, 0) % 2 == 0)
        {
            rooms = new List<RectInt>(CutterHeight(roomAlpha));
        } else
        {
            rooms = new List<RectInt>(CutterWidth(roomAlpha));
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].width >= rooms[i].height && rooms[i].width > limit * 2)
            {
                yield return new WaitForSeconds(animationTimeRooms);
                List<RectInt> list = new(CutterWidth(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                    roomsUsed.Add(room);
                }
                roomsUsed.Remove(rooms[i]);
            }
            else
            {
                if (rooms[i].height < limit * 2) continue;
                yield return new WaitForSeconds(animationTimeRooms);
                List<RectInt> list = new(CutterHeight(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                    roomsUsed.Add(room);
                }
                roomsUsed.Remove(rooms[i]);
            }
        }

        AddWalls();
    }

    void AddWalls()
    { 
        for (int i = 0; i < roomsUsed.Count; i++)
        {
            for (int j = i + 1; j < roomsUsed.Count; j++)
            {
                if (!AlgorithmsUtils.Intersects(roomsUsed[i], roomsUsed[j])) continue;
                RectInt wall = AlgorithmsUtils.Intersect(roomsUsed[i], roomsUsed[j]);
                walls.Add(wall);
            }
        }

        StartCoroutine(AddDoors());
    }

    IEnumerator AddDoors()
    {
        foreach (var wall in walls)
        {
            if (wall.width <= 4 && wall.height <= 4) continue;

            if(wall.width < wall.height)
            {
                RectInt door = new(wall.x, Random.Range(wall.y + 1, wall.y + wall.height - 1), 1, 1);
                doors.Add(door);
                AlgorithmsUtils.DebugRectInt(door, Color.red, float.MaxValue);
                yield return new WaitForSeconds(animationTimeDoors);
            } else
            {
                RectInt door = new(Random.Range(wall.x + 1, wall.x + wall.width - 1), wall.y, 1, 1);
                doors.Add(door);
                AlgorithmsUtils.DebugRectInt(door, Color.red, float.MaxValue);
                yield return new WaitForSeconds(animationTimeDoors);
            }
        }

        //StartCoroutine(wallAssetsGenerator.PlaceAssets());
    }

    List<RectInt> CutterWidth(RectInt roomCut)
    {
        //Store Original Position of RECT
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
        //Calculate the half to cut
        float halfWidth = roomCut.width * Random.Range(0.3f, 0.8f);
        //Create Two identical Rects that represent the two divided parts of the original RECT
        RectInt roomA = new (X, Y, roomCut.width - (int)halfWidth, roomCut.height);
        RectInt roomB = new (X + roomA.width - 1, Y, (int)halfWidth + 1, roomCut.height);
        //Display the two new Rects
        AlgorithmsUtils.DebugRectInt(roomA, Color.blue, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomB, Color.blue, float.MaxValue);
        //Add the two in a List in order to make them cuttable next
        List<RectInt> list = new() { roomA,roomB };
        return list;
    }

    List<RectInt> CutterHeight(RectInt roomCut)
    {
        //Store Original Position of RECT
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
        //Calculate the half to cut
        float halfHeight = roomCut.height * Random.Range(0.3f, 0.8f);
        //Create Two identical Rects that represent the two divided parts of the original RECT
        RectInt roomA = new (X, Y, roomCut.width,  roomCut.height - (int)halfHeight);
        RectInt roomB = new (X, Y + roomA.height - 1, roomCut.width, (int)halfHeight + 1);
        //Display the two new Rects
        AlgorithmsUtils.DebugRectInt(roomA, Color.blue, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomB, Color.blue, float.MaxValue);
        //Add the two in a List in order to make them cuttable next
        List<RectInt> list = new() { roomA, roomB };
        return list;
    }

    public List<RectInt> GetWalls()
    {
        return walls;
    }

    public List<RectInt> GetDoors()
    {
        return doors;
    }

    public RectInt GetDungeonBounds() 
    {
        return roomFirst;
    }
}
