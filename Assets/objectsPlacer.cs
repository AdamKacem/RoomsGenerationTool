using UnityEngine;

public class objectsPlacer : MonoBehaviour
{
    
    public GameObject BedObject;

    public GameObject roomObject;

    public int a, b;
    
    RoomGrid room;
    void Start()
    {
       
        room = roomObject.GetComponent<RoomGridObject>().room;
        


        PlaceObject(BedObject, room);

        //PlaceObjectManually(BedObject, room ,a,b);

    }

    void PlaceObject(GameObject obj, RoomGrid room)
    {
        Vector2Int coords = room.PlaceInRandomFreeCell();
        int x = coords.x;
        int z = coords.y;
       
        Vector3 position = room.GetWorldPosition(x, z);
        
        float randomAngle = Random.value<0.5f ? 90f : 0f;

        Instantiate(obj, position, Quaternion.Euler(0,randomAngle,0));

    }

    void PlaceObjectManually(GameObject obj, RoomGrid room,int x,int z)
    {


        Vector3 position = room.GetWorldPosition(x, z);

        float randomAngle = Random.value < 0.5f ? 90f : 0f;

        Instantiate(obj, position, Quaternion.Euler(0, randomAngle, 0));


    }

    
    





}
