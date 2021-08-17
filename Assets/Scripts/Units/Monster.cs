using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public Node current;
    public List<Node> path = null;

    private Node targetNode;
    private Board board;
    private Node next;

    public float intialHp = 100;

    public float speed = 10f;

    private const float closeDist = 0.25f;

    private const float speedFactor = 0.001f;

    public Transform rotationTarget;

    private HealthSystem healthSystem;
    // Start is called before the first frame update
    void Start () {
        healthSystem = new HealthSystem ();
        healthSystem.Initialize (intialHp, intialHp, GetComponentInChildren<LifeBar> ());
        healthSystem.OnDeathCallback += OnDeath;
    }

    void OnDeath () {
        Debug.Log ("Monster has died");
        Destroy (gameObject);
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

    public void TakeDamage (float damage) {
        healthSystem.TakeDamage (damage);
    }
    // Update is called once per frame
    void Update () {
        if (path != null) {

            if (current == path[0]) {
                if (next == targetNode) {
                    Debug.Log ("Got to target");
                    Destroy (gameObject);
                    //healthSystem.TakeDamage (0.33 * Time.deltaTime);
                    return;
                }
                path.RemoveAt (0);
                next = path[0];
            } else {
                var dir = next.GetPosition () - transform.position;
                if (dir.sqrMagnitude < closeDist * closeDist) {
                    current = next;
                }
                dir = dir.normalized * speed * speedFactor;
                transform.position += dir;

                rotationTarget.up = rotationTarget.up * 0.1f + dir * 0.9f;
            }

        } else {
            path = PathFinding.FindPath (current, targetNode);
            path.Reverse ();
        }
    }
}
