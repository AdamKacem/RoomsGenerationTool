using UnityEngine;

public class objectsPlacer : MonoBehaviour
{

    public Placeable Chair; 
    public Placeable TableChair;
    public Placeable Pot;
    public Placeable Pillar;

    public GameObject roomObject;
    

    public int a, b;
    
    RoomGrid room;
    void Start()
    {
       
        room = roomObject.GetComponent<RoomGridObject>().room;
        


        //PlaceFloorObject(Chair, room);
        PlaceFloorObject(TableChair, room);
        PlaceFloorObject(Pillar, room);
        PlaceFloorObject(Pillar, room);
        
        PlaceFloorObject(Pot, room);
        //PlaceFloorObject(Chair, room);
        

        PlaceObjectManually(Chair, room ,a,b);

    }

    bool PlaceFloorObject(Placeable placeable, RoomGrid room)
    {
        GameObject obj = placeable.prefab;

        
        //try 100 times to find a place for the object
        
        for (int attempt = 0; attempt<100;attempt++)
        {
            Vector2Int coords = room.GetRandomFreeCell();
            if (placeable.CanPlaceAt(room, coords, placeable.shape))
            {
                
                placeable.MarkCells(room, coords, placeable.shape);
                int x = coords.x;
                int z = coords.y;

                Vector3 position = room.GetWorldPosition(x, z);
                float randomAngle = 0;
                if (placeable.canRotate) randomAngle = Random.Range(0, 3) * 90f;
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

    
    void PlaceWallObject()
    {



    }




}
