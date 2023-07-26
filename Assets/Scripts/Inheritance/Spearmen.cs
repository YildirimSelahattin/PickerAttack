using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


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
            Vector3 middlePos = new Vector3(temp.transform.position.x-Random.Range(-5,5),5,(BossManager.Instance.gameObject.transform.position.z+temp.transform.position.z)/2f);
            temp.transform.parent = null;
            temp.transform.LookAt(BossManager.Instance.gameObject.transform.position);
            temp.transform.Rotate(180, 0, 0);
            temp.transform.DOScale(currentBullet.transform.localScale * 2, 1f);
            temp.transform.DOPath(new Vector3[] {middlePos,BossManager.Instance.gameObject.transform.position},10f,PathType.CatmullRom).SetSpeedBased(true).OnComplete(() =>
            {
                Destroy(temp);
            });
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }
    private IEnumerator Shoot()
    {
        animator.ResetTrigger("Shoot");
        yield return new WaitForSeconds(fireRate);
        currentBullet = Instantiate(bulletPrefab, firePoint.transform);
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
