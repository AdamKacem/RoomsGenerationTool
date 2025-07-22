using UnityEngine;

public class MainGenerator : MonoBehaviour
{
    public GameObject roomGeneratorObject;
    public float x, y, z;
    
    
    public int widthTest, heightTest;

    private Vector3 position;

   

    void Start()
    {
        position = new(x,y,z);

        for (int i = 0; i < 5; i++)
        {
            GenerateOneRoom(widthTest, heightTest ,position);
            position = new(x+15*i,y,z);
    
        }

        
    }

    
    void GenerateOneRoom(int width, int height,Vector3 position)
    {
        GameObject newRoom = Instantiate(roomGeneratorObject, position, Quaternion.identity);
        RoomGenerator roomGenerator = newRoom.GetComponent<RoomGenerator>();
        roomGenerator.Init(width,height);
        roomGenerator.GenerateRoom();
    }
}
