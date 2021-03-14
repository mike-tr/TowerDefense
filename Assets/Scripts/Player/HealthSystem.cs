using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem {
    // Start is called before the first frame update

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;

    public delegate void DeathCallback ();
    public DeathCallback OnDeathCallback;

    private LifeBar lifeBar;

    public void Initialize (float maxHealth, float currentHealth, LifeBar lifeBar) {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.lifeBar = lifeBar;
    }

    public void TakeDamage (float damage) {
        currentHealth -= damage;

        if (lifeBar != null) {
            lifeBar.SetLife (currentHealth / maxHealth);
        }

        if (currentHealth < 0) {
            if (OnDeathCallback != null) {
                OnDeathCallback.Invoke ();
            }
        }
    }
}
