using UnityEngine;

public class RoomGrid
{
    public int gridWidth, gridHeight;
    public float cellSize;
    public Vector3 origin;
    private bool[,] occupied;

    public RoomGrid(float houseWidth, float houseHeight, float cellSize, Vector3 center)
    {
        this.cellSize = cellSize;

        gridWidth = Mathf.FloorToInt(houseWidth / cellSize);
        gridHeight = Mathf.FloorToInt(houseHeight / cellSize);

        origin = center - new Vector3(gridWidth / 2f, 0, gridHeight / 2f);
        
        occupied = new bool[gridWidth, gridHeight];
    }


    public Vector3 GetOrigin() {  return origin; }

    public bool IsOccupied(int x, int z)
    {
        return occupied[x, z];
    }

    public void SetOccupied(int x, int z)
    {
        occupied[x, z] = true;
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        int restW = (gridWidth + 1) % 2;
        int restH = (gridHeight + 1) % 2;
        Debug.Log("origin: " + origin);
        //Debug.Log("origin: " + origin);

        return new Vector3(
            origin.x + x * cellSize + (cellSize / 2) ,
            0,
            origin.z + z * cellSize + (cellSize / 2) 
        );
    }

    public Vector2Int PlaceInRandomFreeCell(int margin = 1)
    {
        int x, z;
        do
        {
            x = Random.Range(margin, gridWidth - margin);
            z = Random.Range(margin, gridHeight - margin);
        } while (occupied[x, z]);
        this.occupied[x, z] = true;
        return new Vector2Int(x, z);
    }

   

}
