using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButtonController : MonoBehaviour
{
    public Sprite newImage; // New image for Button
    public Sprite originalImage; // Original image for Button

    private Image activeDeleteImage;
    private Button[] actionButtons; // Array to store other action buttons
    public bool buttonState = false;

    // Start is called before the first frame update
    void Start()
    {
        activeDeleteImage = GetComponent<Image>();
    }

    public void OnDeleteButtonClick()
    {
        // Toggle button state
        buttonState = !buttonState;

        if (buttonState)
        {
            // Change UI of Button A to new image
            if (activeDeleteImage != null && newImage != null)
            {
                activeDeleteImage.sprite = newImage;
                
                // Deactivate other action buttons
                DeactivateOtherActionButtons();
            }
        }
        else
        {
            // Change UI of Button A back to original image
            if (activeDeleteImage != null && originalImage != null)
            {
                activeDeleteImage.sprite = originalImage;
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

        MoveButtonController[] moveButtons = FindObjectsOfType<MoveButtonController>();
        foreach (MoveButtonController moveButton in moveButtons)
        {
            if (moveButton != this)
            {
                moveButton.DeactivateButton();
            }
        }
    }

    // Deactivate this button
    public void DeactivateButton()
    {
        buttonState = false;
        if (activeDeleteImage != null && originalImage != null)
        {
            activeDeleteImage.sprite = originalImage;
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
