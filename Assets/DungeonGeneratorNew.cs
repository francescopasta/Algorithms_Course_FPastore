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

        //if (rooms[i].width > 10)
        //{
        //    yield return new WaitForSeconds(animationTime);
        //    List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //    foreach (var room in list)
        //    {
        //        rooms.Add(room);
        //    }
        //    roomsUsed.Add(rooms[i]);
        //}
        //else if (rooms[i].width < 10 && rooms[i].height > 10)
        //{
        //    yield return new WaitForSeconds(animationTime);
        //    List<RectInt> listB = new List<RectInt>(CutterHeight(rooms[i]));
        //    foreach (var room in listB)
        //    {
        //        rooms.Add(room);
        //    }
        //    roomsUsed.Add(rooms[i]);
        //}
        //else
        //{
        //    continue;
        //}
        //if (rooms[i].height > 10)
        //{
        //    yield return new WaitForSeconds(animationTime);
        //    List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //    foreach (var room in list)
        //    {
        //        rooms.Add(room);
        //    }
        //    roomsUsed.Add(rooms[i]);
        //}
        //else if (rooms[i].height < 10 && rooms[i].width > 10)
        //{
        //    yield return new WaitForSeconds(animationTime);
        //    List<RectInt> listB = new List<RectInt>(CutterWidth(rooms[i]));
        //    foreach (var room in listB)
        //    {
        //        rooms.Add(room);
        //    }
        //    roomsUsed.Add(rooms[i]);
        //}
        //else
        //{
        //    continue;
        //}

        //for (int i = 0; i < rooms.Count; i++)
        //{
        //    if (i % 2 == 0)
        //    {
        //        if (rooms[i].width < 10 || rooms[i].height < 10) continue;
        //        yield return new WaitForSeconds(animationTime);
        //        List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //        foreach (var room in list)
        //        {
        //            rooms.Add(room);
        //        }
        //    }
        //    else
        //    {
        //        if (rooms[i].width < 10 || rooms[i].height < 10) continue;
        //        yield return new WaitForSeconds(animationTime);
        //        List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //        foreach (var room in list)
        //        {
        //            rooms.Add(room);
        //        }
        //    }

        //}

        //for (int i = 0; i < rooms.Count; i++)
        //{
        //    if (i % 2 == 0)
        //    {
        //        //if (rooms[i].width < 10 || rooms[i].height < 10) continue;
        //        if (rooms[i].width > rooms[i].height)
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //            foreach (var room in list)
        //            {
        //                rooms.Add(room);
        //            }
        //        }
        //        else
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //            foreach (var room in list)
        //            {
        //                rooms.Add(room);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //if (rooms[i].width < 10 || rooms[i].height < 10) continue;
        //        if (rooms[i].width < rooms[i].height)
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //            foreach (var room in list)
        //            {
        //                rooms.Add(room);
        //            }
        //        }
        //        else
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //            foreach (var room in list)
        //            {
        //                rooms.Add(room);
        //            }
        //        }
        //    }

        //}

        //int index = 0;

        //for (int i = 0; i < rooms.Count; i++)
        //{
        //    index++;
        //    if (index % 2 == 0)
        //    {
        //        if (rooms[i].width > 10)
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //            foreach (var room in list)
        //            {
        //                rooms.Add(room);
        //            }
        //            roomsUsed.Add(rooms[i]);
        //        }
        //        else if (rooms[i].width < 10 && rooms[i].height > 10)
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> listB = new List<RectInt>(CutterHeight(rooms[i]));
        //            foreach (var room in listB)
        //            {
        //                rooms.Add(room);
        //            }
        //            roomsUsed.Add(rooms[i]);
        //            index++;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //    else
        //    {
        //        if (rooms[i].height > 10)
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //            foreach (var room in list)
        //            {
        //                rooms.Add(room);
        //            }
        //            roomsUsed.Add(rooms[i]);
        //        }
        //        else if (rooms[i].height < 10 && rooms[i].width > 10)
        //        {
        //            yield return new WaitForSeconds(animationTime);
        //            List<RectInt> listB = new List<RectInt>(CutterWidth(rooms[i]));
        //            foreach (var room in listB)
        //            {
        //                rooms.Add(room);
        //            }
        //            roomsUsed.Add(rooms[i]);
        //            index++;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //}

        //for (int i = 0; i < roomsUsed.Count; i++)
        //{
        //    if (roomsUsed[i].width > 10)
        //    {
        //        yield return new WaitForSeconds(animationTime);
        //        List<RectInt> list = new List<RectInt>(CutterWidth(rooms[i]));
        //        foreach (var room in list)
        //        {
        //            roomsUsed.Add(room);
        //        }
        //    }
        //    else if (roomsUsed[i].height > 10)
        //    {
        //        yield return new WaitForSeconds(animationTime);
        //        List<RectInt> list = new List<RectInt>(CutterHeight(rooms[i]));
        //        foreach (var room in list)
        //        {
        //            roomsUsed.Add(room);
        //        }
        //    }

        //    continue;
        //}

    }

    List<RectInt> CutterWidth(RectInt roomCut)
    {
        //Store Original Position of RECT
        int X = roomCut.xMin;
        int Y = roomCut.yMin;
        //Calculate the half to cut
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
