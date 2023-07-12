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
    bool played = true;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (GameManager.Instance.knightCount <= 0 && GameManager.Instance.archerCount <= 0 && played)
        {
            transform.DOMove(arena.transform.position, 1f).OnComplete(attack);
            played = false;
        }
    }
    public void attack()
    {
        transform.DOJump(transform.position, 3f, 1, 1f).OnComplete(() =>
        {


            attack();
        });
    }


}
