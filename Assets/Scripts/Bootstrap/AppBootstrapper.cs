using UnityEngine;
using System;

public class AppBootstrapper : MonoBehaviour
{
    public SolarSystemConfig config;
    public PlanetView[] planets;

    public TimeController timeController;   // <- 新增：显式依赖

    TimeModel timeModel;
    PlanetSystemController controller;

    [Header("UI")]
    public UIController uiController;

    [Header("Debug")]
public DebugOverlay debugOverlay;
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
        // 初始化 UI 控制器
        if (uiController != null)
            uiController.Init(timeModel);
        else
            Debug.LogWarning("[BOOT] UIController is not assigned");
        // 初始化调试覆盖层
        if (debugOverlay != null)
            debugOverlay.Init(timeModel);
        else
            Debug.LogWarning("[BOOT] DebugOverlay is not assigned");
        // 确保启动时是播放状态
        timeModel.Play();
        timeModel.SetTime(DateTime.Now);

        Debug.Log("[BOOT] Application initialized!");
    }
    void OnDestroy()
    {
    controller?.Dispose();
    Debug.Log("[BOOT] AppBootstrapper destroyed, events cleaned up");
    }
}