using UnityEngine;
using System.Collections.Generic;
public class objectsPlacer : MonoBehaviour
{

    public Placeable Chair; 
    public Placeable TableChair;
    public Placeable Pot;
    public Placeable Pillar;

    public List<Placeable> Placeables;


    public int a, b;
    
    public RoomGrid room;

    private SeededRandom rng;

    public void Init(RoomGrid room, SeededRandom rng)
    {
        this.room = room;
        this.rng = rng;

        //make the room create freeCellsByType for the types of placeables it will handle
        foreach (Placeable placeable in Placeables)
        {
            room.InitCellsForType(placeable);
        }

        //apply the rules 
        
    }

    public void PlaceObjects()
    {





        /* //Place with console logs
         Debug.Log(PlaceFloorObject(TableChair, room) ? "Placed TableChair": "Failed to place TableChair") ;
        Debug.Log(PlaceFloorObject(Pillar, room) ? "Placed Pillar" : "Failed to place Pillar");
        Debug.Log(PlaceFloorObject(Pillar, room) ? "Placed Pillar" : "Failed to place Pillar");


        Debug.Log(PlaceFloorObject(Pot, room) ? "Placed Pot" : "Failed to place Pot");*/

        PlaceFloorObject(TableChair, room);
        PlaceFloorObject(Pillar, room);
        PlaceFloorObject(Pillar, room);
        PlaceFloorObject(Pot, room);




    }

    bool PlaceFloorObject(Placeable placeable, RoomGrid room)
    {
        GameObject obj = placeable.prefab;

        
        //try 100 times to find a place for the object
        
        for (int attempt = 0; attempt<100;attempt++)
        {
            Vector2Int coords = room.GetRandomFreeCell(placeable, this.rng);
            int x = coords.x;
            int z = coords.y;
            if(x==-1)return false;

            if (placeable.CanPlaceAt(room, coords, placeable.shape))
            {
                
                placeable.MarkShapeCells(room, coords, placeable.shape);//mark grid occupation for all the other objects
                placeable.MarkRadiusCells(room, coords);//make the grid occupation and radius for objects with same type

                Vector3 position = room.GetWorldPosition(x, z);
                float randomAngle = 0;
                if (placeable.canRotate) randomAngle = rng.Range(0,3) * 90f;
                Instantiate(obj, position+placeable.offset, Quaternion.Euler(0, randomAngle, 0) ,transform);
                return true;
               
            }


        }
    
        return false;
    }

    void PlaceObjectManually(Placeable placeable, RoomGrid room,int x,int z)
    {


        Vector3 position = room.GetWorldPosition(x, z);

        float randomAngle = Random.value < 0.5f ? 90f : 0f;

        Instantiate(placeable.prefab, position, Quaternion.Euler(0, randomAngle, 0), transform);


    }

   
    




}
