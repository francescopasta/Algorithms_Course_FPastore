using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;

public class DungeonGeneratore : MonoBehaviour
{
    RectInt room;
    private List<RectInt> roomList = new();

    public int limit;

    //public bool theBastard;

    //public int wallWidthValue = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        room = new RectInt(0,0,100,50);
        roomList.Add(room);
        AlgorithmsUtils.DebugRectInt(roomList[0], Color.red, float.MaxValue);
        //theBastard = true;
        //StartCoroutine(StartLoop());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < roomList.Count; i++)
        {
            if (i % 2 == 0) 
            {
                AlgorithmsUtils.DebugRectInt(roomList[i], Color.blue);
            } else
            {
                AlgorithmsUtils.DebugRectInt(roomList[i], Color.red);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CutRectH(room);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            CutRectV(room);
        }
    }

    //IEnumerator StartLoop()
    //{
    //    for (int i = 0; i < limit; i++)
    //    {
    //        if (i % 2 == 0)
    //        {
    //            CutRectH(roomList[i]);
    //        }
    //        else
    //        {
    //            CutRectV(roomList[i]);
    //        }
    //        yield return new WaitForSeconds(2f);
    //    }
    //}

    RectInt CutSpecificRectVertical(RectInt rect)
    {
        int newWidth = rect.width / 2;
        RectInt newRect = new(0, 0, newWidth, rect.height);
        return newRect;
    }

    RectInt CutSpecificRectHorizontal(RectInt rect)
    {
        int newHeight= rect.height / 2;
        RectInt newRect = new(0, 0, rect.width, newHeight);
        return newRect;
    }

    void CutRectH(RectInt room)
    {
        RectInt oneRect = CutSpecificRectHorizontal(room);
        RectInt twoRect = new(0, oneRect.height, oneRect.width, oneRect.height);
        roomList.Add(oneRect);
        roomList.Add(twoRect);
    }
    void CutRectV(RectInt room)
    {
        RectInt oneRect = CutSpecificRectVertical(room);
        RectInt twoRect = new(oneRect.width, 0, oneRect.width, oneRect.height);
        roomList.Add(oneRect);
        roomList.Add(twoRect);
    }
}
