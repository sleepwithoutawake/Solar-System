using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SolarSystemGrabHandler : MonoBehaviour
{
    [Header("The root object to move when grabbed")]
    public Transform solarSystemRoot;

    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    Vector3 grabOffset;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("[XR] SolarSystemGrabHandler: XRGrabInteractable not found!");
            return;
        }

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("[XR] Table grabbed");
    }

    void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("[XR] Table released");

        // Move SolarSystemRoot to where Handle ended up
        solarSystemRoot.position = transform.position;

        // Reset Handle back to center of SolarSystemRoot
        transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        // While grabbed, SolarSystemRoot follows Handle
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            solarSystemRoot.position = transform.position;
        }
    }
}