using UnityEngine;
using System.Collections.Generic;
public class objectsPlacer : MonoBehaviour
{

    public Placeable Chair; 
    public Placeable TableChair;
    public Placeable Pot;
    public Placeable Pillar;

    public List<Placeable> Placeables;

    public List<Placeable> TableDecoration;
    
    public int a, b;
    
    public RoomGrid room;

    private SeededRandom rng;

    public void Init(RoomGrid room, SeededRandom rng)
    {
        this.room = room;
        this.rng = rng;

        //make the room create freeCellsByType for the types of placeables it will handle
        //(the types that need adding are those who follwo the rule of no adjacents of same type)
        
        foreach (Placeable placeable in Placeables)
        {
            room.InitCellsForType(placeable);
        }

        
        
    }

    public void PlaceObjects()
    {





        /* //Place with console logs
         Debug.Log(PlaceFloorObject(TableChair, room) ? "Placed TableChair": "Failed to place TableChair") ;
        Debug.Log(PlaceFloorObject(Pillar, room) ? "Placed Pillar" : "Failed to place Pillar");
        Debug.Log(PlaceFloorObject(Pillar, room) ? "Placed Pillar" : "Failed to place Pillar");


        Debug.Log(PlaceFloorObject(Pot, room) ? "Placed Pot" : "Failed to place Pot");*/

        PlaceFloorObject(TableChair, room,transform);
        PlaceFloorObject(Pillar, room,transform);
        PlaceFloorObject(Pillar, room, transform);
        PlaceFloorObject(Pot, room,transform);




    }

    bool PlaceFloorObject(Placeable placeable, RoomGrid room, Transform parent,bool decoratingTable=false)
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
                position.y += placeable.yOffset;

                float randomAngle = 0;
                if (placeable.canRotate) { 
                
                    if (decoratingTable)
                    {
                        randomAngle = rng.Range(0.1f, 3.9f) * 90f; //objects on the table take a full random rotation
                    }
                    else
                    {
                        randomAngle = rng.Range(0, 4) * 90f; // floor objects take a random 90 * k rotation
                    }
                } 

                var newObj = Instantiate(obj, position + placeable.offset*room.cellSize, Quaternion.identity, parent);
                
                //if the placeable is a TableChair, we decorate it randomly before rotating it

                if (placeable.type == "TableChair")
                {
                    DecorateTable(newObj);

                    


                }

                //Instantiate(obj, position + placeable.offset, Quaternion.identity, newObj);

                //random rotation
                
                newObj.transform.Rotate(0, randomAngle, 0);

                
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

   
    
    void DecorateTable(GameObject tableObject)
    {
        //initialize a tableGrid with a fixed origin 
       TableGrid tableGrid = new TableGrid(12,7,0.15f, tableObject.transform.position);
        tableGrid.ResetOrigin(tableObject.transform.position + new Vector3(-0.89f,0.89f,-0.21f));
        
       


        for (int i = 0; i < 4; i++)
        {

        
        //choose a random object and prepare the tableGrid for its type(now it wont have adjacents of same type)
        Placeable randomDecoration = TableDecoration[rng.Range(0, TableDecoration.Count)];
        tableGrid.InitCellsForType(randomDecoration);



        //randomly place an object on the tableGrid
        PlaceFloorObject(randomDecoration, tableGrid, tableObject.transform, true);

        }

        

    }



}
