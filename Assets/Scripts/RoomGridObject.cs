using UnityEngine;

public class RoomGridObject : MonoBehaviour
{
    public RoomGrid room;
    public float houseWidth, houseHeight;
   
    public float cellSize = 1.0f;
    
    void Awake()
    {
        

        

        room = new RoomGrid(houseWidth, houseHeight, cellSize, transform.position);
    }

    
}
