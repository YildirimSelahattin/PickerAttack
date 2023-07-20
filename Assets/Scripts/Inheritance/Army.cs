using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Army : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int damage;
    
    public Image healthbar;
    public Material deathMat;
    public AudioClip soundEffect;

    private void Start() {
        healthbar.fillAmount = 1f;
        maxHealth = health;
        
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.fillAmount = (float) health / (float) maxHealth;
        if (health <= 0)
        {
            Die();
            Destroy(healthbar);
        }
    }

    protected virtual void Die()
    {
        // Base implementation for dying behavior
        transform.GetComponent<Animator>().SetTrigger("death");
       
        StartCoroutine(DestroyDelay());
    }


    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(1);
        
        // Destroy the enemy
                Destroy(gameObject);

        
        // Remove the enemy from the list
        GridSpawner.Instance.EnemyList.Remove(this.gameObject);
    }
}



