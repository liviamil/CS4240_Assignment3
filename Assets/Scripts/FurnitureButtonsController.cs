using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureButtonsController : MonoBehaviour
{
    private Button[] buttonsB;
    private Button activeButton;

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
        }
        else if (greyButtonController != null)
        {
            SetButtonState(greyButtonController, isActive);
        }
        else if (greenButtonController != null)
        {
            SetButtonState(greenButtonController, isActive);
        }
        else if (rectButtonController != null)
        {
            SetButtonState(rectButtonController, isActive);
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
