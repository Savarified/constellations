using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "Resources/Resource")]
public class ResourceData : ScriptableObject // phk - constellations
{
    public enum ResourceType {
        WOOD,
        STONE,
        METAL,
        ALIEN
    };

    public ResourceType resourceType;
    public string resourceName;
    public string ResourceDescription;
    public int resourceMaxStack = 64;
    public int resourceDropAmount;
}
