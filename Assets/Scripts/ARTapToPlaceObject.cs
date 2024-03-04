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

    private Pose placementPose;
    private ARRaycastManager raycastManager;
    private bool placementPoseIsValid = false;
    private GameObject objToSpawn;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        objToSpawn = greenObject; // Set default object to spawn
    }

    private void Update()
    {
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
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void SetObjectToSpawn(GameObject obj)
    {
        objToSpawn = obj;
    }

    private void PlaceObject()
    {
        Instantiate(objToSpawn, placementPose.position, placementPose.rotation);
    }
}
