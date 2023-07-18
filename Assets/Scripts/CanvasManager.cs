using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public GameObject winScreen;
    public GameObject playScreen;
    public GameObject loseScreen;
    public UnityEvent gameWin;
    public UnityEvent gameLose;


    bool done;

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
        }
       
    }
    void Start()
    {
        done = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (BossManager.Instance == null && done)
        {

            StartCoroutine(Delay());

        }
        if (GameManager.Instance.totalCount ==0&&GridSpawner.Instance.EnemyList.Count == 0 && done)
        {
            StartCoroutine(loseDelay());
            
        }
    }

    IEnumerator Delay()
    {
        done = false;
        playScreen.SetActive(false);
        yield return new WaitForSeconds(2);
        winScreen.SetActive(true);
    }
    IEnumerator loseDelay(){
        
         done = false;
        playScreen.SetActive(false);
        yield return new WaitForSeconds(2);
        loseScreen.SetActive(true);
    }
}
