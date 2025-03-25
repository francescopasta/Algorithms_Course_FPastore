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
    List<RectInt> theDoors = new List<RectInt>();

    public float animationTime;
    public int limit = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(roomFirst, Color.red, float.MaxValue);
        StartCoroutine(AnimateCut());
    }

    IEnumerator AnimateCut()
    {
        yield return new WaitForSeconds(animationTime);
        //int boolean = ;
        if(Random.Range(2,0) % 2 == 0)
        {
            rooms = new List<RectInt>(CutterHeight(roomFirst));
        } else
        {
            rooms = new List<RectInt>(CutterWidth(roomFirst));
        }
        

        //int exodus = 0;

        //int i = 0;

        //for (int i = 0; i < rooms.Count; i++)
        //{
        //    if (rooms[i].width >= rooms[i].height && rooms[i].width >= limit * 2)
        //    {
        //        yield return new WaitForSeconds(animationTime);
        //        List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //        foreach (var room in list)
        //        {
        //            rooms.Add(room);
        //        }
        //        roomsUsed.Add(rooms[i]);
        //    }
        //    else if (rooms[i].height > rooms[i].width && rooms[i].height >= limit * 2)
        //    {
        //        yield return new WaitForSeconds(animationTime);
        //        List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //        foreach (var room in list)
        //        {
        //            rooms.Add(room);
        //        }
        //        roomsUsed.Add(rooms[i]);
        //    } else 
        //    {
        //        continue;
        //    }
        //}

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].width < limit * 2 && rooms[i].height < limit * 2) continue;
            if(rooms[i].width >= rooms[i].height && rooms[i].width > limit * 2)
            {
                yield return new WaitForSeconds(animationTime);
                List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                }
            }
            else
            {
                if (rooms[i].height < limit * 2) continue;
                yield return new WaitForSeconds(animationTime);
                List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                }
            }
        }

        //for (int i = 0; i < rooms.Count; i++)
        //{
        //    //if (rooms[i].width > limit * 2 && rooms[i].height > limit * 2)
        //    //{

        //    //}

        //    if (rooms[i].width > rooms[i].height && rooms[i].width > limit * 2)
        //    {
        //        //if (rooms[i].width < limit)
        //        //{
        //        //    yield return new WaitForSeconds(animationTime);
        //        //    List<RectInt> listB = new List<RectInt>(CutterHeight(rooms[i]));
        //        //    foreach (var room in listB)
        //        //    {
        //        //        rooms.Add(room);
        //        //    }
        //        //    continue;
        //        //}
        //        yield return new WaitForSeconds(animationTime);
        //        List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //        foreach (var room in list)
        //        {
        //            rooms.Add(room);
        //        }
        //    } else 
        //    {
        //        if (rooms[i].height > limit * 2)
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //            foreach (var room in list)
        //            {
        //                rooms.Add(room);
        //            }
        //        } else
        //        {
        //            continue;
        //        }
        //        //if (rooms[i].height < limit)
        //        //{
        //        //    yield return new WaitForSeconds(animationTime);
        //        //    List<RectInt> listB = new List<RectInt>(CutterWidth(rooms[i]));
        //        //    foreach (var room in listB)
        //        //    {
        //        //        rooms.Add(room);
        //        //    }
        //        //    continue;
        //        //}

        //    } 
        //    //else
        //    //{
        //    //    if (exodus % 2 == 0)
        //    //    {
        //    //        if (rooms[i].width < limit)
        //    //        {
        //    //            yield return new WaitForSeconds(animationTime);
        //    //            List<RectInt> listB = new List<RectInt>(CutterHeight(rooms[i]));
        //    //            foreach (var room in listB)
        //    //            {
        //    //                rooms.Add(room);
        //    //            }
        //    //            continue;
        //    //        }
        //    //        yield return new WaitForSeconds(animationTime);
        //    //        List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //    //        foreach (var room in list)
        //    //        {
        //    //            rooms.Add(room);
        //    //        }
        //    //    } else
        //    //    {
        //    //        if (rooms[i].height < limit)
        //    //        {
        //    //            yield return new WaitForSeconds(animationTime);
        //    //            List<RectInt> listB = new List<RectInt>(CutterWidth(rooms[i]));
        //    //            foreach (var room in listB)
        //    //            {
        //    //                rooms.Add(room);
        //    //            }
        //    //            continue;
        //    //        }
        //    //        yield return new WaitForSeconds(animationTime);
        //    //        List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //    //        foreach (var room in list)
        //    //        {
        //    //            rooms.Add(room);
        //    //        }
        //    //    }
        //    //    exodus++;
        //    //}
        //}
    }

    IEnumerator ShowRect(RectInt rect)
    {
        yield return new WaitForSeconds(animationTime);
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
        AlgorithmsUtils.DebugRectInt(roomA, Color.blue, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomB, Color.blue, float.MaxValue);

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
        AlgorithmsUtils.DebugRectInt(roomA, Color.blue, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomB, Color.blue, float.MaxValue);

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
