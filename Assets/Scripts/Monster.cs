using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public Node current;
    public List<Node> path = null;

    private Node targetNode;
    private Board board;
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
            print ("path :");
            foreach (var node in path) {
                print (node);
            }

        } else {
            path = PathFinding.FindPath (current, targetNode);
        }
    }
}
