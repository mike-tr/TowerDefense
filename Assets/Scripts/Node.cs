using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public readonly int x, y;
    public int type { get; private set; }
    public readonly Board board;
    public NodeObject terrain;
    public NodeObject nodeObject;
    public Node (Board board, int x, int y, int type) {
        this.board = board;
        this.x = x;
        this.y = y;
        this.type = type;

        UpdateNode ();
    }

    public void UpdateNode () {
        if (terrain != null) {
            GameObject.Destroy (terrain);
        }
        terrain = board.CreateTerrainObject (type);
        terrain.transform.parent = board.transform;
        terrain.transform.localPosition = new Vector2 (x, y);

        UpdateObject ();
    }

    public void UpdateObject () {
        if (type == 0 || type == -1) {
            return;
        }

        if (nodeObject != null) {
            GameObject.Destroy (nodeObject);
        }
        nodeObject = board.CreateObject (type);
        nodeObject.transform.parent = terrain.transform;
        nodeObject.transform.localPosition = Vector2.zero;
    }
}
