using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenButtonController : MonoBehaviour
{
    public GameObject greenPrefab; // Assign in inspector
    public GameObject placementIndicator;
    public Sprite newImage; // New image for Button
    public Sprite originalImage; // Original image for Button
    public ARTapToPlaceObject placementController; // Link to ARTapToPlaceObject

    private Image activeImage;
    public bool buttonState = false;

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

        // Function to set the new image for the button
    public void SetNewImage()
    {
        if (activeImage != null && newImage != null)
        {
            activeImage.sprite = newImage;
        }

        // Activate the PlacementIndicator GameObject
        placementIndicator.SetActive(true);
        placementController.SetObjectToSpawn(greenPrefab);
    }

    // Function to set the original image for the button
    public void SetOriginalImage()
    {
        if (activeImage != null && originalImage != null)
        {
            activeImage.sprite = originalImage;
        }

        // Check if any of the furniture buttons are still active
        if (!AnyFurnitureButtonActive())
        {
            // Deactivate the PlacementIndicator GameObject
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
            SetNewImage();}
        else
        {
            SetOriginalImage();
        }
    }

    // Function to check if any furniture button is active
    private bool AnyFurnitureButtonActive()
    {
        // Check if any of the furniture buttons are active
        // You can add additional checks here if you have more furniture buttons
        return buttonState;
    }
}
