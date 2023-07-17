using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Army : MonoBehaviour
{
    public int health;
    public int damage;
    public int damageTake;
    public Image healthbar;

    private void Start() {
        healthbar.fillAmount = (float)health / 100f;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.fillAmount = (float)health / 100f;
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
        GridSpawner.Instance.EnemyList.Remove(this.gameObject);
        Destroy(gameObject);
    }


}



