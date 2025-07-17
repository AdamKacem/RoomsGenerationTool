using UnityEngine;

public class wallPlacer : MonoBehaviour
{
  
    public GameObject wall;
    public GameObject CenterObject;
    public GameObject roomObject;
    public RoomGrid room;


    public GameObject wall25, wall5, wall75;

   

    
    void Start()
    {
        room = roomObject.GetComponent<RoomGridObject>().room;

        


        Vector3 origin = room.origin;
        Quaternion rotation90 = Quaternion.Euler(0f, 90f, 0f);
        Quaternion rotationM90 = Quaternion.Euler(0f, -90f, 0f);
        Quaternion rotation180 = Quaternion.Euler(0f, 180f, 0f);

        //instantiating walls

        //GameObject width1 =  Instantiate(wall,origin,rotation180);
        Vector3 widthPos = new(1f, 0, -1f);
        Vector3 heightPos = new(-1f, 0, 1f);

        widthPos += origin;
        heightPos += origin;
        Vector3 zTranslator = new Vector3(0,0, room.gridHeight + 2);
        Vector3 xTranslator = new Vector3(room.gridWidth + 2,0, 0);

        for (int i = 0; i < ((room.gridWidth+2)/4); i++)
        {
            Instantiate(wall, widthPos, Quaternion.identity);
            Instantiate(wall, widthPos + zTranslator, rotation180);
            widthPos += new Vector3(4, 0, 0);
    
        }

        int widthMod = (room.gridWidth + 2) % 4;
        switch (widthMod)
        {
            case 1:
                Instantiate(wall25, widthPos - new Vector3(1.5f,0,0), Quaternion.identity);
                Instantiate(wall25, widthPos + zTranslator - new Vector3(1.5f, 0, 0), rotation180);
                break;
            case 2:
                Instantiate(wall5, widthPos - new Vector3(1f, 0, 0), Quaternion.identity);
                Instantiate(wall5, widthPos + zTranslator - new Vector3(1f, 0, 0) , rotation180);
                break;
            default:
                Instantiate(wall75, widthPos - new Vector3(0.5f, 0, 0), Quaternion.identity);
                Instantiate(wall75, widthPos + zTranslator - new Vector3(0.5f, 0, 0), rotation180);
                break;
        }



        for (int i = 0; i < ((room.gridHeight+2)/4); i++)
        {
            
            Instantiate(wall, heightPos, rotation90);
            Instantiate(wall, heightPos+xTranslator, rotationM90);
           
            heightPos += new Vector3(0, 0, 4);
           
        }

        int heightMod = (room.gridHeight + 2) % 4;
        switch (widthMod)
        {
            case 1:
                Instantiate(wall25, heightPos - new Vector3(0, 0, 1.5f), rotation90);
                Instantiate(wall25, heightPos + xTranslator - new Vector3(0, 0, 1.5f), rotationM90);
                break;
            case 2:
                Instantiate(wall5, heightPos - new Vector3(0, 0, 1.5f), rotation90);
                Instantiate(wall5, heightPos + xTranslator - new Vector3(0, 0, 1.5f), rotationM90);
                break;
            default:
                Instantiate(wall75, heightPos - new Vector3(0, 0, 0.5f), rotation90);
                Instantiate(wall75, heightPos + xTranslator - new Vector3(0, 0, 0.5f), rotationM90);
                break;
        }




        //GameObject height1 =  
        //GameObject height2 =  Instantiate(wall, origin , rotation90);





        //dimensions for each wall
        /*
    Vector3 widthWallDimensions = new Vector3(houseWidth, WALLHEIGHT, WALLDEPTH);
    width1.transform.localScale = widthWallDimensions;
    width2.transform.localScale = widthWallDimensions;

    Vector3 heightWallDimensions = new Vector3(houseHeight, WALLHEIGHT, WALLDEPTH);
    height1.transform.localScale = heightWallDimensions;
    height2.transform.localScale = heightWallDimensions;

*/

        //position of each wall:
        /*
    width1.transform.position = new Vector3(.x,.y ,.z+ houseHeight/2 + 0.5f);
    width2.transform.position = new Vector3(.x, .y , .z- houseHeight / 2 - 1.5f);

    height1.transform.position = new Vector3(.x + houseWidth/2+1, .y, .z);
    height2.transform.position = new Vector3(.x - houseWidth / 2-1, .y, .z);*/


        //el house area heya:
        //centerX +- houseWidth/2
        //centerZ +- houseHeight/2
    }

    void Update()
    {
        
    }
}
