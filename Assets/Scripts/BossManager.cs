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
            animationController.SetTrigger("kick");
            Instance = this;
        }
    }
    void Start()
    {
        OnDamageTaken += OnBulletTakenLogic;
        OnDamageTaken += OnBulletTakenUI;


        attack();

    }

    // Update is called once per frame
    void Update()
    {
        if (curhealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void attack()
    {
       
    }
    public void AttackEnd()
    {
        Debug.Log("a");
        StartCoroutine(CallAnotherAttack());
    }

    public void JumpingAttackDamage()
    {
        Debug.Log("b");
        
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
