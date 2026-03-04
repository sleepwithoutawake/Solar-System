using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class PlanetSelectable : MonoBehaviour
{
    [Header("Reference to the PlanetView on this object")]
    public PlanetView planetView;

    public static event Action<PlanetData.Planet> OnPlanetSelected;

    void Start()
    {
        if (GetComponent<Collider>() == null)
            Debug.LogError("[XR] " + gameObject.name + " is missing a Collider!");

        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (interactable == null)
        {
            Debug.LogError("[XR] " + gameObject.name + " is missing XRSimpleInteractable!");
            return;
        }

        interactable.selectEntered.AddListener(OnSelected);
    }

    void OnSelected(SelectEnterEventArgs args)
    {
        Debug.Log("[INPUT] Planet selected: " + planetView.planet);
        OnPlanetSelected?.Invoke(planetView.planet);
    }
}