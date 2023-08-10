using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public GameObject winScreen;
    public GameObject playScreen;
    public GameObject loseScreen;
    public TextMeshProUGUI hpLeft;
    public TextMeshProUGUI tapAndHoldText;
    
    bool done;

    [Header("Audio")]
    public AudioClip gameOver;

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
        done = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (BossManager.Instance == null && done)
        {

            StartCoroutine(Delay());

        }
        if (GameManager.Instance.totalCount == 0 && GridSpawner.Instance.EnemyList.Count == 0 && done)
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
        GameDataManager.Instance.currentLevel++;
        GameDataManager.Instance.currentLevel%=11;
        if(GameDataManager.Instance.currentLevel == 0)
        {
            GameDataManager.Instance.currentLevel = 1;
        }
        GameDataManager.Instance.TotalLevel++;
    }
    IEnumerator loseDelay()
    {
        hpLeft.text = "%" +((int)(((BossManager.Instance.maxhealth - BossManager.Instance.curhealth) / (float)BossManager.Instance.maxhealth) * 100)).ToString();
        done = false;
        playScreen.SetActive(false);
        yield return new WaitForSeconds(2);
        loseScreen.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(gameOver);
    }
    public void buttonClick()
    {
        GameDataManager.Instance.SaveData();
        
        SceneManager.LoadScene(0);
    }
}
