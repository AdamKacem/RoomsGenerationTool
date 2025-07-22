using UnityEngine;

public class gridDrawer : MonoBehaviour
{
    
    RoomGrid roomGrid;
    float cellSize = 1f;

    private void OnDrawGizmos()
    {
        if (roomGrid == null) return;

        float gridWidth = roomGrid.gridWidth;
        float gridHeight = roomGrid.gridHeight;

        Vector3 origin = transform.position - new Vector3(gridWidth / 2f, 0, gridHeight / 2f);

        Gizmos.color = Color.green;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 center = origin + new Vector3(x * cellSize + cellSize / 2f, 0, z * cellSize + cellSize / 2f);
                Vector3 size = new Vector3(cellSize, 0, cellSize);
                Gizmos.DrawWireCube(center, size);
            }
        }
    }

    public void Init(RoomGrid room)
    {
        this.roomGrid = room;        
    }

}
