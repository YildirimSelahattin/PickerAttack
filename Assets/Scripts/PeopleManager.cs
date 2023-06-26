using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class PeopleManager : MonoBehaviour
{
    public static PeopleManager Instance;
    public int index;

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
            transform.DOMove(BossManager.Instance.transform.position, 2f).SetSpeedBased(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
