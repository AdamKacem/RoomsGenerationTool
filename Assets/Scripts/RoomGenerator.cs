using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public SeededRandom rng;
    public objectsPlacer objectsPlacer;
    public wallPlacer wallPlacer;
    public WallDecorator wallDecorator;
    public gridDrawer gridDrawer;
    public FloorPlacer floorPlacer;
    RoomGrid room;

    
   
    public void Init(int width, int height, int openTop,int openBot,int openRight,int openLeft, SeededRandom rng)
    {
        room = new RoomGrid(width, height,1f,transform.position);
        
        //static
        wallPlacer.Init(room, openTop, openBot, openRight, openLeft);
        gridDrawer.Init(room);
        floorPlacer.Init(room);

        //need randomness
        objectsPlacer.Init(room, rng);
        wallDecorator.Init(room, openTop, openBot, openRight, openLeft, rng);
        

        

    }

    public void GenerateRoom()
    {
        wallPlacer.PlaceWalls();
        objectsPlacer.PlaceObjects();
        wallDecorator.DecorateWalls();
        floorPlacer.PlaceFloor();
        
    }
}
