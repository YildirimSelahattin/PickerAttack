using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Net.Http.Headers;

public class Spearmen : Army
{
    public GameObject bulletPrefab;
    public GameObject currentBullet;
    public float fireRate;
    public bool canShoot = true;
    public Animator animator;
    public Transform firePoint;
    private bool played = true;

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
           /* GameObject sound = new GameObject("sound");
            sound.AddComponent<AudioSource>();
            sound.GetComponent<AudioSource>().volume = 1;
            sound.GetComponent<AudioSource>().PlayOneShot(soundEffect);
            Destroy(sound, soundEffect.length);
           */
            GameObject temp = currentBullet;
            temp.GetComponent<BulletManager>().damage = damage;
            Vector3 middlePos = new Vector3(transform.position.x-Random.Range(-5,5),5,(BossManager.Instance.gameObject.transform.position.z-transform.position.z)/2f);
            transform.parent = null;
            currentBullet.transform.DOPath(new Vector3[] {middlePos,BossManager.Instance.gameObject.transform.position},10f,PathType.CatmullRom).SetSpeedBased(true);
            canShoot = false;
            currentBullet = Instantiate(bulletPrefab, firePoint.transform);
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
                
                canShoot = false;
                animator.SetTrigger("Shoot");
                StartCoroutine(Shoot());
                played = false;
            }


        }

    }
}
