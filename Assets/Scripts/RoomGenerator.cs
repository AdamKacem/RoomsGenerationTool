using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public SeededRandom rng;
    public objectsPlacer objectsPlacer;
    public wallPlacer wallPlacer;
    public WallDecorator wallDecorator;
    public gridDrawer gridDrawer;
    RoomGrid room;

    
   
    public void Init(float width, float height, int openWall, SeededRandom rng)
    {
        room = new RoomGrid(width, height,1f,transform.position);
        
        //static
        wallPlacer.Init(room, openWall);
        gridDrawer.Init(room);

        //need randomness
        objectsPlacer.Init(room, rng);
        wallDecorator.Init(room, rng);
        
        

    }

    public void GenerateRoom()
    {
        wallPlacer.PlaceWalls();
        objectsPlacer.PlaceObjects();
        wallDecorator.DecorateWalls();
        
    }
}
