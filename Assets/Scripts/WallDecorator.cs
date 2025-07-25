using UnityEngine;
using System.Collections.Generic;
public class WallDecorator : MonoBehaviour
{
    //public GameObject banner;
    //public GameObject torch;
    public List<WallPlaceable> wallPlaceables;

    
    public WallPlaceable torch;
    
    RoomGrid room;
    private SeededRandom rng;

    WallPlaceable randomWallPlaceable;
    WallsGrid wallsGrid;

    public void Init(RoomGrid room, SeededRandom rng)
    {
        this.room = room;
        this.rng = rng;
        wallsGrid = new WallsGrid(room, rng);
        
    }

    public void DecorateWalls()
    {

        //place necessary random torches on each wall with priority=true
        
        for (int i = 0; i < 4; i++)
        {
            wallsGrid.DecorateWall(torch, transform, i, true);
        }   

        //place random decorations

        int wallNumber = Random.Range(0, 4);

        for (int i = 0; i < 4; i++) {
            randomWallPlaceable = wallPlaceables[rng.Range(0,wallPlaceables.Count)];
            wallsGrid.DecorateWall(randomWallPlaceable, transform, -1, true);
        
        }
        wallsGrid.LogFreeSlots();

    }

   
    
}
