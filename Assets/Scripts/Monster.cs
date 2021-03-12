using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public Node current;
    public List<Node> path = null;

    private Node targetNode;
    private Board board;
    private Node next;

    public float speed = 10f;

    private const float speedFactor = 0.001f;
    // Start is called before the first frame update
    void Start () {

    }

    public void Initialize (Board board, Node current, Node target) {
        targetNode = target;
        this.board = board;
        this.current = current;
        transform.parent = board.monstersHolder;
        transform.localPosition = new Vector2 (current.x, current.y);
    }

    public void ResetPath () {
        path = null;
    }
    // Update is called once per frame
    void Update () {
        if (path != null) {

            if (current == path[0]) {
                if (next == targetNode) {
                    return;
                }
                path.RemoveAt (0);
                next = path[0];
            } else {
                var dir = next.GetPosition () - transform.position;
                if (dir.sqrMagnitude < 0.25 * 0.25) {
                    current = next;
                }
                dir = dir.normalized * speed * speedFactor;
                transform.position += dir;

                transform.up = transform.up * 0.1f + dir * 0.9f;
            }

        } else {
            path = PathFinding.FindPath (current, targetNode);
            path.Reverse ();
        }
    }
}
