using UnityEngine;
using System.Collections.Generic;
public class RoomGrid
{
    public int gridWidth, gridHeight;
    public float cellSize;
    public Vector3 origin;
    protected bool[,] occupiedCell;
    protected List<Vector2Int> freeCells;
    public Dictionary<string, HashSet<Vector2Int>> freeCellsByType = new();
    //in the future make this better: (instead of setting types manually make them generate themselves from the placeables in use)



    public RoomGrid(int gridWidth, int gridHeight, float cellSize, Vector3 center)
    {

        this.cellSize = cellSize;

        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;

        origin = center - new Vector3(gridWidth / 2f, 0, gridHeight / 2f);
        

        //free cells for all type of objects
        InitFreeCells();
       

        occupiedCell = new bool[gridWidth, gridHeight];
    }


  

    public void InitFreeCells()
    {
        freeCells = new List<Vector2Int>();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                freeCells.Add(new Vector2Int(x, z));
            }
        }
    }

    public void InitCellsForType(Placeable placeable) 
    {

        if (!freeCellsByType.ContainsKey(placeable.type))
        {
            freeCellsByType[placeable.type] = new HashSet<Vector2Int>(freeCells);
            //remove corners if necessary
            if (placeable.keepAwayFromCorners)
            {
                //Debug.Log("Removed Corners for: "+placeable.type);
                //remove corners

            }
        }

        

        
        
    
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
            origin.y,
            origin.z + z * cellSize + (cellSize / 2) 
        );
    }

    

    public Vector2Int GetRandomFreeCell(Placeable placeable, SeededRandom rng )
    {
        if (freeCellsByType[placeable.type].Count == 0)
        {
            //Debug.Log("No free cells left!");
            return new Vector2Int(-1,-1);
        }
            

        int randomIndex = rng.Range(0, freeCellsByType[placeable.type].Count);
        var availableCells = new List<Vector2Int>(freeCellsByType[placeable.type]);

        return availableCells[randomIndex];
    }

 public bool InBounds(int x, int z)
    {
        return x >= 0 && z >= 0 && x < gridWidth && z < gridHeight;
    }  

}
