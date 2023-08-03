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

        base.Die(); // Call the base implementation as well
    }
    private void OnTriggerEnter(Collider other)
    {


    }
    private void Start()
    {


    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            if (GameManager.Instance.totalCount <= 0 && played)
            {
                transform.GetComponent<Animator>().SetTrigger("run");
                transform.LookAt(BossManager.Instance.gameObject.transform);
                int way = UnityEngine.Random.Range(0, 11);
                transform.DOMove(BossManager.Instance.waypoints[way].transform.position, speed).SetSpeedBased(true).OnComplete(() =>
                {
                    transform.GetComponent<Animator>().SetTrigger("boss");
                });
                played = false;
            }


        }
    }
    public void DealDamage()
    {
        /*
        GameObject sound = new GameObject("sound");
        sound.AddComponent<AudioSource>();
        sound.GetComponent<AudioSource>().volume = 1;
        sound.GetComponent<AudioSource>().PlayOneShot(soundEffect);
        Destroy(sound, soundEffect.length);
       
        */BossManager.Instance.TakeDamage(damage);
    }
}