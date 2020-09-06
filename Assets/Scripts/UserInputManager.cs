using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInputManager : MonoBehaviour {
    // Start is called before the first frame update
    public Board targetBoard;
    public float speed = 5;
    private Camera cam;
    private Transform camTransform;
    public Transform markerPrefab;
    private Transform marker;

    void Start () {
        cam = Camera.main;
        camTransform = cam.transform;
        camTransform.position = new Vector3 (targetBoard.center.x, targetBoard.center.y, -10);

        marker = Instantiate (markerPrefab);
        marker.parent = transform;
        marker.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.Mouse0)) {
            if (!EventSystem.current.IsPointerOverGameObject ()) {
                var mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
                var node = targetBoard.NodeFromWorldPoint (mousePos);

                if (node != null) {
                    marker.gameObject.SetActive (true);
                    marker.position = node.position;
                } else {
                    marker.gameObject.SetActive (false);
                }
            }
        }

        CameraControll ();
    }

    void CameraControll () {
        var movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        if (Input.mousePosition.x > Screen.width * 0.95f) {
            movement.x = 1;
        } else if (Input.mousePosition.x < Screen.width * 0.05f) {
            movement.x = -1;
        }
        if (Input.mousePosition.y > Screen.height * 0.95f) {
            movement.y = 1;
        } else if (Input.mousePosition.y < Screen.height * 0.05f) {
            movement.y = -1;
        }

        var halfX = targetBoard.sizeX / 2 + 1;
        var halfY = targetBoard.sizeY / 2 + 1;

        var verticalBound = cam.orthographicSize;
        var horizontalBound = verticalBound * (Screen.width * (1 - cam.rect.xMin)) / Screen.height;

        var offset = (Vector2) camTransform.position - targetBoard.center;

        if (halfY - verticalBound < Mathf.Abs (offset.y)) {
            if (Vector2.Dot (new Vector2 (0, offset.y), movement) >= 0) {
                movement.y = 0;
            }
        }

        if (halfX - horizontalBound < Mathf.Abs (offset.x)) {
            if (Vector2.Dot (new Vector2 (offset.x, 0), movement) >= 0) {
                movement.x = 0;
            }
        }

        if (Input.GetKeyDown (KeyCode.R)) {
            camTransform.position = new Vector3 (targetBoard.center.x, targetBoard.center.y, -10);
        }
        camTransform.position += movement * Time.deltaTime * speed;
    }
}
