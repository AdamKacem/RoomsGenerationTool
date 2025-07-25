using UnityEngine;

[CreateAssetMenu(fileName = "WallPlaceable", menuName = "Scriptable Objects/WallPlaceable")]
public class WallPlaceable : ScriptableObject
{
    public GameObject prefab;

    public int padding;

    public float yOffset;

    public float offset;

    public string type;

    

    public WallPlaceable(GameObject prefab, int padding, float offset, float yOffset,string type)
    {
        this.prefab = prefab;
        this.padding = padding;
        this.offset = offset;
        this.yOffset = yOffset;
        this.type = type;
    }

    


}
