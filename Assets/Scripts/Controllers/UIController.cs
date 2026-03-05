using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("References")]
    public TimeModel timeModel;

    [Header("UI Elements")]
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI speedText;
    public Button playPauseButton;
    public TextMeshProUGUI playPauseButtonText;

    [Header("Scale")]
    public ScaleController scaleController;

    void OnEnable()
    {
        if (timeModel != null)
            timeModel.OnTimeChanged += UpdateDateDisplay;
    }

    void OnDisable()
    {
        if (timeModel != null)
            timeModel.OnTimeChanged -= UpdateDateDisplay;
    }

    // Called by AppBootstrapper after TimeModel is created
    public void Init(TimeModel model)
    {
        timeModel = model;
        timeModel.OnTimeChanged += UpdateDateDisplay;
        UpdateSpeedDisplay();
    }

    void UpdateDateDisplay(System.DateTime time)
    {
        if (dateText != null)
            dateText.text = time.ToString("yyyy-MM-dd");
    }

    void UpdateSpeedDisplay()
    {
        if (speedText != null)
            speedText.text = "Speed: x" + timeModel.TimeScale;
    }

    public void OnPlayPauseClicked()
    {
        if (timeModel.IsPlaying)
        {
            timeModel.Pause();
            playPauseButtonText.text = " Play";
            Debug.Log("[INPUT] Simulation paused");
        }
        else
        {
            timeModel.Play();
            playPauseButtonText.text = " Pause";
            Debug.Log("[INPUT] Simulation resumed");
        }
    }

    public void OnSpeedUpClicked()
    {
        float newSpeed = timeModel.TimeScale * 10f;
        newSpeed = Mathf.Min(newSpeed, 100f);
        timeModel.SetScale(newSpeed);
        UpdateSpeedDisplay();
        Debug.Log("[INPUT] Speed set to x" + newSpeed);
    }

    public void OnSpeedDownClicked()
    {
        float newSpeed = timeModel.TimeScale / 10f;
        newSpeed = Mathf.Max(newSpeed, 1f);
        timeModel.SetScale(newSpeed);
        UpdateSpeedDisplay();
        Debug.Log("[INPUT] Speed set to x" + newSpeed);
    }

    public void OnResetClicked()
    {
        if (scaleController != null)
            scaleController.SetScale(1f);

        Debug.Log("[INPUT] Reset scale");
    }
}