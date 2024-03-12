using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToMoveObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private GameObject selectedObject;
    private bool isObjectSelected = false;
    private bool tapButtonClicked = false;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    public void OnTapButtonClick()
    {
        // Toggle tap button clicked state
        tapButtonClicked = !tapButtonClicked;

        // If tap button is clicked again and an object is already selected, deselect it
        if (!tapButtonClicked && isObjectSelected)
        {
            DeselectObject();
        }       
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // Check if tap button is clicked
        if (tapButtonClicked)
        {
            if (!isObjectSelected)
            {
                SelectObject();
            }
            else
            {
                MoveSelectedObject();
            }
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
        placementIndicator.SetActive(placementPoseIsValid);
        if (placementPoseIsValid)
        {
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
    }

    void SelectObject()
    {
        // Raycast to detect objects under the placement indicator
        RaycastHit objectHit;
        if (Physics.Raycast(placementPose.position - Vector3.up * 5.0f, Vector3.up, out objectHit, Mathf.Infinity))
        {
            GameObject hitObject = objectHit.collider.gameObject;

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

    void DeselectObject()
    {
        // Deselect the object by resetting its selected state
        selectedObject = null;
        isObjectSelected = false;
    }
}
