using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectButtonController : MonoBehaviour
{
    public GameObject placementIndicator;
    public Sprite newImage;
    public Sprite originalImage;
    public ARTapToPlaceObject placementController;

    private Image activeImage;
    private bool buttonState = false;

    void Start()
    {
        activeImage = GetComponent<Image>();

        if (placementController == null)
        {
            Debug.LogError("ARTapToPlaceObject reference not set!");
        }

        if (placementIndicator == null)
        {
            Debug.LogError("PlacementIndicator reference is not set!");
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void OnRectButtonClick()
{
    // Toggle button state
    buttonState = !buttonState;

    if (buttonState)
    {
        if (activeImage != null && newImage != null)
        {
            activeImage.sprite = newImage;
        }

        // Activate the PlacementIndicator GameObject
        placementIndicator.SetActive(true);

        // Check if placementController is not null before accessing it
        if (placementController != null)
        {
            // Set the object to spawn in ARTapToPlaceObject script
            placementController.SetObjectToSpawn(placementController.rectObject);
        }
        else
        {
            Debug.LogError("placementController is not assigned in RectButtonController!");
        }
    }
    else
    {
        if (activeImage != null && originalImage != null)
        {
            activeImage.sprite = originalImage;
        }

        placementIndicator.SetActive(false);

        if (placementController != null)
        {
            // Set the object to spawn to null in ARTapToPlaceObject script
            placementController.SetObjectToSpawn(null);
        }
        else
        {
            Debug.LogError("placementController is not assigned in RectButtonController!");
        }
    }
}

}
