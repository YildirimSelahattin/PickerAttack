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
    public List<GameObject> waypoints;
    public int curhealth = 500;
    public int maxhealth = 500;
    public Animator animationController;
    public static event Action<float> OnDamageTaken;
    public Image healthbar;
    bool dead;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        dead = false;
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
            if (enemy != null)
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
                    transform.DOMove(GridSpawner.Instance.EnemyList[0].transform.position, 3f);
                    animationController.SetTrigger("run");
                    break;
                }
            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (curhealth <= 0 && dead == false)
        {
            StartCoroutine(DestroyDelay());
            //CanvasManager.Instance.winScreen.SetActive(true);
            dead = true;
        }
    }
    private IEnumerator DestroyDelay()
    {
        animationController.SetTrigger("death");
        yield return new WaitForSeconds(2);

        // Destroy the enemy
        Destroy(gameObject, 0.1f);

        yield return new WaitForSeconds(1);
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
        int temp = UnityEngine.Random.Range(0, 2);
        if (temp == 0)
        {
            animationController.SetTrigger("punch");

        }
        else
        {
            animationController.SetTrigger("kick");

        }

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
    private void OnDestroy() {
        
        foreach (GameObject enemy in GridSpawner.Instance.EnemyList)
        {
            Destroy(enemy);
        }
       // CanvasManager.Instance.winScreen.SetActive(true);
    }
    
}
