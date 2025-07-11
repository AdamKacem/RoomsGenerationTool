using UnityEngine;

public class wallPlacer : MonoBehaviour
{
  
    public GameObject wall;
    public GameObject CenterObject;

   float houseWidth, houseHeight;

    const float WALLHEIGHT = 9;
    const float WALLDEPTH = 2;

    float originX, originZ;
    void Start()
    {
        CenterClass Center = CenterObject.GetComponent<CenterClass>();
        houseWidth = Center.houseWidth;
        houseHeight = Center.houseHeight;

        if (houseWidth < 10 || houseHeight < 10)
        {
            Debug.Log("Can't place walls ! (minimum size is 10x10)");
            return;
        }

    Quaternion rotation90 = Quaternion.Euler(0f, 90f, 0f);
    Quaternion rotationM90 = Quaternion.Euler(0f, -90f, 0f);
    Quaternion rotation180 = Quaternion.Euler(0f, 180f, 0f);

        //instantiating walls
        GameObject width1 =  Instantiate(wall,Center.transform.position,rotation180);
    GameObject width2 =  Instantiate(wall,Center.transform.position, Quaternion.identity);

    GameObject height1 =  Instantiate(wall,Center.transform.position, rotationM90);
    GameObject height2 =  Instantiate(wall,Center.transform.position, rotation90);


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
        
    width1.transform.position = new Vector3(Center.transform.position.x,Center.transform.position.y ,Center.transform.position.z+ houseHeight/2 + 0.5f);
    width2.transform.position = new Vector3(Center.transform.position.x, Center.transform.position.y , Center.transform.position.z- houseHeight / 2 - 1.5f);

    height1.transform.position = new Vector3(Center.transform.position.x + houseWidth/2+1, Center.transform.position.y, Center.transform.position.z);
    height2.transform.position = new Vector3(Center.transform.position.x - houseWidth / 2-1, Center.transform.position.y, Center.transform.position.z);


        //el house area heya:
        //centerX +- houseWidth/2
        //centerZ +- houseHeight/2
    }

    void Update()
    {
        
    }
}
