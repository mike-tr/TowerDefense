using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Monster target;
    private float damage = 10;
    private float missleSpeed = 1f;

    public void Initialize (Monster target, float damage, float missleSpeed) {
        this.target = target;
        this.damage = damage;
        this.missleSpeed = missleSpeed;

        if (target == null) {
            Destroy (gameObject);
        }
    }

    int frame = 0;

    // Update is called once per frame
    void Update () {
        if (target) {
            var dir = target.transform.position - transform.position;
            dir.z = 0;
            dir = dir.normalized;
            transform.up = transform.up * 0.1f + dir * 0.9f;
            transform.position += transform.up * missleSpeed * Time.deltaTime;
            //transform.position += dir * missleSpeed * Time.deltaTime;
        } else if (frame > 5) {
            Destroy (gameObject);
        } else {
            frame++;
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (target && other.transform == target.transform) {
            target.TakeDamage (damage);
            Destroy (gameObject);
        }
    }
}
