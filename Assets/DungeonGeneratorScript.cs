using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DungeonGeneratorScript : MonoBehaviour
{
    private RectInt area = new RectInt(0,0,100,50);

    public bool SplitHorizontally = false;

    private List<RectInt> rooms = new List<RectInt>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(area,Color.green, float.MaxValue);
    }

    List<RectInt> SplitRoomVertically(RectInt room)
    {
        if (SplitHorizontally) 
        {
            int division = room.height / 2;
            RectInt wall = new RectInt(0, division, room.width, 2);
            AlgorithmsUtils.DebugRectInt(wall, Color.red, float.MaxValue);

            List<RectInt> rooms = new()
            {
                new RectInt(0, 0, room.width, division),
                new RectInt(0, division, room.width, division)
            };

            foreach (var item in rooms)
            {
                AlgorithmsUtils.DebugRectInt(item, Color.blue, float.MaxValue);
            }

            SplitHorizontally = !SplitHorizontally;

            return rooms;

        } else
        {
            int division = room.width / 2;
            RectInt wall = new RectInt(division, 0, 2, room.height);
            AlgorithmsUtils.DebugRectInt(wall, Color.red, float.MaxValue);

            List<RectInt> rooms = new()
            {
                new RectInt(0, 0, division, room.height),
                new RectInt(division, 0, division, room.height)
            };

            SplitHorizontally = !SplitHorizontally;

            return rooms;
        }
    }

    void SplitTwo(List<RectInt> rooms)
    {
        foreach (var room in rooms)
        {
            SplitRoomVertically(room);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(VisualDebugging());
            //SplitTwo(SplitRoomVertically(area));
        }
    }

    IEnumerator VisualDebugging()
    {
        yield return new WaitForSeconds(1);
        rooms = SplitRoomVertically(area);
        yield return new WaitForSeconds(1);
        SplitTwo(rooms);
    }
}
