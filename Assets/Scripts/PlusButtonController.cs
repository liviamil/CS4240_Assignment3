using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusButtonController : MonoBehaviour
{
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
    }

    public void OnPlusButtonClick()
    {
        // Toggle button state
        buttonState = !buttonState;

        // Debug the button state
        Debug.Log("PlusButtonController Button State: " + buttonState);

        if (buttonState)
        {
            // Change UI of Button A to new image
            if (activePlusImage != null && newImage != null)
            {
                activePlusImage.sprite = newImage;
                // Deactivate other action buttons
                DeactivateOtherActionButtons();
            }

            // Turn on the visibility of Buttons B
            if (furnitureButtons != null)
            {
                foreach (Button furniture in furnitureButtons)
                {
                    furniture.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // Change UI of Button A back to original image
            if (activePlusImage != null && originalImage != null)
            {
                activePlusImage.sprite = originalImage;
            }

            // Turn off the visibility of Buttons B
            if (furnitureButtons != null)
            {
                foreach (Button furniture in furnitureButtons)
                {
                    furniture.gameObject.SetActive(false);
                }
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

    }
}