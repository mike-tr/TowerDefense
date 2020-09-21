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
    public Tower[] defaultUpgrades;
    Dictionary<int, NodeObject> nodeObjects = new Dictionary<int, NodeObject> ();

    void Start () {
        var towers = Resources.LoadAll ("Towers", typeof (Tower));
        AddGameObject (PortalPrefab);
        AddGameObject (WallPrefab);
        foreach (Tower tower in towers) {
            AddGameObject (tower);
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

    private void AddGameObject (NodeObject nobject) {
        nobject.SetUniqueID ();
        nodeObjects.Add (nobject.id, nobject);
    }

    public Node NodeFromWorldPoint (Vector2 pos) {
        pos -= (Vector2) transform.position - Vector2.one * 0.5f;
        if (pos.x < 0 || pos.y < 0 || pos.x > sizeX || pos.y > sizeY) {
            return null;
        }
        return map[(int) pos.x, (int) pos.y];
    }

    public Node GetNode (int x, int y) {
        if (x < 0 || x > sizeX - 1 || y < 0 || y > sizeY - 1) {
            return null;
        }
        return map[x, y];
    }

    public NodeObject CreateTerrainObject () {
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
