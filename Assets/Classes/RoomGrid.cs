using UnityEngine;
using System.Collections.Generic;
public class RoomGrid
{
    public int gridWidth, gridHeight;
    public float cellSize;
    public Vector3 origin;
    private bool[,] occupiedCell;
    private List<Vector2Int> freeCells;

   


    public RoomGrid(float houseWidth, float houseHeight, float cellSize, Vector3 center)
    {

        this.cellSize = cellSize;

        gridWidth = Mathf.FloorToInt(houseWidth / cellSize);
        gridHeight = Mathf.FloorToInt(houseHeight / cellSize);

        origin = center - new Vector3(gridWidth / 2f, 0, gridHeight / 2f);

        freeCells = new List<Vector2Int>();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                freeCells.Add(new Vector2Int(x, z));
            }
        }

       

        occupiedCell = new bool[gridWidth, gridHeight];
    }


    public Vector3 GetOrigin() {  return origin; }

    public bool IsOccupied(int x, int z)
    {
        return occupiedCell[x, z];
    }

    public void SetOccupied(int x, int z)
    {
        occupiedCell[x, z] = true;
        freeCells.Remove(new Vector2Int(x, z));
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
    

        return new Vector3(
            origin.x + x * cellSize + (cellSize / 2) ,
            0,
            origin.z + z * cellSize + (cellSize / 2) 
        );
    }

    

    public Vector2Int GetRandomFreeCell(SeededRandom rng)
    {
        if (freeCells.Count == 0)
            throw new System.Exception("No free cells left!");

        int randomIndex = rng.Range(0, freeCells.Count);
        return freeCells[randomIndex];
    }

 public bool InBounds(int x, int z)
    {
        return x >= 0 && z >= 0 && x < gridWidth && z < gridHeight;
    }  

}
