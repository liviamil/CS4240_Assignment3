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
    private bool collisionDetected = false;


    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    public void OnTapButtonClick()
    {
        // if there is a valid location + we tap the tapbutton, destroy an item at that location
        if (placementPoseIsValid && collisionDetected)
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
            foreach (Collider collider in colliders)
            {
                // Check if the hit object has the tag "ARObject"
                if (hitObject.CompareTag("ARObject"))
                {
                    collisionDetected = true;
                }
                else
                {
                    collisionDetected = false;
                }
            
            collisionDetected = false;
            }
        }
        else
        {
            collisionDetected = false;
        }
        //     // Raycast to detect objects above the placement indicator
        //     RaycastHit hit;
        //     if (Physics.Raycast(PlacementPose.position, Vector3.up, out hit))
        //     {
        //         hitObject = hit.collider.gameObject;

        //         // Check if the hit object has the tag "ARObject"
        //         if (hitObject.CompareTag("ARObject"))
        //         {
        //             collisionDetected = true;
        //         }
        //         else
        //         {
        //             collisionDetected = false;
        //         }
        //     }
        //     else
        //     {
        //         collisionDetected = false;
        //     }
        // }
        // else
        // {
        //     collisionDetected = false;
        // }
    }

    void UpdatePlacementIndicator()
    {
        placementIndicator.SetActive(placementPoseIsValid);
        if (placementPoseIsValid)
        {
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
    }

    void DeleteObject()
    {
        // Delete the object
        Destroy(hitObject);
    }
}