using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenButtonController : MonoBehaviour
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

    public void OnGreenButtonClick()
    {
        buttonState = !buttonState;

        if (buttonState)
        {
            if (activeImage != null && newImage != null)
            {
                activeImage.sprite = newImage;
            }

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
