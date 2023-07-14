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
            other.GetComponent<BossManager>().curhealth -= damage;
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
            if (GameManager.Instance.knightCount <= 0 && played)
            {
                transform.GetComponent<Animator>().SetTrigger("run");
                transform.LookAt(BossManager.Instance.gameObject.transform);
                transform.DOMove(BossManager.Instance.gameObject.transform.position, speed).SetSpeedBased(true).OnComplete(() =>
                {
                    transform.GetComponent<Animator>().SetTrigger("boss");
                });
                played = false;
            }


        }
    }
    public void DealDamage()
    {
        BossManager.Instance.TakeDamage(damage);
    }
}