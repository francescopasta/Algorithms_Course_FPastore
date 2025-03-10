using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DungeonScript : MonoBehaviour
{
    private RectInt area = new RectInt(0, 0, 100, 50);

    public int limit = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(area, Color.green, float.MaxValue);
        Cut(area);
    }

    List<RectInt> Cut(RectInt room)
    {
        List<RectInt> rooms = new();

        int division = room.width / 2;
        rooms.Add(new RectInt(0,0,division,room.height));
        rooms.Add(new RectInt(division + 1, 0, division-1, room.height));

        foreach (RectInt roomma in rooms)
        {
            AlgorithmsUtils.DebugRectInt(roomma, Color.green, float.MaxValue); 
        }

        if(limit <= 0)
        {
            ReCut(rooms);
        }

        return rooms;
    }

    void ReCut(List<RectInt> list)
    {
        //if(limit >= 10)
        //{
            Dictionary<int, List<RectInt>> newRooms = new();

            for (int i = 0; i < list.Count; i++)
            {
                newRooms.Add(i, Cut(list[i]));
            }

            foreach (var item in newRooms)
            {
                ReCut(list);
            }

            limit++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
