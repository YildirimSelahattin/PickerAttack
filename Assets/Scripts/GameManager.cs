using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    
    public int archerCount;
    public float timer;
    public int knightCount; 
    public int cannonCount;
    public int spearCount;
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
        if (SceneManager.GetActiveScene().name == "SampleScene" && gameStarted && GetComponent<AudioSource>().enabled == false)
        {
            GetComponent<AudioSource>().enabled = true;
            
        }
    }
}
