using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapButtonController : MonoBehaviour
{
    public enum ActionType
    {
        None,
        Plus,
        Move,
        Delete
    }

    public ActionType currentAction = ActionType.None;

    public ARTapToPlaceObject placeObjectScript;
    public ARTapToMoveObject moveObjectScript;
    public ARTapToDeleteObject deleteObjectScript;

    public void SetActionPlus()
    {
        currentAction = ActionType.Plus;
        Debug.Log("Action set to Plus");
    }

    public void SetActionMove()
    {
        currentAction = ActionType.Move;
        Debug.Log("Action set to Move");
    }

    public void SetActionDelete()
    {
        currentAction = ActionType.Delete;
        Debug.Log("Action set to Delete");
    }

    public void OnTap()
    {
        switch (currentAction)
        {
            case ActionType.Plus:
                if (placeObjectScript != null)
                {
                    placeObjectScript.OnTapButtonClick();
                    Debug.Log("Plus action activated");
                }
                break;
            case ActionType.Move:
                if (moveObjectScript != null)
                {
                    moveObjectScript.OnTapButtonClick();
                    Debug.Log("Move action activated");
                }
                break;
            case ActionType.Delete:
                if (deleteObjectScript != null)
                {
                    deleteObjectScript.OnTapButtonClick();
                    Debug.Log("Delete action activated");
                }
                break;
            default:
                Debug.LogError("Invalid action selected.");
                break;
        }
    }
}
