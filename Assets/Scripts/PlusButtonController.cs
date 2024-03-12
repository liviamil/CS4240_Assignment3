using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusButtonController : MonoBehaviour
{
    // Reference to the script you want to enable/disable
    public ARTapToPlaceObject ArTapToPlaceScript;

    public Sprite newImage; // New image for Button
    public Sprite originalImage; // Original image for Button

    private Image activePlusImage;
    private Button[] furnitureButtons;
    private Button[] actionButtons; // Array to store other action buttons
    public bool buttonState = false;

    // Start is called before the first frame update
    void Start()
    {
        activePlusImage = GetComponent<Image>();

        // Find all buttons under the "Furniture" GameObject
        GameObject furniture = GameObject.Find("Furniture");
        if (furniture != null)
        {
            furnitureButtons = furniture.GetComponentsInChildren<Button>(includeInactive: true);
        }   
        // Disable the ArTapToPlaceScript initially
        ArTapToPlaceScript.enabled = false;
    }

    public void OnPlusButtonClick()
    {
        // Toggle button state
        buttonState = !buttonState;

        if (buttonState)
        {
            // Change UI of Button A to new image
            if (activePlusImage != null && newImage != null)
            {
                activePlusImage.sprite = newImage;
                // Deactivate other action buttons
                DeactivateOtherActionButtons();
            }
        }
        else
        {
            // Change UI of Button A back to original image
            if (activePlusImage != null && originalImage != null)
            {
                activePlusImage.sprite = originalImage;
            }
        }
    }

    // Deactivate other action buttons
    void DeactivateOtherActionButtons()
    {
        MoveButtonController[] moveButtons = FindObjectsOfType<MoveButtonController>();
        foreach (MoveButtonController moveButton in moveButtons)
        {
            if (moveButton != this)
            {
                moveButton.DeactivateButton();
            }
        }

        DeleteButtonController[] deleteButtons = FindObjectsOfType<DeleteButtonController>();
        foreach (DeleteButtonController deleteButton in deleteButtons)
        {
            if (deleteButton != this)
            {
                deleteButton.DeactivateButton();
            }
        }
    }

    // Deactivate this button
    public void DeactivateButton()
    {
        buttonState = false;
        if (activePlusImage != null && originalImage != null)
        {
            activePlusImage.sprite = originalImage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle visibility of furniture buttons based on button state
        if (furnitureButtons != null)
        {
            foreach (Button furniture in furnitureButtons)
            {
                furniture.gameObject.SetActive(buttonState);
            }
        }

        // Enable or disable the ArTapToPlaceScript based on the button state
        ArTapToPlaceScript.enabled = buttonState;

        // Debug the ARTapToPlace script
        Debug.Log("ARTapToPlace enabled: " + ArTapToPlaceScript.enabled);
        Debug.Log("Plus button state: " + buttonState);
 
    }
}