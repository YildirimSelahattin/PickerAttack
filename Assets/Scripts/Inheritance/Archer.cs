using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Archer : Army
{
    public GameObject bullet;
    public int bulDMG;
    public float fireRate;
    public bool canShoot = true;
    public Animator animator;
    
    protected override void Die()
    {
        Debug.Log("archer died");
        // Additional behavior specific to MobB when it dies
        // For example, spawn additional enemies or trigger an event
        base.Die(); // Call the base implementation as well
    }

    private void Start()
    {
        bulDMG = damage;
        canShoot = false;
        animator.SetTrigger("Shoot");
        StartCoroutine(Shoot());
        
    }

    public void archerShoot()
    {
        if (canShoot)
        {
            GameObject temp = Instantiate(bullet, transform.parent);
            temp.GetComponent<BulletManager>().damage = bulDMG;
            temp.transform.DOMove(BossManager.Instance.transform.position, 5f).SetSpeedBased(true);
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
    {
        animator.ResetTrigger("Shoot");
       
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        animator.SetTrigger("Shoot");


    }
     private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("boss"))
        {
            TakeDamage(damageTake);
        }
     }
}
