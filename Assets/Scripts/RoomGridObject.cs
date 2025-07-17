using UnityEngine;

public class RoomGridObject : MonoBehaviour
{
    public RoomGrid room;
    public float houseWidth, houseHeight;
    RoomGridObject RoomInstance;
    public float cellSize = 1.0f;
    public GameObject RoomObject;
    void Awake()
    {
        RoomInstance = RoomObject.GetComponent<RoomGridObject>();

        houseWidth = RoomInstance.houseWidth;
        houseHeight = RoomInstance.houseHeight;

        room = new RoomGrid(houseWidth, houseHeight, cellSize, RoomInstance.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
