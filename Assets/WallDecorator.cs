using UnityEngine;

public class WallDecorator : MonoBehaviour
{
    public GameObject banner;
    public GameObject torch;

    

    
    RoomGrid room;
    private SeededRandom rng;

    WallsGrid wallsGrid;

    public void Init(RoomGrid room, SeededRandom rng)
    {
        this.room = room;
        this.rng = rng;
        wallsGrid = new WallsGrid(room, rng);
        
    }

    public void DecorateWalls()
    {

        
        
        
        

        int wallNumber = Random.Range(0, 4);

        for (int i = 0; i < 4; i++) { 
        wallsGrid.DecorateWall(banner, transform);
        wallsGrid.DecorateWall(torch, transform);
        }


    }

   
    
}
