using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    public readonly int x, y;
    public readonly BoardManager drawer;
    public Tower tower;
    public Transform floor;

    public int type;

    public Tile (BoardManager drawer, int x, int y, int type) {
        this.drawer = drawer;
        this.x = x;
        this.y = y;
        this.type = type;

        Update ();
    }

    public void Update () {
        if (floor == null) {
            floor = drawer.CreateFloor (type);
            floor.parent = drawer.transform;
            floor.localPosition = new Vector2 (x, y);
        }

        if (type > 0) {
            tower = drawer.CreateTower (type);
            tower.transform.parent = floor;
            tower.transform.localPosition = Vector2.zero;
        }
    }
}
