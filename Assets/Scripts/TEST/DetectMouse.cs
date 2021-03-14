using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    // Start is called before the first frame update
    private UserInputManager inputManager;
    private bool addSpeed = false;
    void Start () {
        inputManager = UserInputManager.instance;
    }
    public Vector2 addCameraMovement = Vector2.zero;

    private void Update () {
        if (addSpeed) {
            inputManager.addMovement (addCameraMovement);
            //inputManager.moveme
        }
    }
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter (PointerEventData pointerEventData) {
        addSpeed = true;
        //Output to console the GameObject's name and the following message
        //Debug.Log ("Cursor Entering " + name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit (PointerEventData pointerEventData) {
        addSpeed = false;
        //Output the following message with the GameObject's name
        //Debug.Log ("Cursor Exiting " + name + " GameObject");
    }
}
