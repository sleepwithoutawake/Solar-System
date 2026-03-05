using System;
using UnityEngine;

public class PlanetSystemController
{
    TimeModel timeModel;
    IPlanetEphemerisService ephemeris;
    PlanetView[] planets;
    int lastUpdateFrame = -1; 

    public PlanetSystemController(
        TimeModel timeModel,
        IPlanetEphemerisService ephemeris,
        PlanetView[] planets)
    {
        this.timeModel = timeModel;
        this.ephemeris = ephemeris;
        this.planets = planets;

        Debug.Log("[BOOT] PlanetSystemController created with " + planets.Length + " planets");

        timeModel.OnTimeChanged += UpdatePlanets;
    }
    public void Dispose()
    {
        timeModel.OnTimeChanged -= UpdatePlanets;
        Debug.Log("[BOOT] PlanetSystemController disposed, event unsubscribed");
    }

    void UpdatePlanets(DateTime time)
    {
        // 非回归测试：检测同帧被调用两次
        if (Time.frameCount == lastUpdateFrame)
        {
            Debug.LogWarning("[WARN] UpdatePlanets called twice in frame "
                + Time.frameCount + " - possible double subscription!");
            return;
        }
        lastUpdateFrame = Time.frameCount;

        Debug.Log("[TIME] Updating " + planets.Length + " planets @ " + time.ToString("yyyy-MM-dd"));
        foreach (var planet in planets)
        {
            Vector3 pos = ephemeris.GetPlanetPosition(planet.planet, time);
            // 对数压缩：让远近行星都在合理范围内
            float distance = pos.magnitude;
            float scaledDistance = Mathf.Log(1f + distance) * 2f;
            Vector3 scaledPos = pos.normalized * scaledDistance;

            // 非回归测试：如果还是太远就警告
            if (scaledPos.magnitude > 15f)
                Debug.LogWarning("[WARN] " + planet.planet
                    + " still far after scaling: " + scaledPos.magnitude.ToString("F2") + " units");

            planet.SetPosition(pos);
        }
    }
}