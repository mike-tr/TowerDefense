using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject : MonoBehaviour {
    private static int NEXT_ID = 1;
    public int id;
    public Tower[] upgradeList;

    private SpriteRenderer sprite;
    private Color origin;

    public void Recolor (Color color) {
        if (sprite == null) {
            sprite = GetComponent<SpriteRenderer> ();
            origin = sprite.color;
        }
        sprite.color = color;
    }

    public void ResetColor () {
        if (sprite) {
            sprite.color = origin;
        }
    }

    public void SetUniqueID () {
        if (id > 0) {
            id = NEXT_ID;
            NEXT_ID++;
        }
    }
}
