using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonController : MonoBehaviour
{
    public ARTapToMoveObject ARTapToMoveScript;
    
    public Sprite newImage; // New image for Button
    public Sprite originalImage; // Original image for Button

    private Image activeMoveImage;
    private Button[] actionButtons; // Array to store other action buttons
    public bool buttonState = false;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveImage = GetComponent<Image>();

        ARTapToMoveScript.enabled = false;
    }

    public void OnMoveButtonClick()
    {
        // Toggle button state
        buttonState = !buttonState;

        if (buttonState)
        {
            // Change UI of Button A to new image
            if (activeMoveImage != null && newImage != null)
            {
                activeMoveImage.sprite = newImage;
                // Deactivate other action buttons
                DeactivateOtherActionButtons();
            }
        }
        else
        {
            // Change UI of Button A back to original image
            if (activeMoveImage != null && originalImage != null)
            {
                activeMoveImage.sprite = originalImage;
            }
        }
    }

    // Deactivate other action buttons
    void DeactivateOtherActionButtons()
    {
        PlusButtonController[] plusButtons = FindObjectsOfType<PlusButtonController>();
        foreach (PlusButtonController plusButton in plusButtons)
        {
            if (plusButton != this)
            {
                plusButton.DeactivateButton();
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
        if (activeMoveImage != null && originalImage != null)
        {
            activeMoveImage.sprite = originalImage;
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        ARTapToMoveScript.enabled = buttonState;
    }
}
