using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class DungeonScript : MonoBehaviour
{
    private RectInt area = new RectInt(0, 0, 100, 50);

    public int limit = 0;

    private List<RectInt> rooms = new ();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(area, Color.green, float.MaxValue);
        Cut(area);
        Lol();
    }

    void Cut(RectInt room)
    {
        if (rooms.Count > 0)
        {
            rooms.Clear();
        }

        int division = room.width / 2;
        rooms.Add(new RectInt(0,0,division,room.height));
        rooms.Add(new RectInt(division + 1, 0, division - 1, room.height));

        foreach (var roomma in rooms)
        {
            AlgorithmsUtils.DebugRectInt(roomma, Color.green, float.MaxValue);
        }
    }

    void Lol()
    {
        List<RectInt> extraRooms = new ();
        extraRooms = rooms.ToList<RectInt>();
        for (int i = 0; i < extraRooms.Count; i++)
        {
            List<RectInt> newRooms = new ();
            AlgorithmsUtils.DebugRectInt(extraRooms[i], Color.green, float.MaxValue);
            int divisionTwo = extraRooms[i].width / 2;
            newRooms.Add(new RectInt(0, 0, divisionTwo, extraRooms[i].height));
            AlgorithmsUtils.DebugRectInt(newRooms[0], Color.blue, float.MaxValue);
            newRooms.Add(new RectInt((divisionTwo*2+1) + 1, 0, divisionTwo - 1, extraRooms[i].height));
            AlgorithmsUtils.DebugRectInt(newRooms[1], Color.red, float.MaxValue);
            //foreach (var room in newRooms)
            //{
            //    AlgorithmsUtils.DebugRectInt(room, Color.green, float.MaxValue);
            //}
        }
    }
}
