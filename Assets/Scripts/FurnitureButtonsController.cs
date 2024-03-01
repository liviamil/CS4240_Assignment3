using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureButtonsController : MonoBehaviour
{
    private Button[] buttonsB;

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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
