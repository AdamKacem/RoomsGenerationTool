using UnityEngine;

public class WallDecorator : MonoBehaviour
{
    public GameObject banner;
    public GameObject torch;

    

    
    RoomGrid room;

    WallsGrid wallsGrid;
   
    public void DecorateWalls()
    {

        
        
        
        

        int wallNumber = Random.Range(0, 4);

        for (int i = 0; i < 4; i++) { 
        wallsGrid.DecorateWall(banner, transform);
        wallsGrid.DecorateWall(torch, transform);
        }


    }

   public void Init(RoomGrid room)
    {
        this.room = room;
        wallsGrid = new WallsGrid(room);
    }
    
}
