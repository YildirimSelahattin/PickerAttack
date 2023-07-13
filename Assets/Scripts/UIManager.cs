using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject playScreen;
    public GameObject upgradeScreen;

    public TextMeshPro timerText;
    public SpriteRenderer fillObject;
    public TextMeshProUGUI sizeL;
    public TextMeshProUGUI sizePrice;
    public TextMeshProUGUI timeL;
    public TextMeshProUGUI timePrice;
    public TextMeshProUGUI speedL;
    public TextMeshProUGUI speedPrice;


    private void Start()
    {
        upgradeScreen.SetActive(true);
        playScreen.SetActive(false);
        timerText.text = (GameManager.Instance.timer * PlayerPrefs.GetInt(GameDataManager.Instance.timeLevelKey)).ToString();
        sizeL.text = PlayerPrefs.GetInt(GameDataManager.Instance.sizeLevelKey).ToString();
        timeL.text = PlayerPrefs.GetInt(GameDataManager.Instance.timeLevelKey).ToString();
        speedL.text = PlayerPrefs.GetInt(GameDataManager.Instance.speedLevelKey).ToString();

        sizePrice.text = (GameDataManager.Instance.sizePrice*Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1)).ToString();
        speedPrice.text =(GameDataManager.Instance.speedPrice*Mathf.Pow(1.25f, GameDataManager.Instance.SpeedLevel-1)).ToString();
        timePrice.text = (GameDataManager.Instance.timePrice*Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1)).ToString();

    
    }

    private void Update()
    {
        GameManager.Instance.timer = GameManager.Instance.timer - Time.deltaTime;
        timerText.text = ((int)GameManager.Instance.timer).ToString();
        fillObject.material.SetFloat("_Arc1", ((GameManager.Instance.maxTimer - GameManager.Instance.timer) / GameManager.Instance.maxTimer) * 360);
        if (GameManager.Instance.timer < 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void sizeButtonClick()

    {
        GameDataManager.Instance.SizeLevel++;
        GameDataManager.Instance.SaveData();
        sizeL.text = GameDataManager.Instance.SizeLevel.ToString();
        GameManager.Instance.scale = GameManager.Instance.scale * Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1);
        GameDataManager.Instance.sizePrice = GameDataManager.Instance.sizePrice*Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1);
        GameManager.Instance.player.transform.DOScale(GameManager.Instance.scale, 1f);
        sizePrice.text = GameDataManager.Instance.sizePrice.ToString();
    }
    public void speedButtonClick()
    {
        GameDataManager.Instance.SpeedLevel++;
        GameDataManager.Instance.SaveData();

        speedL.text = GameDataManager.Instance.SpeedLevel.ToString();

        GameManager.Instance.speed = GameManager.Instance.speed * Mathf.Pow(1.25f, GameDataManager.Instance.SpeedLevel-1);
        GameDataManager.Instance.speedPrice = GameDataManager.Instance.speedPrice*Mathf.Pow(1.25f, GameDataManager.Instance.SpeedLevel-1);
        speedPrice.text = GameDataManager.Instance.speedPrice.ToString();

    }
    public void timeButtonClick()

    {
        Debug.Log("basti");
        GameDataManager.Instance.TimeLevel++;
        GameDataManager.Instance.SaveData();

        timeL.text = GameDataManager.Instance.TimeLevel.ToString();

        GameManager.Instance.timer = GameManager.Instance.timer * Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1);
        GameDataManager.Instance.timePrice = GameDataManager.Instance.timePrice*Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1);

        GameManager.Instance.maxTimer = GameManager.Instance.timer;
        timerText.text = GameManager.Instance.timer.ToString();
        timePrice.text = GameDataManager.Instance.timePrice.ToString();


    }
    public void StartGame(){

        playScreen.SetActive(true);
        upgradeScreen.SetActive(false);
    }
    public void AddArcher()
    {

    }
    public void sceneChange()
    {
        SceneManager.LoadScene(1);
    }
}
