using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;
    public GameObject arena;
    public int health = 500;
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
        attack();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void attack(){
        transform.DOJump(transform.position,3f,1,1f).OnComplete(()=>{
        attack();
        });
    }

    
}
