using UnityEngine;
using System;

public class AppBootstrapper : MonoBehaviour
{
    public SolarSystemConfig config;
    public PlanetView[] planets;

    public TimeController timeController;   // <- 新增：显式依赖

    TimeModel timeModel;
    PlanetSystemController controller;

    void Start()
    {
        Debug.Log("[BOOT] Initializing application");

        timeModel = new TimeModel();
        var ephemeris = new PlanetEphemerisService();

        controller = new PlanetSystemController(
            timeModel,
            ephemeris,
            planets
        );

        // 初始化时间推进器
        if (timeController != null)
            timeController.Init(timeModel);
        else
            Debug.LogWarning("[BOOT] TimeController is not assigned");
    }
}