using UnityEngine;
using TMPro;

public class FocusController : MonoBehaviour
{
    [Header("Info panel UI")]
    public GameObject infoPanel;
    public TextMeshProUGUI planetNameText;
    public TextMeshProUGUI planetInfoText;

    void OnEnable()
    {
        PlanetSelectable.OnPlanetSelected += ShowPlanetInfo;
    }

    void OnDisable()
    {
        PlanetSelectable.OnPlanetSelected -= ShowPlanetInfo;
    }

    void ShowPlanetInfo(PlanetData.Planet planet)
    {
        Debug.Log("[XR] Focus on: " + planet);
        infoPanel.SetActive(true);
        planetNameText.text = planet.ToString();
        planetInfoText.text = GetPlanetInfo(planet);
    }

    string GetPlanetInfo(PlanetData.Planet planet)
    {
        switch (planet)
        {
            case PlanetData.Planet.Mercury: return "Distance: 0.39 AU\nOrbital period: 88 days";
            case PlanetData.Planet.Venus:   return "Distance: 0.72 AU\nOrbital period: 225 days";
            case PlanetData.Planet.Earth:   return "Distance: 1.00 AU\nOrbital period: 365 days";
            case PlanetData.Planet.Mars:    return "Distance: 1.52 AU\nOrbital period: 687 days";
            case PlanetData.Planet.Jupiter: return "Distance: 5.20 AU\nOrbital period: 12 years";
            case PlanetData.Planet.Saturn:  return "Distance: 9.58 AU\nOrbital period: 29 years";
            case PlanetData.Planet.Uranus:  return "Distance: 19.2 AU\nOrbital period: 84 years";
            case PlanetData.Planet.Neptune: return "Distance: 30.1 AU\nOrbital period: 165 years";
            default: return "No data";
        }
    }
}