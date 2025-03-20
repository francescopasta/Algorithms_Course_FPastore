using System.Collections;
using System.Collections.Generic;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(roomFirst, Color.red, float.MaxValue);
        StartCoroutine(AnimateCut());
    }

    IEnumerator AnimateCut()
    {
        yield return new WaitForSeconds(1);
        rooms = new List<RectInt>(CutterHeight(roomFirst));

        for (int i = 0; i < 11; i++)
        {
            if (i % 2 == 0)
            {
                if (roomsUsed.Contains(rooms[i])) continue;
                yield return new WaitForSeconds(1);
                List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                }
                roomsUsed.Add(rooms[i]);
            }
            else
            {
                if (roomsUsed.Contains(rooms[i])) continue;
                yield return new WaitForSeconds(1);
                List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                }
                roomsUsed.Add(rooms[i]);
            }
        }
    }

    List<RectInt> CutterWidth(RectInt roomCut)
    {
        //Store Original Position of RECT
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
        //Calculate the half to cut
        int halfWidth = roomCut.width / 2;
        //Create Two identical Rects that represent the two divided parts of the original RECT
        RectInt roomA = new RectInt(X, Y, halfWidth, roomCut.height);
        RectInt roomB = new RectInt(X + roomA.width + 1, Y, halfWidth - 1, roomCut.height);
        //Display the two new Rects
        AlgorithmsUtils.DebugRectInt(roomA, Color.green, float.MaxValue);
        AlgorithmsUtils.DebugRectInt(roomB, Color.green, float.MaxValue);
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
    }
}
