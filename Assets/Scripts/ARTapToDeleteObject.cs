using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToDeleteObject : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    private GameObject objToDelete;
    private bool collisionDetected = false;

    public GameObject placementIndicator;
    private Pose placementPose;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        placementIndicator.SetActive(false); // Ensure the placement indicator is initially hidden
    }

    public void OnTapButtonClick()
    {
        if (collisionDetected && objToDelete != null)
        {
            DeleteObject();
        }
    }

    private void Update()
    {
        UpdatePlacementIndicator();
        UpdateCollisionStatus();
    }


    void UpdatePlacementIndicator()
    {
        // convert viewport position to screen position. Center of screen may not be (0.5, 0.5) since different phones have different sizes
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
                objToDelete = hit.collider.gameObject;
            }
        }
    }


    void DeleteObject()
    {
        // Delete the object
        Destroy(objToDelete);
    }
}