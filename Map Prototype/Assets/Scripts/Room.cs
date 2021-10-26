using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType
    {
        Empty,
        Waiting,
        Invalid,
        Done
    }
    public RoomType myType;

    public int xPos, yPos;

    public Room westRoom, eastRoom, northRoom, southRoom;

    public bool playerHere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ShowRoomSymbol()
    {
        switch (myType)
        {
           case RoomType.Empty:
               return "1 ";
           case RoomType.Invalid:
               return "2 ";
           case RoomType.Waiting:
               return "3 ";
           case RoomType.Done:
               return "4 ";
        }
        return "X ";
    }

    public void SetToWaiting()
    {
        //print("debug");
        myType = RoomType.Waiting;
    }
}
