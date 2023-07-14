using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Unity.VisualScripting;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;
    public int curhealth = 500;
    public int maxhealth = 500;
    public Animator animationController;
    public static event Action<float> OnDamageTaken;
    public Image healthbar;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        OnDamageTaken += OnBulletTakenLogic;
        OnDamageTaken += OnBulletTakenUI;




    }
    public void Anim()
    {
        float distance;
        bool isHit;
        foreach (GameObject enemy in GridSpawner.Instance.EnemyList)
        {
            if (enemy!=null)
            {
                distance = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distance < 10)
            {
                enemy.GetComponent<Army>().TakeDamage(enemy.GetComponent<Army>().damageTake);

                enemy.GetComponent<Army>().healthbar.fillAmount = enemy.GetComponent<Army>().health / 100f;
                isHit = true;
            }
            if (!checkDist())
            {
                transform.DOMove(GridSpawner.Instance.EnemyList[0].transform.position, 1f);
                break;
            }
            }
            
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (curhealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public bool checkDist()
    {
        foreach (GameObject enemy in GridSpawner.Instance.EnemyList)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= 10)
            {
                return true;
            }
        }
        return false;
    }

    public void AttackEnd()
    {
        Anim();
        Debug.Log("a");

        StartCoroutine(CallAnotherAttack());
    }

    public void JumpingAttackDamage()
    {
        Anim();

        Debug.Log("b");
        StartCoroutine(CallAnotherAttack());

    }
    public IEnumerator CallAnotherAttack()
    {
        yield return new WaitForSeconds(2);
        if ((int)UnityEngine.Random.Range(0, 2) == 0)
        {
            animationController.SetTrigger("punch");
        }
        else
        {
            animationController.SetTrigger("kick");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        animationController.SetTrigger("kick");

        if (other.CompareTag("bullet"))
        {

            OnDamageTaken?.Invoke((int)other.gameObject.GetComponent<BulletManager>().damage);
            Destroy(other.gameObject);
        }


    }

    public void OnBulletTakenUI(float damage)
    {
        healthbar.fillAmount = (curhealth) / (float)maxhealth;
    }

    public void OnBulletTakenLogic(float damage)
    {
        curhealth -= (int)damage;
    }
    public void TakeDamage(int damage)
    {
        OnDamageTaken?.Invoke(damage);
    }
}
