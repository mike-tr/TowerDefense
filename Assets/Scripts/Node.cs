using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public readonly int x, y;
    public int type { get; private set; }
    public readonly Board board;
    public NodeObject terrain;
    public NodeObject nodeObject;

    public bool walkable {
        get {
            return type == 0;
        }
    }

    public void ColorNode (Color color) {
        terrain.Recolor (color);
    }

    public void ResetColor () {
        terrain.ResetColor ();
    }

    public Vector3 position {
        get {
            return board.transform.position + new Vector3 (x, y, 0);
        }
    }

    public Node (Board board, int x, int y, int type) {
        this.board = board;
        this.x = x;
        this.y = y;
        this.type = type;

        UpdateNode ();
    }

    public void ResetCost () {
        moves = 0;
        cost = int.MaxValue;
        pivot = null;
    }
    public int cost;
    public Node pivot;
    public int moves = 0;
    public bool UpdateCost (Node end, Node current) {
        var ncost = Mathf.Abs (end.x - x) + Mathf.Abs (end.y - y) + current.moves;
        if (cost > ncost) {
            cost = ncost;
            moves = current.moves + 1;
            pivot = current;
            return true;
        }
        return false;
    }

    public List<Node> GetNeighbours () {
        List<Node> list = new List<Node> ();
        for (int i = -1; i < 2; i++) {
            if (i == 0) {
                continue;
            }
            var node = board.GetNode (x + i, y);
            if (node != null) {
                list.Add (node);
            }
            node = board.GetNode (x, y + i);
            if (node != null) {
                list.Add (node);
            }
        }
        return list;
    }

    public void HandleUpgrade (int id) {
        var upgrades = GetUpgradeList ();
        if (upgrades.Length > 0 && upgrades.Length > id) {
            if (id == -1) {
                // sell the tower.
                type = 0;
                UpdateNode ();
            } else {
                // build the tower.
                bool enoughMoney = true;
                if (enoughMoney) {
                    type = upgrades[id].id;
                    UpdateNode ();
                }
            }
        }
    }

    public Tower[] GetUpgradeList () {
        if (nodeObject != null) {
            return nodeObject.upgradeList;
        }
        return board.defaultUpgrades;
    }

    public void UpdateNode () {
        if (terrain != null) {
            GameObject.Destroy (terrain);
        }
        terrain = board.CreateTerrainObject ();
        terrain.transform.parent = board.transform;
        terrain.transform.localPosition = new Vector2 (x, y);

        UpdateObject ();
    }

    public void UpdateObject () {
        if (nodeObject != null) {
            GameObject.Destroy (nodeObject.gameObject);
        }

        if (type == 0) {
            return;
        }

        nodeObject = board.CreateObject (type);
        nodeObject.transform.parent = terrain.transform;
        nodeObject.transform.localPosition = Vector2.zero;
    }
}
