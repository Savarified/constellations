using UnityEngine;

[CreateAssetMenu(fileName = "ResourceNodeData", menuName = "Resources/Resource Node")]
public class ResourceNodeData : ScriptableObject
{
    public enum RequiredTool {
        AXE,
        PICKAXE,
        SHOVEL
    };

    public RequiredTool requiredTool;
    public string nodeName;
    public string nodeDescription;
    public int nodeHealth;
}
