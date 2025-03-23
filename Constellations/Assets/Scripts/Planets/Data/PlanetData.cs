using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "Planets/Planet")]
public class PlanetData : ScriptableObject
{
    [Header("Planet Info")]
    public string planet_name;
    public string planet_description;

    [Header("Planet Data")]
    public string planet_scene;
}
