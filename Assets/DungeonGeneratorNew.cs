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

    public float animationTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlgorithmsUtils.DebugRectInt(roomFirst, Color.red, float.MaxValue);
        StartCoroutine(AnimateCut());
    }

    IEnumerator AnimateCut()
    {
        yield return new WaitForSeconds(animationTime);
        rooms = new List<RectInt>(CutterHeight(roomFirst));

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].width < 10 && rooms[i].height < 10) continue;

            if (rooms[i].width > rooms[i].height)
            {
                if (rooms[i].width < 10)
                {
                    yield return new WaitForSeconds(animationTime);
                    List<RectInt> listB = new List<RectInt>(CutterHeight(rooms[i]));
                    foreach (var room in listB)
                    {
                        rooms.Add(room);
                    }
                    continue;
                }
                yield return new WaitForSeconds(animationTime);
                List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                }
            } else if(rooms[i].width < rooms[i].height)
            {
                if (rooms[i].height < 10)
                {
                    yield return new WaitForSeconds(animationTime);
                    List<RectInt> listB = new List<RectInt>(CutterWidth(rooms[i]));
                    foreach (var room in listB)
                    {
                        rooms.Add(room);
                    }
                    continue;
                }
                yield return new WaitForSeconds(animationTime);
                List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
                foreach (var room in list)
                {
                    rooms.Add(room);
                }
            } else
            {
                //int randomBro = Random.Range(1, 3);
                //if(randomBro == 1)
                //{
                //    yield return new WaitForSeconds(animationTime);
                //    List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
                //    foreach (var room in list)
                //    {
                //        rooms.Add(room);
                //    }
                //    continue;
                //}
                //if (randomBro == 2) 
                //{
                //    yield return new WaitForSeconds(animationTime);
                //    List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
                //    foreach (var room in list)
                //    {
                //        rooms.Add(room);
                //    }
                //    continue;
                //}
                continue;
            }
        }
    }

    List<RectInt> CutterWidth(RectInt roomCut)
    {
        //Store Original Position of RECT
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
        //Calculate the half to cut
        //float normalized = (roomCut.width) - 10f / 100f - 10f;
        //float approx = (0.3f * (1f - normalized));
        //float minRange = Mathf.Max(0f, 0.5f - approx);
        //float maxRange = Mathf.Min(1f, 0.5f + approx);

        //float normalized = roomCut.width / 100;


        float halfWidth = roomCut.width * Random.Range(0.2f, 0.9f);
        //Create Two identical Rects that represent the two divided parts of the original RECT
        RectInt roomA = new RectInt(X, Y, roomCut.width - (int)halfWidth, roomCut.height);
        RectInt roomB = new RectInt(X + roomA.width + 1, Y, (int)halfWidth - 1, roomCut.height);
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
        //float normalized = (roomCut.width) - 10f / 100f - 10f;
        //float approx = (0.3f * (1f - normalized));
        //float minRange = Mathf.Max(0f, 0.5f - approx);
        //float maxRange = Mathf.Min(1f, 0.5f + approx);
        float halfHeight = roomCut.height * Random.Range(0.2f, 0.9f);
        //Create Two identical Rects that represent the two divided parts of the original RECT
        RectInt roomA = new RectInt(X, Y, roomCut.width,  roomCut.height - (int)halfHeight);
        RectInt roomB = new RectInt(X, Y + roomA.height + 1, roomCut.width, (int)halfHeight - 1);
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
