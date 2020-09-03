using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public Tile[, ] map;
    public int SizeX, SizeY;

    public SpriteRenderer floorPrefab;

    public Color empty = Color.black;
    public Color Wall = Color.red;
    public Color Portal = Color.blue;

    public Tower[] towers;
    private Dictionary<int, Tower> towersByType = new Dictionary<int, Tower> ();

    public int startType = 0;

    public Vector2 center;
    // Start is called before the first frame update
    void Start () {
        transform.position = new Vector2 ((int) (-SizeX / 2), (int) (-SizeY / 2)) + center;

        foreach (var tower in towers) {
            towersByType.Add (tower.type, tower);
        }

        map = new Tile[SizeX, SizeY];
        for (int x = 0; x < SizeX; x++) {
            for (int y = 0; y < SizeY; y++) {
                if (x == 0 || x == SizeX - 1 || y == 0 || y == SizeY - 1) {
                    map[x, y] = new Tile (this, x, y, -1);
                    continue;
                }
                map[x, y] = new Tile (this, x, y, startType);
            }
        }
    }

    public Tower CreateTower (int type) {
        return Instantiate (towersByType[type]);
    }

    public Transform CreateFloor (int type) {
        SpriteRenderer sprite = Instantiate (floorPrefab);
        if (type >= 0) {
            sprite.color = empty;
        } else if (type == -1) {
            sprite.color = Wall;
        } else if (type == -2) {
            sprite.color = Portal;
        }
        return sprite.transform;
    }

    // Update is called once per frame
    void Update () {

    }
}
