using UnityEngine;

public class FloorPlacer : MonoBehaviour
{
    RoomGrid room;
    public GameObject floorTile;
    public void Init(RoomGrid room)
    {
        this.room = room;
    }


    public void PlaceFloor()
    {
        //floor tile dimensions
        int FLOORWIDTH =4; //follows x axis  
        int FLOORHEIGHT=4; // follows y axis

        //
        int widthRest = (room.gridWidth+2) % FLOORWIDTH; // how many square (cells of 1 unit) to fill 
        int heightRest = (room.gridHeight+2) % FLOORHEIGHT;
        
        
        

        float xScale = FLOORWIDTH + ((float)widthRest / ((room.gridWidth + 2) / FLOORWIDTH)); //new width of a floor tile (on x axis)
        
        xScale = (float) xScale / FLOORWIDTH; //new scale for the tile

        float zScale = FLOORHEIGHT + ((float)heightRest / ((room.gridHeight + 2) / FLOORHEIGHT));

        zScale = (float) zScale / FLOORHEIGHT;

        

        //calculation of steps for generation
        float xStep = xScale*FLOORWIDTH ;
        float zStep = zScale*FLOORHEIGHT ;

        Vector3 position = room.origin + new Vector3(1, 0.1f ,1); //first tile position
        position.x += (xScale-1)*2; //adjust first position based on the new tile scale
        position.z += (zScale-1)*2;

       Vector3 firstPosition = position;
        



      
       
        for (int i = 0; i < (room.gridHeight + 2)/FLOORWIDTH; i++)
        {

            for (int j = 0; j < (room.gridWidth + 2)/FLOORHEIGHT; j++)
            {
                //place wall
                GameObject floorPiece = Instantiate(floorTile, position, Quaternion.identity, transform);

                //scale wall
                floorPiece.transform.localScale = new Vector3(xScale ,1 ,zScale);

                //next position on same row
                position.x += xStep;




            }

            //move to next row
            position.z += zStep ;
            position.x = firstPosition.x;
        }
    }
}
