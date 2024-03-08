using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureButtonsController : MonoBehaviour
{
    private Button[] buttonsB;
    private Button activeButton;
    private bool allButtonsOriginal = true; // Variable to check if all buttons are in their original state

    // Start is called before the first frame update
    void Start()
    {
        GameObject furniture = GameObject.Find("Furniture");
        if (furniture != null)
        {
            Debug.Log("get furniture in FurnitureButtons");
            buttonsB = furniture.GetComponentsInChildren<Button>();
            // Set buttons B to be invisible initially
            foreach (Button buttonB in buttonsB)
            {
                buttonB.gameObject.SetActive(false);
                buttonB.onClick.AddListener(() => OnButtonClick(buttonB));
            }
        }
    }

    // Function to be called when any button is clicked
    void OnButtonClick(Button clickedButton)
    {
        // Check if all buttons have their original images
        allButtonsOriginal = AllButtonsOriginal();

        // If all buttons are original, do not spawn object
        if (allButtonsOriginal)
        {
            Debug.Log("All buttons are in their original state. No object will be spawned.");
            return;
        }

        // If the clicked button is already active, do nothing
        if (activeButton == clickedButton)
            return;

        // Deactivate the previously active button
        if (activeButton != null)
            SetButtonState(activeButton, false);

        // Activate the clicked button
        SetButtonState(clickedButton, true);
        activeButton = clickedButton;
    }

    // Check if all buttons have their original images
    bool AllButtonsOriginal()
    {
        foreach (Button button in buttonsB)
        {
            CircleButtonController circleButtonController = button.GetComponent<CircleButtonController>();
            GreyButtonController greyButtonController = button.GetComponent<GreyButtonController>();
            GreenButtonController greenButtonController = button.GetComponent<GreenButtonController>();
            RectButtonController rectButtonController = button.GetComponent<RectButtonController>();

            if (circleButtonController != null && circleButtonController.originalImage != button.GetComponent<Image>().sprite)
                return false;
            else if (greyButtonController != null && greyButtonController.originalImage != button.GetComponent<Image>().sprite)
                return false;
            else if (greenButtonController != null && greenButtonController.originalImage != button.GetComponent<Image>().sprite)
                return false;
            else if (rectButtonController != null && rectButtonController.originalImage != button.GetComponent<Image>().sprite)
                return false;
        }
        return true;
    }

    // Set the state (sprite and activation) of a button
    void SetButtonState(Button button, bool isActive)
    {
        CircleButtonController circleButtonController = button.GetComponent<CircleButtonController>();
        GreyButtonController greyButtonController = button.GetComponent<GreyButtonController>();
        GreenButtonController greenButtonController = button.GetComponent<GreenButtonController>();
        RectButtonController rectButtonController = button.GetComponent<RectButtonController>();

        if (circleButtonController != null)
        {
            SetButtonState(circleButtonController, isActive);
            if (isActive)
            {
                Debug.Log("Circle button is active");
            }
        }
        else if (greyButtonController != null)
        {
            SetButtonState(greyButtonController, isActive);
            if (isActive)
            {
                Debug.Log("Grey button is active");
            }
        }
        else if (greenButtonController != null)
        {
            SetButtonState(greenButtonController, isActive);
             if (isActive)
            {
                Debug.Log("Green button is active");
            }
        }
        else if (rectButtonController != null)
        {
            SetButtonState(rectButtonController, isActive);
            if (isActive)
            {
                Debug.Log("Rect button is active");
            }
        }
        else
        {
            Debug.LogError("Unknown button controller type!");
        }
    }

    // Overloaded method to set the state (sprite and activation) of a CircleButtonController and the rest
    void SetButtonState(CircleButtonController buttonController, bool isActive)
    {
        Button button = buttonController.GetComponent<Button>();
        if (isActive)
        {
            buttonController.SetNewImage();
        }
        else
        {
            buttonController.SetOriginalImage();
        }
    }

        void SetButtonState(GreenButtonController buttonController, bool isActive)
    {
        Button button = buttonController.GetComponent<Button>();
        if (isActive)
        {
            buttonController.SetNewImage();
        }
        else
        {
            buttonController.SetOriginalImage();
        }
    }
        void SetButtonState(GreyButtonController buttonController, bool isActive)
    {
        Button button = buttonController.GetComponent<Button>();
        if (isActive)
        {
            buttonController.SetNewImage();
        }
        else
        {
            buttonController.SetOriginalImage();
        }
    }

        void SetButtonState(RectButtonController buttonController, bool isActive)
    {
        Button button = buttonController.GetComponent<Button>();
        if (isActive)
        {
            buttonController.SetNewImage();
        }
        else
        {
            buttonController.SetOriginalImage();
        }
    }
}
