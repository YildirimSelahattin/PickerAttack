using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneManager : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public GameObject cylinder;
    public static LastSceneManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
       /*
        foreach (int index in GameManager.Instance.objectList)
        {
            if (index == 1)
            {
                Instantiate(cube, transform);
            }
            if (index == 0)
            {
                Instantiate(sphere, transform);
            }
            if (index == 2)
            {
                Instantiate(cylinder, transform);
            }
            
        }
        */
    }
}
