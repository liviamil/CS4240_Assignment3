using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonController : MonoBehaviour
{
    public Sprite newImage; // New image for Button
    public Sprite originalImage; // Original image for Button

    private Image activeMoveImage;
    private bool buttonState = false;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveImage = GetComponent<Image>();
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
    
    // Update is called once per frame
    void Update()
    {

    }
}
