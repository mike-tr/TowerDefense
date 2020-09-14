using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tower : NodeObject {
    // Start is called before the first frame update
    public float attackSpeed = 1;
    public float damage = 1;
    public float cost = 50f;

    [SerializeField] private Sprite icon;
    private Color color;
    void Start () {

    }

    public (Sprite, Color) GetUpgradeIcon () {
        return (icon, color);
    }

    private void OnValidate () {
        if (icon == null) {
            var renderer = GetComponent<SpriteRenderer> ();
            icon = renderer.sprite;
        }
        color = GetComponent<SpriteRenderer> ().color;
    }

    // Update is called once per frame
    void Update () {

    }
}
