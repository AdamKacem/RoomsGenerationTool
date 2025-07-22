using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    

    public objectsPlacer objectsPlacer;
    public wallPlacer wallPlacer;
    public WallDecorator wallDecorator;
    public gridDrawer gridDrawer;
    RoomGrid room;

    
   
    public void Init(float width, float height)
    {
        room = new RoomGrid(width, height,1f,transform.position);
        
        
        wallPlacer.Init(room);
        objectsPlacer.Init(room);
        wallDecorator.Init(room);
        gridDrawer.Init(room);

    }

    public void GenerateRoom()
    {
        wallPlacer.PlaceWalls();
        objectsPlacer.PlaceObjects();
        wallDecorator.DecorateWalls();
        
    }
}
