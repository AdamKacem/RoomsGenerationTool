using UnityEngine;

public class TableGridDrawer : MonoBehaviour
{
    public TableGrid tableGrid;
    public float cellSize;

    public float gridWidth, gridHeight;

    public GameObject Origin;

    Vector3 center;

    public void OnDrawGizmos()
    {
        if (Origin == null) return;

        center = Origin.transform.position;
        //Debug.Log(center);
        

        

        Vector3 origin = center ;

        Gizmos.color = Color.blue;
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

    
}
