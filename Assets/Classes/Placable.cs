using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewPlaceable", menuName = "PlaceableObjects/NewPlaceable")]
public class Placeable : ScriptableObject
{
    
    public GameObject prefab;

   
    public List<Vector2Int> shape;

    
    public bool canRotate = true;

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
            if (!grid.InBounds(cell.x, cell.y) || grid.IsOccupied(cell.x, cell.y))
                return false;
        }
        return true;
    }
    public void MarkCells(RoomGrid grid, Vector2Int origin, List<Vector2Int> shape)
    {
        foreach (var offset in shape)
        {
            Vector2Int cell = origin + offset;
            grid.SetOccupied(cell.x, cell.y);
        }
    }





}

