using UnityEngine;


public class GrabHandler : MonoBehaviour
{
    void Start()
    {
        var grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab == null)
        {
            Debug.LogError("[XR] GrabHandler: 找不到 XRGrabInteractable 组件！");
            return;
        }

        grab.selectEntered.AddListener(_ => Debug.Log("[XR] Table grabbed"));
        grab.selectExited.AddListener(_ => Debug.Log("[XR] Table released"));
    }
}