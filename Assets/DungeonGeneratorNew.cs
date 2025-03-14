using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class DungeonGeneratorNew : MonoBehaviour
{
    RectInt roomFirst = new RectInt(0,0,100,50);
    Queue<RectInt> rooms = new ();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(roomFirst, Color.red, float.MaxValue);
        Cutter(roomFirst);
        foreach (RectInt room in rooms)
        {
            Cutter(room);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Cutter(RectInt roomCut)
    {
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
        int halfWidth = roomCut.width / 2;
        RectInt roomA = new RectInt(X, Y, halfWidth, roomCut.height);
        AlgorithmsUtils.DebugRectInt(roomA, Color.green, float.MaxValue);
        RectInt roomB = new RectInt(roomA.width+2, Y, halfWidth-2, roomCut.height);
        AlgorithmsUtils.DebugRectInt(roomB, Color.green, float.MaxValue);
        rooms.Enqueue(roomA);
        rooms.Enqueue(roomB);
        rooms.Dequeue();
    }
}
