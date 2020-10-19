using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int health = 10;

    public event Action<Player> onPlayerDeath;

    void Start() { 
    }
    void Update() {  
    }

    void collidedWithEnemy(Enemy enemy) {
        enemy.Attack(this);
        if(health <= 0) {
            if(onPlayerDeath != null) {
                onPlayerDeath(this);
            }
        }
    }

    void OnCollisionEnter (Collision col) {
        Enemy enemy = col.collider.gameObject.GetComponent<Enemy>();
        if(enemy) {
            collidedWithEnemy(enemy);
        }

    }

}
