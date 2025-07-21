using UnityEngine;

public class WallDecorator : MonoBehaviour
{
    public GameObject banner;
    public GameObject torch;

    RoomGridObject RoomGridHolder;

    public GameObject roomGridObject;
   RoomGrid room;

    WallsGrid wallsGrid;
   
    void Start()
    {

        room = roomGridObject.GetComponent<RoomGridObject>().room;
        wallsGrid = new WallsGrid(room);

        int wallNumber = Random.Range(0, 4);

        for (int i = 0; i < 4; i++) { 
        wallsGrid.DecorateWall(banner);
        wallsGrid.DecorateWall(torch,3);
        }


    }

   
    
}
