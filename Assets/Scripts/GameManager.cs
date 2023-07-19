using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public int inCount = 0;
    public int archerCount;
    public float timer;
    public int knightCount; 
    public int smasherCount;
    public int totalCount;
    public bool gameStarted;
    public List<GameObject> levelPrefabs;
    



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
        
    }
}
