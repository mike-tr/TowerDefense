using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public Node[, ] map;
    public int sizeX, sizeY;

    public NodeObject FloorPrefab;
    public NodeObject WallPrefab;
    public NodeObject PortalPrefab;

    public Monster[] monsters;
    public Vector2 center;
    // Start is called before the first frame update
    public Tower[] startingTowers;
    Dictionary<int, NodeObject> nodeObjects = new Dictionary<int, NodeObject> ();
    private List<Node> portals = new List<Node> ();

    public const int EMPTY = 0;
    public const int PORTAL = -2;
    public const int WALL = -1;

    [HideInInspector] public Transform terrainHolder, structuresHolder, monstersHolder;

    private MonsterSpawner spawner;

    void Start () {
        terrainHolder = new GameObject ("Terrain").transform;
        structuresHolder = new GameObject ("Structures").transform;
        monstersHolder = new GameObject ("Monsters").transform;

        terrainHolder.parent = transform;
        structuresHolder.parent = transform;
        monstersHolder.parent = transform;

        terrainHolder.position = Vector3.zero;
        structuresHolder.position = Vector3.zero;
        monstersHolder.position = Vector3.zero;

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
                        map[x, y] = new Node (this, x, y, PORTAL);
                        portals.Add (map[x, y]);
                        continue;
                    }
                    map[x, y] = new Node (this, x, y, WALL);
                    continue;
                }
                map[x, y] = new Node (this, x, y, EMPTY);
            }
        }

        spawner = gameObject.AddComponent<MonsterSpawner> ();
        spawner.Initialize (this, 1);
    }

    public void ResetPathAll () {
        // spawner.ResetPathForAll ();
    }

    public Monster SpawnMonster (int index) {
        var monster = Instantiate (monsters[index]);
        monster.Initialize (this, portals[0], portals[1]);
        return monster;
    }

    public Node GetPortal (int index) {
        return portals[index];
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

    public bool IsViablePath () {
        var path = PathFinding.FindPath (portals[0], portals[1]);
        return path != null;
    }
}
