using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    public int health;
    public int damage;
    public int damageTake;
    public bool picked = false;
    public GameObject mergeObject;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Base implementation for dying behavior
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("army"))
        {
        }
    }
}



