using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public Button tapToPlaceButton; // Reference to the TapToPlace button
    private GameObject objToSpawn;

    private Pose PlacementPose; 
    public ARRaycastManager raycastManager;
    private bool placementPoseIsValid = false;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();

        // Add an event listener to the TapToPlace button
        tapToPlaceButton.onClick.AddListener(PlaceObject);
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
            PlacementPose = hits[0].pose;
        }
    }

    void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void SetObjectToSpawn(GameObject prefab)
    {
        objToSpawn = prefab;
    }

    private void PlaceObject()
    {
        // Check if the tap occurs within the bounds of the button
        if (EventSystem.current.currentSelectedGameObject == tapToPlaceButton.gameObject)
        {
            Instantiate(objToSpawn, PlacementPose.position, PlacementPose.rotation);
        }
    }
}
