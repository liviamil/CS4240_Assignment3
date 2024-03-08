using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public PlusButtonController plusButtonController;
    public MoveButtonController moveButtonController;
    public DeleteButtonController deleteButtonController;

    public void SetActionPlus()
    {
        currentAction = ActionType.Plus;
        LogButtonStates();
        Debug.Log("Action set to Plus");
    }

    public void SetActionMove()
    {
        currentAction = ActionType.Move;
        LogButtonStates();
        Debug.Log("Action set to Move");
    }

    public void SetActionDelete()
    {
        currentAction = ActionType.Delete;
        LogButtonStates();
        Debug.Log("Action set to Delete");
    }

    public void OnTap()
    {
        LogButtonStates();
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
                Debug.Log("No action is selected");
                break;
        }
    }

    private void LogButtonStates()
    {
        string logMessage = "Button States: " +
                            "\nPlace Button  State: " + placeObjectScript.buttonState +
                            "\nPlus Button State: " + plusButtonController.buttonState +
                            "\nMove Button State: " + moveButtonController.buttonState +
                            "\nDelete Button State: " + deleteButtonController.buttonState;
        Debug.Log(logMessage);
    }
}
