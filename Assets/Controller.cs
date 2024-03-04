using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject interaction;
    // Start is called before the first frame update
    void Start()
    {
        placementIndicator.SetActive(false);
        interaction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
