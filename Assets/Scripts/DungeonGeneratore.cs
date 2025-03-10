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
    List<RectInt> listaNuova = new();

    public int limit;

    //public bool theBastard;

    //public int wallWidthValue = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        room = new RectInt(0,0,100,50);
        roomList.Add(room);
        AlgorithmsUtils.DebugRectInt(roomList[0], Color.red, float.MaxValue);
        //StartCoroutine(StartLoop(roomList));
        //theBastard = true;
        //StartCoroutine(StartLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (roomList.Count > 0) 
            {
                CutRectH(roomList[0]);
            }
            if (listaNuova.Count > 0) 
            {
                CutRectH(listaNuova[0]);
            }
        }

        foreach (var item in roomList)
        {
            AlgorithmsUtils.DebugRectInt(item, Color.red);
        }

        foreach (var item in listaNuova)
        {
            AlgorithmsUtils.DebugRectInt(item, Color.red);
        }
    }

    IEnumerator StartLoop(List<RectInt> list)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            foreach (var item in list)
            {
                AlgorithmsUtils.DebugRectInt(item, Color.green, float.MaxValue);
            }
            //if (i % 2 == 0)
            //{
            CutRectH(list[i]);
            //}
            //else
            //{
            //    CutRectH(roomList[i]);
            //}
            yield return new WaitForSeconds(2f);
        }
    }

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
        if (roomList.Count <= 0)
        {
            roomList.Clear();
        } 
        if(listaNuova.Count <= 0)
        {
            listaNuova.Clear();
        }

        roomList.Add(oneRect);
        listaNuova.Add(twoRect);
    }
    //void CutRectV(RectInt room)
    //{
    //    RectInt oneRect = CutSpecificRectVertical(room);
    //    RectInt twoRect = new(oneRect.width, 0, oneRect.width, oneRect.height);
    //    roomList.RemoveAt(0);
    //    if (roomList.Count > 1)
    //    {
    //        roomList.RemoveAt(1);
    //    }
    //    roomList.Add(oneRect);
    //    roomList.Add(twoRect);
    //}
}
