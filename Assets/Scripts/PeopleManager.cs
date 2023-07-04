using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor.Search;

public class PeopleManager : MonoBehaviour
{
    public static PeopleManager Instance;
    public GameObject bullet;
    public int index;
    public float fireRate;
    public bool canShoot=true;
    public Animator animator;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            if (index == 0)
            {
                transform.DOMove(BossManager.Instance.transform.position, (3-index)*2f).SetSpeedBased(true);
            }

            if (index == 1)
            {
                if (canShoot)
                {
                    //GameObject temp = Instantiate(bullet, transform);
                    //temp.transform.DOMove(BossManager.Instance.transform.position, 1f).SetSpeedBased(true);
                    //archerShoot();
                    canShoot = false;
                    animator.SetTrigger("Shoot");
                    StartCoroutine(Shoot());
                }
            }
            
        }
    }

    public void archerShoot()
    {
        if (canShoot)
        {
            GameObject temp = Instantiate(bullet,transform.parent);
            temp.transform.DOMove(BossManager.Instance.transform.position, 5f).SetSpeedBased(true);
            canShoot = false;
            animator.SetTrigger("Shoot");
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
    {
        
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        animator.SetTrigger("Idle");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
