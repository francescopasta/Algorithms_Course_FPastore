using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class DungeonGeneratorNew : MonoBehaviour
{
    RectInt roomFirst = new RectInt(0,0,100,50);
    //Queue<RectInt> rooms = new ();
    //List<RectInt> stanze = new List<RectInt>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(roomFirst, Color.red, float.MaxValue);
        List<RectInt> rooms = new List<RectInt>(CutterHeight(roomFirst));
        //stanze.AddRange();
        //stanze.Add();
        CutterWidth(rooms[0]);
        CutterHeight(rooms[1]);


        //int switcher = 1;
        //for (int i = 0; i < 11; i++)
        //{
        //    if (i % 2 == 0)
        //    {
        //        CutterHeight(stanze[i]);
        //    }
        //    else
        //    {
        //        CutterWidth(stanze[i]);
        //    }
        //}
        //foreach (RectInt room in rooms)
        //{
        //    if (switcher % 2 == 0) 
        //    {
        //        CutterWidth(room);
        //        switcher++;
        //    } else
        //    {
        //        CutterHeight(room);
        //        switcher++;
        //    }
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CutterWidth(RectInt roomCut)
    {
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
        int halfWidth = roomCut.width / 2;
        RectInt roomA = new RectInt(X, Y, halfWidth, roomCut.height);
        AlgorithmsUtils.DebugRectInt(roomA, Color.green, float.MaxValue);
        RectInt roomB = new RectInt(X + roomA.width + 1, Y, halfWidth - 1, roomCut.height);
        AlgorithmsUtils.DebugRectInt(roomB, Color.green, float.MaxValue);
        //stanze.Add(roomA);
        //stanze.Add(roomB);
        //stanze.RemoveAt(0);
        //rooms.Enqueue(roomA);
        //rooms.Enqueue(roomB);
        //rooms.Dequeue();
    }

    List<RectInt> CutterHeight(RectInt roomCut)
    {
        //Store Original Position of RECT
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
        //Calculate the half to cut
        int halfHeight = roomCut.height / 2;
        //Create Two identical Rects that represent the two divided parts of the original RECT
        RectInt roomA = new RectInt(X, Y, roomCut.width,  halfHeight);
        RectInt roomB = new RectInt(X, Y + roomA.height + 1, roomCut.width, halfHeight - 1);
        //Display the two new Rects
        AlgorithmsUtils.DebugRectInt(roomA, Color.green, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomB, Color.green, float.MaxValue);
        //Add the two in a List in order to make them cuttable next
        List<RectInt> list = new ();
        list.Add(roomA);
        list.Add(roomB);
        return list;

        //stanze.Add(roomA);
        //stanze.Add(roomB);
        //stanze.RemoveAt(0);
        //rooms.Enqueue(roomA);
        //rooms.Enqueue(roomB);
        //rooms.Dequeue();
    }
}
