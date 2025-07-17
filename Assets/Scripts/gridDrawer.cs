using UnityEngine;

public class gridDrawer : MonoBehaviour
{
    public GameObject RoomObject;

    void OnDrawGizmos()
    {
        if (RoomObject == null) return;

        float cellSize = 1.0f;

        // Rename this to avoid conflict
        RoomGridObject roomGrid = RoomObject.GetComponent<RoomGridObject>();
        if (roomGrid == null) return;

        float houseWidth = roomGrid.houseWidth;
        float houseHeight = roomGrid.houseHeight;

        int gridWidth = Mathf.FloorToInt(houseWidth / cellSize);
        int gridHeight = Mathf.FloorToInt(houseHeight / cellSize);

        Vector3 origin = RoomObject.transform.position - new Vector3(houseWidth / 2, 0, houseHeight / 2);

        Gizmos.color = Color.green;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 center = origin + new Vector3(x * cellSize + cellSize / 2, 0, z * cellSize + cellSize / 2);
                Vector3 size = new Vector3(cellSize, 0, cellSize);
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
}
