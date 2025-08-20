using UnityEngine;
using System.Collections.Generic;
using System;


public class WallsGrid 
{

   
    
    private Dictionary<int, List<int>> freeWallSlots;

    int WALLWIDTH = 4;

    RoomGrid room;
    private SeededRandom rng;

    public Vector2Int openWall;

    
    public int openBottom;
    public int openLeft;
        public int openRight;
    public int openTop;

    public WallsGrid(RoomGrid room, int openTop, int openBottom, int openRight, int openLeft, SeededRandom rng)
    {
        this.room = room;
        this.rng = rng;


        this.openBottom = openBottom;
        this.openLeft = openLeft;
        this.openRight = openRight;
        this.openTop = openTop;

        freeWallSlots = new Dictionary<int, List<int>>();
        int limit;
        for (int wallNum = 0; wallNum < 4; wallNum++)
        {
            if (wallNum == 0 || wallNum == 1) limit = room.gridHeight;
            else limit = room.gridWidth;

            freeWallSlots.Add(wallNum, new List<int>());


            for (int slot = 0; slot < limit; slot++)
            {
                freeWallSlots[wallNum].Add(slot);

            }
        }


        

        RemoveOpenWalls(); //occupy the slots of the open wall with a door (shouldn't be decorated)

        //LogFreeSlots();
    }

    

    public void RemoveOpenWalls()
    {
        for (int wallNum = 0; wallNum < 4; wallNum++)
        {
            switch (wallNum)
            {
                case 0:
                    RemoveOpenWall(new Vector2Int(0,openLeft));
                    break;
                case 1:
                    RemoveOpenWall(new Vector2Int(1,openRight));
                    break;
                case 2:
                    RemoveOpenWall(new Vector2Int(2,openBottom));
                    break;
                case 3:
                    RemoveOpenWall(new Vector2Int(3, openTop));
                    break;


                default:

                    break;

            }
            
        }

    }



    public void RemoveOpenWall(Vector2Int openWall)
    {
        int side = openWall.x; //which side has the open wall
        int wall = openWall.y; //which wall is open (first, second ..)

        int dimension = ( ((side == 0) || (side == 1)) ? room.gridHeight : room.gridWidth) +2;

        //Debug.Log(openWall);

        

        if ( (dimension / WALLWIDTH - 1) < wall) //because wall starts from 0
        {
            Debug.Log($"Dimension: {dimension}, removable walls: from 0 to  {(dimension / WALLWIDTH) - 1}, wall to remove: {wall}");
            Debug.Log("Can't remove this wall from freeDecoratingSlots");
            return;
        }

        int firstSlot = WALLWIDTH * wall - 1;

        for (int slot = firstSlot; slot < firstSlot + WALLWIDTH; slot++)
        {
            freeWallSlots[side].Remove(slot);
        }


        
    }
    public void LogFreeSlots()
    {
        if (freeWallSlots == null || freeWallSlots.Count == 0)
        {
            Debug.Log("freeWallSlots is empty or not initialized!");
            return;
        }

        Debug.Log("=== Free Wall Slots ===");
        foreach (var wall in freeWallSlots)
        {
            string slots = string.Join(", ", wall.Value);
            Debug.Log($"Side {wall.Key}: [{slots}]");
        }
        Debug.Log("=======================");
    }





    int RandomFreeSlot(int wallNumber, int padding, bool priority=false) //with occupation
    {
        if (freeWallSlots[wallNumber].Count == 0)
            return -1;
        
        int randomIndex = priority 
            ? rng.Range(0, Math.Max(freeWallSlots[wallNumber].Count-padding,0)) 
            : rng.Range(0, freeWallSlots[wallNumber].Count);

        int slot = freeWallSlots[wallNumber][randomIndex];

        if (!IsFreeSpace(wallNumber, slot, padding)) {
            
            return -1;
                }

        //occupy padding slots
        for (int i = 0; i <= padding;i++)
        {
            OccupySlot(wallNumber, slot+i);
        }
        
        return slot;
    }

    bool IsFreeSpace(int wallNumber, int slot ,int padding)
    {
        for (int i = 0;i <= padding; i++)
        {
            if (!freeWallSlots[wallNumber].Contains(slot + i)) return false ;
        
        }
        return true;
    }

    void OccupySlot(int wallNumber, int slot)
    {
        
        freeWallSlots[wallNumber].Remove(slot);
    }

    


    public void DecorateWall(WallPlaceable placeable, Transform transform , int Wall=-1,bool priority=false)
    {
        int wallNumber = rng.Range(0, 4);
        
        if (Wall != -1)
            wallNumber = Wall;
        
        float x, y, z, angle = 0;
        Vector3 position;

        y = rng.Range(2.7f+placeable.yOffset, 3.7f);
        
        switch (wallNumber)
        {
            case 0:
                x = 0;
                z = RandomFreeSlot(0, placeable.padding,priority);
                if (z == -1) return;
                position = new(x - 0.5f, y, z + placeable.offset);
                angle = 90f;
                break;
            case 1:
                x = room.gridWidth;
                z = RandomFreeSlot(1, placeable.padding,priority);
                if (z == -1) return;
                position = new(x + 0.5f, y, z + placeable.offset);
                angle = -90f;
                break;
            case 2:
                z = 0;
                x = RandomFreeSlot(2, placeable.padding, priority);
                if (x == -1) return;
                position = new(x + placeable.offset, y, z - 0.5f);

                break;

            default:
                z = room.gridHeight;
                x = RandomFreeSlot(3, placeable.padding, priority);
                if (x == -1) return;
                position = new(x + placeable.offset, y, z + 0.5f);
                angle = 180f;
                break;

        }

        UnityEngine.Object.Instantiate(placeable.prefab, position + room.origin, Quaternion.Euler(0, angle, 0), transform);
    }


}
