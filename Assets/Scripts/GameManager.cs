using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public int inCount = 0;
    public float speed=2;
    public float timer=2;
    public Vector3 scale = new Vector3(1,1,1);
    public int archerCount; 
    public int knightCount; 
    public int smasherCount;
    public float maxTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
