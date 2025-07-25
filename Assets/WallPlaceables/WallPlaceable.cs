using UnityEngine;

[CreateAssetMenu(fileName = "WallPlaceable", menuName = "Scriptable Objects/WallPlaceable")]
public class WallPlaceable : ScriptableObject
{
    public GameObject prefab;

    public int padding;

    

    

    public WallPlaceable(GameObject prefab, int padding)
    {
        this.prefab = prefab;
        this.padding = padding;
    }

    


}
