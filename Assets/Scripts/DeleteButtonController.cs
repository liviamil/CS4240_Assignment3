using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButtonController : MonoBehaviour
{
    public Sprite newImage; // New image for Button
    public Sprite originalImage; // Original image for Button

    private Image activeDeleteImage;
    private bool buttonState = false;

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
    
    // Update is called once per frame
    void Update()
    {

    }
}
