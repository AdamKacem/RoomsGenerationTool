using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewPlaceable", menuName = "PlaceableObjects/NewPlaceable")]
public class Placeable : ScriptableObject
{
    
    public GameObject prefab;

   
    public List<Vector2Int> shape;

    public string type;
    
    public bool canRotate = true;

    public Vector3 offset; //offset in placement (place the pivot directly in the center of the cell? a little above ? a little to the right ?...)



    //rules for procedural generation

    public int radiusForSameObject; //no adjacents of same type within a certain radius

    public bool keepAwayFromCorners = false;

    

    public Placeable(GameObject prefab, List<Vector2Int> shape)
    {
        this.prefab = prefab;
        this.shape = shape;
    }

    

    public bool CanPlaceAt(RoomGrid grid, Vector2Int origin, List<Vector2Int> shape)
    {
        
        foreach (var offset in shape)
        {
            Vector2Int cell = origin + offset;
            
            if (!grid.InBounds(cell.x, cell.y) || grid.IsOccupied(cell.x,cell.y) || !grid.freeCellsByType[type].Contains(cell))
                return false;
        }
        

        return true;
    }

    //mark cells around the placeable depending on the shape it takes
    public void MarkShapeCells(RoomGrid grid, Vector2Int origin, List<Vector2Int> shape)
    {
        foreach (var offset in shape)
        {
            Vector2Int cell = origin + offset;
            grid.SetOccupied(cell.x, cell.y);
        }
    }

    //to prohibit the placement of the same type within the radius
    public void MarkRadiusCells(RoomGrid grid, Vector2Int origin)
    {
        for (int dx = -radiusForSameObject; dx <= radiusForSameObject; dx++)
        {
            for (int dz = -radiusForSameObject; dz <= radiusForSameObject; dz++)
            {
                Vector2Int pos = origin + new Vector2Int(dx, dz);
                if (grid.InBounds(pos.x, pos.y))
                    grid.freeCellsByType[type].Remove(pos);
            }
        }
    }




}

