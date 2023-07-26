using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Archer : Army
{
    public GameObject bullet;
    
    public float fireRate;
    public bool canShoot = true;
    public Animator animator;
    private bool played = true ;

    protected override void Die()
    {
        Debug.Log("archer died");
        
        base.Die(); // Call the base implementation as well
    }

    private void Start()
    {

       
    }

    public void archerShoot()
    {
        if (canShoot)
        {
            GameObject sound = new GameObject("sound");
        sound.AddComponent<AudioSource>();
        sound.GetComponent<AudioSource>().volume = 1;
        sound.GetComponent<AudioSource>().PlayOneShot(soundEffect);
        Destroy(sound, soundEffect.length);
        
            GameObject temp = Instantiate(bullet, transform.parent);
            temp.transform.LookAt(BossManager.Instance.transform);
            temp.transform.Rotate(180f, 0f, 0f);
            temp.GetComponent<BulletManager>().damage = damage;
            temp.transform.DOMove(BossManager.Instance.arrowPoints[Random.Range(0,11)].transform.position,10f).SetSpeedBased(true);
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
     
    }
    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            if (GameManager.Instance.totalCount <= 0 && played)
            {
                GridSpawner.Instance.DestroyEmptyGrids();
                canShoot = false;
                animator.SetTrigger("Shoot");
                StartCoroutine(Shoot());
                played = false;
            }
        }

    }
}
