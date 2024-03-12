using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToMoveObject : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject placementIndicator;

    private GameObject objToMove;

    private bool isObjectSelected = false;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    public TapButtonController tapButtonController;
    public bool buttonState = false;

    private bool collisionDetected = false;
    private bool moving = false;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        placementIndicator.SetActive(false);
    }

    public void OnTapButtonClick()
    {
        // Consider the case where initially false, before toggling, need to check if object is detected
        if (!buttonState && collisionDetected && objToMove != null) {
            tapButtonController.SetOnHold(); // Update UI to onhold
            buttonState = !buttonState; // Toggle button state
            MoveObject();
        }
        else if (buttonState && placementPoseIsValid && moving) {
            tapButtonController.ReleaseOnHold(); // Reset UI
            buttonState = !buttonState; // Toggle button state
            PlaceObject();
            moving = false;
        }     
    }

    private void Update()
    {
        UpdatePlacementIndicator();
        UpdatePlacementPose();
        UpdateCollisionStatus();
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

    void UpdateCollisionStatus()
    {
        collisionDetected = false; // Reset collision status

        // Perform a raycast from the placement indicator upwards to detect objects
        if (Physics.Raycast(placementPose.position - Vector3.up * 5.0f, Vector3.up, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("ARObject"))
            {
                collisionDetected = true;
                objToMove = hit.collider.gameObject;
            }
        } else {
            objToMove = null;
        }
    }

    void MoveObject() {
        objToMove.transform.SetParent(placementIndicator.transform, true);
        moving = true;
    }

    void PlaceObject() {
        objToMove.transform.SetParent(null); // Reset parent
        objToMove.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
    }

}
