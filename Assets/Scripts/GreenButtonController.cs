using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenButtonController : MonoBehaviour
{
    public GameObject placementIndicator;
    public Sprite newImage; // New image for Button
    public Sprite originalImage; // Original image for Button
    public ARTapToPlaceObject placementController; // Link to ArTap to place

    private Image activeImage;
    private bool buttonState = false;

    void Start()
    {
        activeImage = GetComponent<Image>();

        if (placementController == null)
        {
            Debug.LogError("ARTapToPlaceObject reference not set!");
        }
        // Ensure the reference to the PlacementIndicator is set
        if (placementIndicator == null)
        {
            Debug.LogError("PlacementIndicator reference is not set!");
        }
        else
        {
            // Deactivate the PlacementIndicator GameObject initially
            placementIndicator.SetActive(false);
        }
    }

    // Function to be called when the button is clicked
    public void OnGreenButtonClick()
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
            placementController.SetObjectToSpawn(placementController.greenObject);
        }
        else
        {
            if (activeImage != null && originalImage != null)
            {
                activeImage.sprite = originalImage;
            }

            placementIndicator.SetActive(false);
            placementController.SetObjectToSpawn(null);
        }


    }
}
