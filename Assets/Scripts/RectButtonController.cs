using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectButtonController : MonoBehaviour
{
    public GameObject placementIndicator;
    public Sprite newImage; // New image for Button
    public Sprite originalImage; // Original image for Button

    private Image activeImage;
    private bool buttonState = false;

    void Start()
    {
        activeImage = GetComponent<Image>();

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
        }
        else
        {
            if (activeImage != null && originalImage != null)
            {
                activeImage.sprite = originalImage;
            }

            placementIndicator.SetActive(false);

        }


    }
}
