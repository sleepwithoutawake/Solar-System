using UnityEngine;


public class ScaleDownButton : MonoBehaviour
{
    public ScaleController scaleController;

    void Start()
    {
        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.AddListener(_ =>
        {
            Debug.Log("[INPUT] Scale down requested");
            scaleController.AddScale(-0.2f);  // negative = shrink
        });
    }
}