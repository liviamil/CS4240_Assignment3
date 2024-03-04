using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject greenObject;
    public GameObject rectObject;
    public GameObject circleObject;
    public GameObject greyObject;

    private Pose PlacementPose; // Stores position + rotation data
    private ARRaycastManager raycastManager;
    private bool placementPoseIsValid = false;
    private GameObject objToSpawn;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        objToSpawn = circleObject; // Set default object to spawn
    }

    private void Update()
    {
        if (raycastManager == null)
        return;

        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // if there is a valid location + we tap the screen, spawn an item at that location
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    void UpdatePlacementPose()
    {
        if (Camera.main == null || raycastManager == null)
        return;

        // convert viewport position to screen position. Center of screen may not be (0.5, 0.5) since different phones have different sizes
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); 

        // shoot a ray out from middle of screen to see if it hits anything
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        // is there a plane and are we currently facing it
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    /**
     * Move the placement indicator object
     */
    void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            // if there is a valid plane, activate placement indicator object and make it follow around
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            // no valid place, deactivate
            placementIndicator.SetActive(false);
        }
    }

    public void SetObjectToSpawn(GameObject obj)
    {
        objToSpawn = obj;
    }

    private void PlaceObject()
    {
        Instantiate(objToSpawn, PlacementPose.position, PlacementPose.rotation);
    }
}