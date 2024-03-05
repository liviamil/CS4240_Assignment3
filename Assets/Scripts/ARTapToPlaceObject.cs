using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject greenObject;
    public GameObject greyObject;
    public GameObject rectObject;
    public GameObject circleObject;

    private Pose PlacementPose; 
    public ARRaycastManager raycastManager;
    private bool placementPoseIsValid = false;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        UpdatePlacementPose();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
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
            PlacementPose = hits[0].pose;
        }
    }

    private void PlaceObject()
    {
        // Your existing code to place the object
    }

    public void ActivateRect()
    {
        // Your existing code
        Debug.Log("Rect is activated");
    }

    public void ActivateCircle()
    {
        // Your existing code
        Debug.Log("Circle is activated");
    }

    public void ActivateGrey()
    {
        // Your existing code
        Debug.Log("Grey is activated");
    }

    public void ActivateGreen()
    {
        // Your existing code
        Debug.Log("Green is activated");
    }
}
