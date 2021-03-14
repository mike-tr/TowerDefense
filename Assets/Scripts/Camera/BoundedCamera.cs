using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedCamera : MonoBehaviour {
    // Start is called before the first frame update
    public float speed = 10;
    public float sizeX = 10;
    public float sizeY = 10;
    public Vector2 center;
    public Transform camTransform;

    public Transform target;

    private Camera cam;
    void Start () {
        target.localScale = new Vector3 (sizeX, sizeY, 1);
        target.position = center;

        if (camTransform == null)
            camTransform = Camera.main.transform;
        cam = camTransform.GetComponent<Camera> ();

        camTransform.position = new Vector3 (center.x, center.y, -10);
    }

    // Update is called once per frame
    void Update () {
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

        var halfX = sizeX / 2;
        var halfY = sizeY / 2;

        var verticalBound = cam.orthographicSize;
        var horizontalBound = verticalBound * Screen.width / Screen.height;

        var offset = (Vector2) camTransform.position - center;

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
            camTransform.position = new Vector3 (center.x, center.y, -10);
        }
        camTransform.position += movement * Time.deltaTime * speed;
    }
}
