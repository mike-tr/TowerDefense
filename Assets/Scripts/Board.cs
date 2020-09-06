using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public Node[, ] map;
    public int sizeX, sizeY;

    public NodeObject FloorPrefab;
    public NodeObject WallPrefab;
    public NodeObject PortalPrefab;
    public Vector2 center;
    // Start is called before the first frame update
    public Tower[] towers;
    Dictionary<int, NodeObject> nodeObjects = new Dictionary<int, NodeObject> ();

    void Start () {
        nodeObjects.Add (PortalPrefab.id, PortalPrefab);
        foreach (var tower in towers) {
            nodeObjects.Add (tower.id, tower);
        }

        transform.position = new Vector2 (-sizeX / 2, -sizeY / 2) + center;

        map = new Node[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++) {
            for (int y = 0; y < sizeY; y++) {
                if (x == 0 || x + 1 == sizeX || y == 0 || y + 1 == sizeY) {
                    if (y == sizeY / 2) {
                        map[x, y] = new Node (this, x, y, -2);
                        continue;
                    }
                    map[x, y] = new Node (this, x, y, -1);
                    continue;
                }
                map[x, y] = new Node (this, x, y, 0);
            }
        }
    }

    public Node NodeFromWorldPoint (Vector2 pos) {
        pos -= (Vector2) transform.position - Vector2.one * 0.5f;
        if (pos.x < 0 || pos.y < 0 || pos.x > sizeX || pos.y > sizeY) {
            return null;
        }
        return map[(int) pos.x, (int) pos.y];
    }

    public NodeObject CreateTerrainObject (int type) {
        if (type == -1) {
            return Instantiate (WallPrefab);
        }
        return Instantiate (FloorPrefab);
    }

    public NodeObject CreateObject (int type) {
        if (nodeObjects.TryGetValue (type, out var value)) {
            return Instantiate (nodeObjects[type]);
        } else {
            Debug.LogError ("type " + type + " was not found");
            return null;
        }
    }

}
