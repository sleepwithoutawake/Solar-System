using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DebugOverlay : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI text;

    [Header("Settings")]
    public int maxLines = 15;
    public float updateInterval = 0.5f;

    [Header("References")]
    public TimeModel timeModel;

    Queue<string> lines = new Queue<string>();
    float fpsTimer;
    float fps;
    float timeSinceUpdate;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    public void Init(TimeModel model)
    {
        timeModel = model;
    }

    void Update()
    {
        // Calculate FPS
        fpsTimer += Time.deltaTime;
        if (fpsTimer >= 0.5f)
        {
            fps = 1f / Time.deltaTime;
            fpsTimer = 0f;
        }

        // Update display periodically (not every frame)
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate >= updateInterval)
        {
            timeSinceUpdate = 0f;
            RefreshDisplay();
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
    // 忽略字体相关的警告（避免死循环）
    if (logString.Contains("font") || logString.Contains("Font") 
        || logString.Contains("Unicode") || logString.Contains("character"))
        return;

    // 只保留带标签的日志和错误
    bool isTagged = logString.StartsWith("[");
    bool isError = type == LogType.Error;
    
    if (!isTagged && !isError) return;

    string prefix = type == LogType.Warning ? "[WARN] " :
                    type == LogType.Error   ? "[ERR]  " : "";

    lines.Enqueue(prefix + logString);

    while (lines.Count > maxLines)
        lines.Dequeue();
    }

    void RefreshDisplay()
    {
        if (text == null) return;

        string date = timeModel != null
            ? timeModel.CurrentTime.ToString("yyyy-MM-dd")
            : "N/A";

        float speed = timeModel != null ? timeModel.TimeScale : 0f;
        string playing = timeModel != null && timeModel.IsPlaying ? "PLAYING" : "PAUSED";

        string header = $"=== DEBUG OVERLAY ===\n" +
                        $"FPS: {fps:F0} | Frame: {Time.deltaTime * 1000:F1}ms\n" +
                        $"Date: {date} | Speed: x{speed} | {playing}\n" +
                        $"---------------------\n";

        text.text = header + string.Join("\n", lines);
    }
}