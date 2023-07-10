using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor.Search;

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
        
        
    }

  
    // Update is called once per frame
    

   
}
