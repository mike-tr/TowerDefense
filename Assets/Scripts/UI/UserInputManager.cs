using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInputManager : MonoBehaviour {
    public static UserInputManager instance;
    // Start is called before the first frame update
    public Board targetBoard;
    public float speed = 5;
    private Camera cam;
    private Transform camTransform;
    public Transform markerPrefab;
    private Transform marker;

    public Vector2 offsetWidth = Vector2.zero;
    public Vector2 offsetHeight = Vector2.zero;

    private void Awake () {
        // if (instance != null) {
        //     return;
        // }
        instance = this;
    }

    void Start () {
        cam = Camera.main;
        camTransform = cam.transform;
        camTransform.position = new Vector3 (targetBoard.center.x, targetBoard.center.y, -10);

        marker = Instantiate (markerPrefab);
        marker.parent = transform;
        marker.gameObject.SetActive (false);

        Debug.Log (cam.rect.xMin + " " + cam.rect.x);

        Debug.Log (offsetWidth + " : " + offsetHeight);
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

                    UIManager.instance.ShowUpgrades (node);
                    Debug.Log ("x : " + node.x + ", y : " + node.y + ", walkable : " + node.walkable);
                } else {
                    //marker.gameObject.SetActive (false);
                }
            }
        }

        CameraControll ();
    }

    private float delay = 0;
    private const float minTime = 0.1f;

    Vector3 movement = Vector3.zero;
    public void addMovement (Vector3 vector) {
        movement += vector;
    }
    void CameraControll () {
        movement += new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        // if (Input.mousePosition.x > Screen.width * (0.95f - offsetWidth.y)) {
        //     movement.x = 1;
        // } else if (Input.mousePosition.x < Screen.width * (0.05f + offsetWidth.x)) {
        //     if (Input.mousePosition.x > Screen.width * 0.1f) {
        //         movement.x = -1;
        //     }
        // }
        // if (Input.mousePosition.y > Screen.height * (0.90f - offsetHeight.y)) {
        //     movement.y = 1;
        // } else if (Input.mousePosition.y < Screen.height * (0.1f + offsetHeight.x)) {
        //     movement.y = -1;
        // }

        if (movement.sqrMagnitude > 0) {
            if (delay < minTime) {
                movement = Vector3.zero;
                delay += Time.deltaTime;
                return;
            }

            var halfX = targetBoard.sizeX / 2 + 1; // 30
            var halfY = targetBoard.sizeY / 2 + 1; // 30

            var verticalBound = cam.orthographicSize; // 10
            var horizontalBound = verticalBound * (Screen.width * (1 - cam.rect.xMin)) / Screen.height;

            var offset = (Vector2) camTransform.position - targetBoard.center;

            var maxY = halfY - verticalBound; // 20
            if (maxY > 0.1) {
                if (offset.y > maxY * offsetHeight.y) {
                    if (movement.y > 0) {
                        movement.y = 0;
                    }
                } else if (offset.y < -maxY * offsetHeight.x) {
                    if (movement.y < 0) {
                        movement.y = 0;
                    }
                }
            } else {
                movement.y = 0;
            }

            var maxX = halfX - horizontalBound;
            if (maxX > 0.1f) {
                if (offset.x > maxX * offsetWidth.y) {
                    if (movement.x > 0) {
                        movement.x = 0;
                    }

                } else if (offset.x < -maxX * offsetWidth.x) {
                    if (movement.x < 0) {
                        movement.x = 0;
                    }
                }

            } else {
                movement.x = 0;
            }

            if (Input.GetKeyDown (KeyCode.R)) {
                camTransform.position = new Vector3 (targetBoard.center.x, targetBoard.center.y, -10);
            }
            camTransform.position += movement * Time.deltaTime * speed;
            movement = Vector3.zero;
        } else {
            delay = 0;
        }
    }
}
