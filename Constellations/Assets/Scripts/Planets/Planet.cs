using UnityEngine;

public class Planet : MonoBehaviour
{
    // References
    [Header("PlanetData")]
    [SerializeField] PlanetData planet_data;

    [Header("Planet Flags")]
    public bool can_land = true;

    // Data
    [HideInInspector] public string planet_name;
    [HideInInspector] public string planet_description;
    [HideInInspector] public string planet_scene;

    void Awake()
    {
        planet_name = planet_data.planet_name;
        planet_description = planet_data.planet_description;
        planet_scene = planet_data.planet_scene;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
