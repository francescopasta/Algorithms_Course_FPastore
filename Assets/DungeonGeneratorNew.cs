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
    RectInt roomFirst = new RectInt(0,0,100,50);
    List<RectInt> rooms;
    List<RectInt> roomsUsed = new List<RectInt>();
    //List<RectInt> theDoors = new List<RectInt>();

    public float animationTimeRooms;
    public float animationTimeDoors;
    public int limit = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(roomFirst, Color.red, float.MaxValue);
        StartCoroutine(AnimateCut());
    }

    IEnumerator AnimateCut()
    {
        yield return new WaitForSeconds(animationTimeRooms);
        //int boolean = ;
        if(Random.Range(2,0) % 2 == 0)
        {
            rooms = new List<RectInt>(CutterHeight(roomFirst));
        } else
        {
            rooms = new List<RectInt>(CutterWidth(roomFirst));
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            //if (rooms[i].width < limit * 2 && rooms[i].height < limit * 2) continue;
            if(rooms[i].width >= rooms[i].height && rooms[i].width > limit * 2)
            {
                yield return new WaitForSeconds(animationTimeRooms);
                List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                }
                roomsUsed.Add(rooms[i]);
            }
            else
            {
                if (rooms[i].height < limit * 2) continue;
                yield return new WaitForSeconds(animationTimeRooms);
                List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                }
                roomsUsed.Add(rooms[i]);
            }
        }

        StartCoroutine(AddDoors());
    }

    IEnumerator AddDoors()
    {
        //RectInt intersector = AlgorithmsUtils.Intersect(roomsUsed[0], roomsUsed[1]);
        //AlgorithmsUtils.DebugRectInt(intersector, Color.red, float.MaxValue);

        foreach (var room in roomsUsed)
        {
            for (global::System.Int32 i = 0; i < roomsUsed.Count; i++)
            {
                //if(AlgorithmsUtils.Intersects(room, roomsUsed[i]))
                //{
                    RectInt intersector = AlgorithmsUtils.Intersect(room, roomsUsed[i]);
                    AlgorithmsUtils.DebugRectInt(intersector, Color.red, float.MaxValue);
                    //AlgorithmsUtils.FillRectangleOutline(["A", "B"], intersector, "A");
                    yield return new WaitForSeconds(animationTimeDoors);
                //}

                //if (AlgorithmsUtils.Intersects(room, roomsUsed[i]))
                //{
                //    RectInt door = new RectInt(X + roomA.width, Random.Range(Y, Y + roomCut.height), 1, 1);
                //    StartCoroutine(ShowRect(door));
                //    AlgorithmsUtils.DebugRectInt(door, Color.yellow, float.MaxValue);
                //}
            } 
        }

    }

    IEnumerator ShowRect(RectInt rect)
    {
        yield return new WaitForSeconds(animationTimeDoors);
        AlgorithmsUtils.DebugRectInt(rect, Color.red, float.MaxValue);
    }

    List<RectInt> CutterWidth(RectInt roomCut)
    {
        //Store Original Position of RECT
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
 
        float halfWidth = roomCut.width * Random.Range(0.3f, 0.8f);
        //Create Two identical Rects that represent the two divided parts of the original RECT
        RectInt roomA = new RectInt(X, Y, roomCut.width - (int)halfWidth, roomCut.height);
        RectInt roomB = new RectInt(X + roomA.width + 1, Y, (int)halfWidth - 1, roomCut.height);
        //Display the two new Rects
        AlgorithmsUtils.DebugRectInt(roomA, Color.black, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomB, Color.black, float.MaxValue);

        ////ADD RANDOM DOOR

        //RectInt door = new RectInt(X + roomA.width, Random.Range(Y, Y + roomCut.height), 1, 1);
        //StartCoroutine(ShowRect(door));
        ////AlgorithmsUtils.DebugRectInt(door, Color.yellow, float.MaxValue);

        ////INSERT DOOR IN A LIST

        //theDoors.Add(door);

        //Add the two in a List in order to make them cuttable next
        List<RectInt> list = new();
        list.Add(roomA);
        list.Add(roomB);
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
        RectInt roomA = new RectInt(X, Y, roomCut.width,  roomCut.height - (int)halfHeight);
        RectInt roomB = new RectInt(X, Y + roomA.height + 1, roomCut.width, (int)halfHeight - 1);
        //Display the two new Rects
        AlgorithmsUtils.DebugRectInt(roomA, Color.black, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomB, Color.black, float.MaxValue);

        ////ADD RANDOM DOOR

        //RectInt door = new RectInt(Random.Range(X, X + roomA.width), Y + roomA.height, 1, 1);
        //StartCoroutine(ShowRect(door));
        ////AlgorithmsUtils.DebugRectInt(door, Color.yellow, float.MaxValue);

        //////INSERT DOOR IN A LIST

        //theDoors.Add(door);

        //Add the two in a List in order to make them cuttable next
        List<RectInt> list = new ();
        list.Add(roomA);
        list.Add(roomB);
        return list;
    }
}
