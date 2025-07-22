using UnityEngine;

public class wallPlacer : MonoBehaviour
{
  
    public GameObject wall;
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

        
        Vector3 widthPos = new(1f, 0, -1f);
        Vector3 heightPos = new(-1f, 0, 1f);

        widthPos += origin;
        heightPos += origin;

        Vector3 zTranslator = new Vector3(0,0, room.gridHeight + 2);
        Vector3 xTranslator = new Vector3(room.gridWidth + 2,0, 0);

        for (int i = 0; i < ((room.gridWidth+2)/4); i++)
        {
            Instantiate(wall, widthPos, Quaternion.identity ,transform);
            Instantiate(wall, widthPos + zTranslator, rotation180, transform);
            widthPos += new Vector3(4, 0, 0);
    
        }

        int widthMod = (room.gridWidth + 2) % 4;
        
        switch (widthMod)
        {
            case 0:
                break;
            case 1:
                Instantiate(wall25, widthPos - new Vector3(1.5f,0,0), Quaternion.identity, transform);
                Instantiate(wall25, widthPos + zTranslator - new Vector3(1.5f, 0, 0), rotation180, transform);
                break;
            case 2:
                Instantiate(wall5, widthPos - new Vector3(1f, 0, 0), Quaternion.identity, transform);
                Instantiate(wall5, widthPos + zTranslator - new Vector3(1f, 0, 0) , rotation180, transform);
                break;
            default:
                Instantiate(wall75, widthPos - new Vector3(0.5f, 0, 0), Quaternion.identity, transform);
                Instantiate(wall75, widthPos + zTranslator - new Vector3(0.5f, 0, 0), rotation180, transform);
                break;
        }



        for (int i = 0; i < ((room.gridHeight+2)/4); i++)
        {
            
            Instantiate(wall, heightPos, rotation90, transform);
            Instantiate(wall, heightPos+xTranslator, rotationM90, transform);
           
            heightPos += new Vector3(0, 0, 4);
           
        }

        int heightMod = (room.gridHeight + 2) % 4;
        switch (heightMod)
        {
            case 0:
                break;
            case 1:
                Instantiate(wall25, heightPos - new Vector3(0, 0, 1.5f), rotation90, transform);
                Instantiate(wall25, heightPos + xTranslator - new Vector3(0, 0, 1.5f), rotationM90, transform);
                break;
            case 2:
                Instantiate(wall5, heightPos - new Vector3(0, 0, 1.5f), rotation90, transform);
                Instantiate(wall5, heightPos + xTranslator - new Vector3(0, 0, 1.5f), rotationM90, transform);
                break;
            default:
                Instantiate(wall75, heightPos - new Vector3(0, 0, 0.5f), rotation90, transform);
                Instantiate(wall75, heightPos + xTranslator - new Vector3(0, 0, 0.5f), rotationM90, transform);
                break;
        }

        Debug.Log("HeightMod: " + heightMod + " WidthMod: " + widthMod);



        
    }

    void Update()
    {
        
    }
}
