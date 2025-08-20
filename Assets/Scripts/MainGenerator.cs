using UnityEngine;

public class MainGenerator : MonoBehaviour
{
    public GameObject roomGeneratorObject;
    public float x, y, z;

    public int seed;
    public int widthTest, heightTest;

    private Vector3 position;

    public int minSize;


    public bool allowBigRooms;




    public int openTop;
    public int openBottom;
    public int openLeft;
    public int openRight;

    public Vector2Int openWall = new Vector2Int(1, 2);
    void Start()
    {
/*
       
        position = new(x,y,z);

        SeededRandom rng = new SeededRandom(292);
        GenerateOneRoom(widthTest,heightTest, openTop, openBottom, openRight, openLeft, position, rng);*/

 
        RectInt area = new RectInt(0,0,widthTest,heightTest);

        SeededRandom rng = new SeededRandom(seed);

        BSPNode root = new BSPNode(area);


        //root.topDoor = 0;

        root.SplitWithDoors(minSize , rng, allowBigRooms);

        root.CreateRooms(this, rng);
        
    }

    //with seed
    public void GenerateOneRoom(int width, int height, int openTop, int openBot, int openRight, int openLeft, Vector3 position, SeededRandom rng)
    {
       
        GameObject newRoom = Instantiate(roomGeneratorObject, position, Quaternion.identity);
        RoomGenerator roomGenerator = newRoom.GetComponent<RoomGenerator>();
        //initialize roomGenerator
        roomGenerator.Init(width,height, openTop, openBot, openRight, openLeft, rng);

        //generate the rooom
        roomGenerator.GenerateRoom();
    }


    //without seed
    public void GenerateOneRoom(int width, int height, int openTop, int openBot, int openRight, int openLeft, Vector3 position)
    {
        int seed = Random.Range(0, 500);
        SeededRandom rng = new SeededRandom(seed);
        //Debug.Log("Last room was created with seed: " + seed); 
        GameObject newRoom = Instantiate(roomGeneratorObject, position, Quaternion.identity);
        RoomGenerator roomGenerator = newRoom.GetComponent<RoomGenerator>();
        //initialize roomGenerator
        roomGenerator.Init(width, height, openTop, openBot, openRight, openLeft, rng);

        //generate the rooom
        roomGenerator.GenerateRoom();
        Debug.Log($"Generated a room at position: {position} with seed = {seed}");
    }
}
