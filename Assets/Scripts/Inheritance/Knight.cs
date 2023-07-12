using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;
public class Knight : Army
{
    public int speed;
    bool played = true;

    // Custom properties for MobA

    protected override void Die()
    {
        Debug.Log("knight died");
        // Additional behavior specific to MobA when it dies
        // For example, play a specific death animation or drop unique loot
        base.Die(); // Call the base implementation as well
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boss"))
        {
            transform.GetComponent<Animator>().SetTrigger("boss");
            other.GetComponent<BossManager>().health -= damage;
            TakeDamage(damageTake);
        }

    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
              
        }

    }
    private void Update() {
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            if (GameManager.Instance.knightCount <= 0 && GameManager.Instance.archerCount <= 0 && played)
            {
                 transform.GetComponent<Animator>().SetTrigger("run");
                transform.LookAt(BossManager.Instance.arena.transform);
                transform.DOMove(BossManager.Instance.arena.transform.position, speed).SetSpeedBased(true);
                played = false;
            }


        }
    }
}