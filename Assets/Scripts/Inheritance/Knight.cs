using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Knight : Army
{
    public int speed;
    
    // Custom properties for MobA

    protected override void Die()
    {
        Debug.Log("knight died");
        // Additional behavior specific to MobA when it dies
        // For example, play a specific death animation or drop unique loot
        base.Die(); // Call the base implementation as well
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("boss"))
        {
           transform.GetComponent<Animator>().SetTrigger("boss"); 
           other.GetComponent<BossManager>().health -= damage;
           TakeDamage(damageTake);
        }

    }
    private void Start() {
<<<<<<< Updated upstream
=======
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
           transform.GetComponent<Animator>().SetTrigger("run"); 
            
>>>>>>> Stashed changes
        transform.DOMove(BossManager.Instance.arena.transform.position, speed).SetSpeedBased(true);
    }
}