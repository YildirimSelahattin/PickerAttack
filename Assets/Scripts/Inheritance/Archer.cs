using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class Archer : Army
{
    public GameObject bullet;
    public int bulDMG;
    public float fireRate;
    public bool canShoot = true;
    public Animator animator;
    private bool played = true ;
    protected override void Die()
    {
        Debug.Log("archer died");
        // Additional behavior specific to MobB when it dies
        // For example, spawn additional enemies or trigger an event
        base.Die(); // Call the base implementation as well
    }

    private void Start()
    {

        if (SceneManager.GetActiveScene().name == "BossScene")
        {

           

        }
    }

    public void archerShoot()
    {
        if (canShoot)
        {
            GameObject temp = Instantiate(bullet, transform.parent);
            temp.transform.LookAt(BossManager.Instance.transform);
            temp.transform.Rotate(180f, 0f, 0f);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boss"))
        {
            TakeDamage(damageTake);
        }
    }
    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            if (GameManager.Instance.archerCount <= 0 && GameManager.Instance.knightCount <= 0 && played)
            {
                bulDMG = damage;
                canShoot = false;
                animator.SetTrigger("Shoot");
                StartCoroutine(Shoot());
                played = false;
            }


        }

    }
}
