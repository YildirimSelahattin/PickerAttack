using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public GameObject winScreen;
   

    // Start is called before the first frame update
    
    private void Awake() {
        if (Instance != null)
        {
            Instance = this;
        }
        winScreen.SetActive(false);
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BossManager.Instance == null)
        {
            winScreen.SetActive(true);
        }
    }
    
}
