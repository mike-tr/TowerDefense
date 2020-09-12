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
    void Start () {

    }

    public Sprite GetUpgradeIcon () {
        return icon;
    }

    private void OnValidate () {
        if (icon == null) {
            var renderer = GetComponent<SpriteRenderer> ();
            icon = renderer.sprite;
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
