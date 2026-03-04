using UnityEngine;

public class ScaleController : MonoBehaviour
{
    [Header("Target to scale")]
    public Transform target;

    [Header("Scale limits")]
    public float minScale = 0.2f;
    public float maxScale = 5f;

    float currentScale = 1f;

    public void SetScale(float value)
    {
        float clamped = Mathf.Clamp(value, minScale, maxScale);

        if (Mathf.Abs(clamped - value) > 0.001f)
            Debug.LogWarning("[WARN] Scale clamped: " + value + " -> " + clamped);

        currentScale = clamped;
        Debug.Log("[XR] Scale applied: " + currentScale);
        target.localScale = Vector3.one * currentScale;
    }

    public void AddScale(float delta)
    {
        SetScale(currentScale + delta);
    }
}