using UnityEngine;

public class WallsGridHolder : MonoBehaviour
{
    public WallsGrid wallsGrid;
    public GameObject roomObject;
    RoomGrid room;
    
    void Awake()
    {
        room = roomObject.GetComponent<RoomGridObject>().room;
        wallsGrid = new WallsGrid(room);

    }

   
}
