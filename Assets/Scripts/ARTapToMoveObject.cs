using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToMoveObject : MonoBehaviour
{
    public ARRaycastManager raycastManager;

    public GameObject placementIndicator;
    
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private GameObject selectedObject;

    private bool isObjectSelected = false;
    public bool buttonState = false; // Flag to indicate if tap button is clicked

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        placementIndicator.SetActive(false);
    }

    public void OnTapButtonClick()
    {
        // if there is a valid location + we tap the tapbutton, destroy an item at that location
        if (placementPoseIsValid)
        {
            if (!isObjectSelected)
            {
                SelectObject();
            }
            else
            {
                PlaceSelectedObject();
            }
        }        
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (isObjectSelected)
        {
            MoveSelectedObject();
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    void UpdatePlacementIndicator()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); 
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            placementPose = hits[0].pose;
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void SelectObject()
    {
        // Raycast to detect objects under the placement indicator
        RaycastHit hit;
        if (Physics.Raycast(placementPose.position, Vector3.down, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object is selectable
            if (hitObject.CompareTag("ARObject"))
            {
                selectedObject = hitObject;
                isObjectSelected = true;
            }
        }
    }

    void MoveSelectedObject()
    {
        if (isObjectSelected && selectedObject != null)
        {
            // Move the selected object along with the camera
            selectedObject.transform.position = placementPose.position;
            selectedObject.transform.rotation = placementPose.rotation;
        }
    }

    void PlaceSelectedObject()
    {
        // Check if there's a valid placement location for the selected object
        Collider[] colliders = Physics.OverlapBox(placementPose.position, selectedObject.GetComponent<Collider>().bounds.extents / 2f, Quaternion.identity);
        bool canPlace = true;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != selectedObject)
            {
                canPlace = false;
                break;
            }
        }

        // If there's a valid placement location, place the object
        if (canPlace)
        {
            selectedObject = null;
            isObjectSelected = false;
        }
    }
}
