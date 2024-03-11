using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToDeleteObject : MonoBehaviour
{
    public GameObject placementIndicator;
    private GameObject hitObject; // Store reference to the object to delete

    private Pose PlacementPose;
    public ARRaycastManager raycastManager;
    private bool placementPoseIsValid = false;


    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    public void OnTapButtonClick()
    {
        // If there is a valid location and a valid hit object, delete it
        if (placementPoseIsValid && hitObject != null)
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

            // Check for collisions with existing objects
            Collider[] colliders = Physics.OverlapBox(PlacementPose.position, placementIndicator.GetComponent<BoxCollider>().size / 2f, Quaternion.identity);
            bool objectFound = false;
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("ARObject"))
                {
                    hitObject = collider.gameObject;
                    objectFound = true;
                    break;
                }
            }

            if (!objectFound)
            {
                hitObject = null;
            }
        }
        else
        {
            hitObject = null;
        }
    }
    
    void UpdatePlacementIndicator()
    {
        placementIndicator.SetActive(placementPoseIsValid);
        if (placementPoseIsValid)
        {
            // Set placement indicator's position to the camera's position and rotation
            placementIndicator.transform.SetPositionAndRotation(Camera.main.transform.position, Camera.main.transform.rotation);
        }
    }

    void DeleteObject()
    {
        // Delete the object
        Destroy(hitObject);
    }
}