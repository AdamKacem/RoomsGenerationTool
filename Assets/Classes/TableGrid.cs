using UnityEngine;

public class TableGrid : RoomGrid
{
    
    public TableGrid(int gridWidth, int gridHeight, float cellSize, Vector3 center) : base(gridWidth, gridHeight, cellSize, center)
    {

       
    }


    public void ResetOrigin(Vector3 newOrigin)
    {
        origin = newOrigin;
    }
}
