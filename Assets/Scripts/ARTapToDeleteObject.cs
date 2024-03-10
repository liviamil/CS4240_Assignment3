using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToDeleteObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    public bool buttonState = false; // Flag to indicate if tap button is clicked

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    public void OnTapButtonClick()
    {
        // if there is a valid location + we tap the tapbutton, destroy an item at that location
        if (placementPoseIsValid)
        {
            DeleteObject();
        }        
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
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
        placementIndicator.SetActive(placementPoseIsValid);
        if (placementPoseIsValid)
        {
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
    }

    void DeleteObject()
    {
        // Raycast to detect objects under the placement indicator
        RaycastHit hit;
        if (Physics.Raycast(placementPose.position, Vector3.up, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object has a tag indicating it's deletable (you can customize this)
            if (hitObject.CompareTag("ARObject"))
            {
                // Delete the object
                Destroy(hitObject);
            }
        }
    }
}