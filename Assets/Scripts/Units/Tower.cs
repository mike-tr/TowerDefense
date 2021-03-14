using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tower : NodeObject {
    // Start is called before the first frame update
    public float attackSpeed = 1;
    public float damage = 1;
    public float cost = 50f;
    public float range = 5f;

    public float missleSpeed = 5f;
    public float idleRotationSpeed = 20f;

    private Monster target;

    [SerializeField] private Sprite icon;
    [SerializeField] private Bullet bullet;
    private Color color;

    private LayerMask layerMask;

    [SerializeField] private float searchCD = 0.25f;
    void Start () {
        layerMask = LayerMask.NameToLayer ("Monster");

        StartCoroutine (Logic ());
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
        var t = transform.eulerAngles;
        t.z += idleRotationSpeed * Time.deltaTime;
        transform.eulerAngles = t;

    }

    IEnumerator Logic () {
        while (true) {
            if (target == null) {
                var coll = Physics2D.CircleCast (transform.position, range, Vector2.zero, layerMask);
                if (coll) {
                    target = coll.transform.GetComponent<Monster> ();
                }
                yield return new WaitForSeconds (searchCD);
                continue;
            }

            Bullet newBullet = Instantiate (bullet);
            newBullet.transform.position = transform.position;
            newBullet.Initialize (target, damage, missleSpeed);
            print ("attack " + Time.timeSinceLevelLoad);
            yield return new WaitForSeconds (1f / attackSpeed);
        }
    }

    private void OnDrawGizmos () {
        Gizmos.DrawWireSphere (transform.position, range);
    }
}
