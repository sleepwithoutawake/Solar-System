using System;
using UnityEngine;

public class PlanetEphemerisService : IPlanetEphemerisService
{
    public Vector3 GetPlanetPosition(PlanetData.Planet planet, DateTime date)
    {
        Vector3 rawPos = PlanetData.GetPlanetPosition(planet, date);
        float distance = rawPos.magnitude;

        if (distance > 20f)
            Debug.LogWarning("[WARN] " + planet
                + " distance is very large: " + distance.ToString("F2") + " AU");

        return rawPos;
    }
}