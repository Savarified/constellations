using UnityEngine;
using UnityEngine.UI;

public class ResourceNode : MonoBehaviour // phk - constellations
{
    // References
    [SerializeField] ResourceData resourceData;
    [SerializeField] ResourceNodeData resourceNodeData;

    [SerializeField] Text titleText; // Change to TMP? Not sure regarding your plans for the UI
    [SerializeField] Text healthText;
    [SerializeField] Text descriptionText;
    [SerializeField] Text requirementText;

    // Data
    [HideInInspector] public string title;
    [HideInInspector] public string description;
    [HideInInspector] public ResourceData.ResourceType type;
    [HideInInspector] public ResourceNodeData.RequiredTool requiredTool;
    [HideInInspector] public int maxHealth;
    public int currentHealth;


    void Awake()
    {
        if (resourceData == null)
        {
            Debug.LogError("ERROR: ResourceData ScriptableObject missing from a ResourceNode!");
            return;
        }
        else if (resourceNodeData == null)
        {
            Debug.LogError("ERROR: ResourceNodeData ScriptableObject missing from a ResourceNode!");
            return;
        }

        title = resourceData.resourceName;
        description = resourceNodeData.nodeDescription;
        type = resourceData.resourceType;
        maxHealth = resourceNodeData.nodeHealth;
        currentHealth = maxHealth;
        requiredTool = resourceNodeData.requiredTool;
    }

    void Start()
    {
        titleText.text = title;
        healthText.text = $"{currentHealth} / {maxHealth}";
        descriptionText.text = description;
        requirementText.text = $"REQUIRED TOOL: {requiredTool}";
    }

    void Update()
    {
        
    }

    // Placeholder functions for tool interactions
    void TakeDamage(ResourceNodeData.RequiredTool tool, int damage)
    {
        if (tool != requiredTool)
            return;

        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else if (currentHealth <= 0)
        {
            Deplete();
        }
    }
    
    void Deplete()
    {
        Destroy(this);
    }
}
