using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;

public class DungeonGeneratore : MonoBehaviour
{
    RectInt room;
    private List<RectInt> roomList = new();

    //public int wallWidthValue = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        room = new RectInt(0,0,100,50);
        roomList.Add(room);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            AlgorithmsUtils.DebugRectInt(roomList[i], Color.red);
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            CutRect(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            CutRect(true);
        }
    }

    void CutRect(bool splitHorizontally)
    {
        RectInt rect = roomList[0];
        RectInt newRect;

        rect.width /= 2;
        newRect = new RectInt(rect.width + 2, 0, rect.width - 2, rect.height);

        roomList.Add(rect);
        roomList.Add(newRect);

        RectInt alab= roomList[roomList.Count-1];
        RectInt ilib = roomList[roomList.Count];

        //roomList.Insert(0, rect);



        //if (!splitHorizontally)
        //{

        //}
        //else
        //{
        //    rect.height /= 2;
        //    newRect = new RectInt(0, rect.height + 2, rect.width, rect.height - 2);
        //    roomList.Add(rect);
        //    roomList.Add(newRect);
        //}

        //for (int i = 0; i < 10; i++)
        //{
        //    if (!splitHorizontally)
        //    {
        //        rect.width = (roomma.width / 2) /*- 1 * wallWidthValue*/;
        //        newRect = new RectInt(50 + 2 * wallWidthValue, 0, rect.width - 2 * wallWidthValue, rect.height);
        //        roomList.Add(rect);
        //        roomList.Add(newRect);
        //    }
        //    else
        //    {
        //        rect.height = (roomma.height / 2) - 1 * wallWidthValue;
        //        newRect = new RectInt(0, 25 + 2 * wallWidthValue, rect.width, rect.height - 2 * wallWidthValue);
        //        roomList.Add(rect);
        //        roomList.Add(newRect);
        //    }

        //    splitHorizontally = !splitHorizontally;

        //    rect = roomList[i + 1];
        //    newRect = roomList[i + 2];
        //}

    }

}
