using UnityEngine;
using System.Collections.Generic;



public class WallsGrid 
{

    private Dictionary<int, HashSet<int>> occupiedWallSlots;
    
    private Dictionary<int, List<int>> freeWallSlots;

    RoomGrid room;
    
    public WallsGrid(RoomGrid room) {
        this.room = room;
        freeWallSlots = new Dictionary<int, List<int>>();
        int limit;
        for (int wallNum = 0; wallNum < 4; wallNum++)
        {
            if(wallNum%2==0)limit = room.gridHeight;
            else limit = room.gridWidth;

            freeWallSlots.Add(wallNum, new List<int>());

            
            for (int slot = 0; slot < limit; slot++)
            {
                freeWallSlots[wallNum].Add(slot);
            }
        }


        occupiedWallSlots = new Dictionary<int, HashSet<int>>();

        for (int i = 0; i < 4; i++)
        {
            occupiedWallSlots.Add(i, new HashSet<int>());
        }
    }

    int RandomFreeSlot(int wallNumber) //with occupation
    {
        if (freeWallSlots[wallNumber].Count == 0)
            return -1;

        int randomIndex = Random.Range(0, freeWallSlots[wallNumber].Count);
        int slot = freeWallSlots[wallNumber][randomIndex];
        OccupySlot(wallNumber, slot);
        return slot;
    }

    void OccupySlot(int wallNumber, int slot)
    {
        occupiedWallSlots[wallNumber].Add(slot);
        freeWallSlots[wallNumber].Remove(slot);
    }

    public void DecorateWall(GameObject obj, int Wall=-1)
    {
        int wallNumber = Random.Range(0, 4);
        Debug.Log(wallNumber);
        if (Wall != -1)
            wallNumber = Wall;
        float x, y, z, angle = 0;
        Vector3 position;

        y = Random.value + 2.7f;
        switch (wallNumber)
        {
            case 0:
                x = 0;
                z = RandomFreeSlot(0);
                position = new(x - 0.5f, y, z);
                angle = 90f;
                break;
            case 1:
                x = room.gridWidth;
                z = RandomFreeSlot(1);
                position = new(x + 0.5f, y, z);
                angle = -90f;
                break;
            case 2:
                z = 0;
                x = RandomFreeSlot(2);
                position = new(x, y, z - 0.5f);

                break;

            default:
                z = room.gridHeight;
                x = RandomFreeSlot(3);
                position = new(x, y, z + 0.5f);
                angle = 180f;
                break;

        }

        Object.Instantiate(obj, position + room.origin, Quaternion.Euler(0, angle, 0));
    }


}
