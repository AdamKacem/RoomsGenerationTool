using UnityEngine;

public class MainGenerator : MonoBehaviour
{
    public GameObject roomGeneratorObject;
    public float x, y, z;

    public int seed;
    public int widthTest, heightTest;

    private Vector3 position;

   

    void Start()
    {

        //SeededRandom rng = new SeededRandom(seed);
        position = new(x,y,z);
        /**/
        for (int i = 1; i < 6; i++)
        {
            GenerateOneRoom(widthTest, heightTest,0 ,position, new SeededRandom(seed));
            position = new(x+15*i,y,z);
    
        }

        GenerateOneRoom(widthTest,heightTest,0,position);

        
    }

    //with seed
    void GenerateOneRoom(int width, int height, int openWall, Vector3 position, SeededRandom rng)
    {
        //Debug.Log("Created a room with pre-specified seed.");
        GameObject newRoom = Instantiate(roomGeneratorObject, position, Quaternion.identity);
        RoomGenerator roomGenerator = newRoom.GetComponent<RoomGenerator>();
        //initialize roomGenerator
        roomGenerator.Init(width,height, openWall, rng);

        //generate the rooom
        roomGenerator.GenerateRoom();
    }


    //without seed
    void GenerateOneRoom(int width, int height, int openWall, Vector3 position)
    {
        int seed = Random.Range(0, 500);
        SeededRandom rng = new SeededRandom(seed);
        //Debug.Log("Last room was created with seed: " + seed); 
        GameObject newRoom = Instantiate(roomGeneratorObject, position, Quaternion.identity);
        RoomGenerator roomGenerator = newRoom.GetComponent<RoomGenerator>();
        //initialize roomGenerator
        roomGenerator.Init(width, height, openWall, rng);

        //generate the rooom
        roomGenerator.GenerateRoom();
    }
}
