using UnityEngine;


public class ScaleUpButton : MonoBehaviour
{
    public ScaleController scaleController;

    void Start()
    {
        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.AddListener(_ =>
        {
            Debug.Log("[INPUT] Scale up requested");
            scaleController.AddScale(0.2f);
        });
    }
}