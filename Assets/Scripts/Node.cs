using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public readonly int x, y;
    public int type { get; private set; }
    public readonly Board board;
    public NodeObject terrain;
    public NodeObject nodeObject;

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
