using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject playScreen;
    public GameObject upgradeScreen;
    

    public TextMeshPro timerText;
    public SpriteRenderer fillObject;
    public GameObject speedButtonArrow;
    public GameObject sizeButtonArrow;
    public GameObject timeButtonArrow;

    public Button speedButton;
    public Button timeButton;
    public Button sizeButton;
    public TextMeshProUGUI sizeL;
    public TextMeshProUGUI sizePrice;
    public TextMeshProUGUI timeL;
    public TextMeshProUGUI timePrice;
    public TextMeshProUGUI speedL;
    public TextMeshProUGUI speedPrice;
    public TextMeshProUGUI totalMoney;
    public static UIManager Instance; 
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        upgradeScreen.SetActive(true);
        playScreen.SetActive(false);
        /*
        timerText.text = (GameManager.Instance.timer * PlayerPrefs.GetInt(GameDataManager.Instance.timeLevelKey)).ToString();
        sizeL.text = PlayerPrefs.GetInt(GameDataManager.Instance.sizeLevelKey).ToString();
        timeL.text = PlayerPrefs.GetInt(GameDataManager.Instance.timeLevelKey).ToString();
        speedL.text = PlayerPrefs.GetInt(GameDataManager.Instance.speedLevelKey).ToString();
        */
        sizePrice.text = ((int)(GameDataManager.Instance.sizePrice*Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1))).ToString();
        speedPrice.text =((int)(GameDataManager.Instance.speedPrice*Mathf.Pow(1.25f, GameDataManager.Instance.SpeedLevel-1))).ToString();
        timePrice.text = ((int)(GameDataManager.Instance.timePrice*Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1))).ToString();
        totalMoney.text = GameDataManager.Instance.totalMoney.ToString();
        ControlButtonInteractable();
    }

    private void Update()
    {
        
        timerText.text = ((int)GameManager.Instance.timer).ToString();
        fillObject.material.SetFloat("_Arc1", ((GameManager.Instance.maxTimer - GameManager.Instance.timer) / GameManager.Instance.maxTimer) * 360);
        if (GameManager.Instance.gameStarted == true)
        {
            GameManager.Instance.timer = GameManager.Instance.timer - Time.deltaTime;
        }
        if (GameManager.Instance.timer < 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void sizeButtonClick()

    {

        float sizeAwardToAdd; ;
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            sizeAwardToAdd = 2;
        }
        else
        {
            sizeAwardToAdd = 1;

        }
        GameManager.Instance.scale += GameManager.Instance.scale * sizeAwardToAdd / 10f;
        GameDataManager.Instance.SpeedLevel++;
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            sizeAwardToAdd = 2;
        }
        else
        {
            sizeAwardToAdd = 1;

        }
        sizeL.text = GameDataManager.Instance.SizeLevel.ToString();
        GameManager.Instance.scale = GameManager.Instance.scale * Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1);
        GameDataManager.Instance.sizePrice = GameDataManager.Instance.sizePrice*Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1);
        GameManager.Instance.player.transform.DOScale(GameManager.Instance.scale, 1f);
        sizePrice.text = GameDataManager.Instance.sizePrice.ToString();
        GameDataManager.Instance.SaveData();
        ControlButtonInteractable();
    }
    public void speedButtonClick()
    {
        float speedAwardToAdd;
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            speedAwardToAdd = 2;
        }
        else
        {
            speedAwardToAdd = 1;

        }
        GameManager.Instance.speed += GameManager.Instance.speed * speedAwardToAdd / 10f;
        GameDataManager.Instance.SpeedLevel++;
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            speedAwardToAdd = 2;
        }
        else
        {
            speedAwardToAdd = 1;

        }
        speedL.text = GameDataManager.Instance.SpeedLevel.ToString();
        GameDataManager.Instance.speedPrice = (int)(GameDataManager.Instance.speedPrice*Mathf.Pow(1.25f, GameDataManager.Instance.SpeedLevel-1));
        speedPrice.text = GameDataManager.Instance.speedPrice.ToString();

        GameDataManager.Instance.SaveData();
        ControlButtonInteractable();
    }
    public void timeButtonClick()
    {
        int timeAwardToAdd;
        if (GameDataManager.Instance.TimeLevel % 5 == 0)
        {
            timeAwardToAdd = 2;
        }
        else
        {
            timeAwardToAdd = 1;

        }
        GameDataManager.Instance.TimeLevel++;

        GameManager.Instance.timer +=timeAwardToAdd;
        GameManager.Instance.maxTimer = GameManager.Instance.timer;
        GameDataManager.Instance.timePrice = (int)(GameDataManager.Instance.timePrice*Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1));

        timerText.text = GameManager.Instance.timer.ToString();
        timePrice.text = GameDataManager.Instance.timePrice.ToString();
        timeL.text = GameDataManager.Instance.TimeLevel.ToString();

        if (GameDataManager.Instance.TimeLevel % 5 == 0)
        {
            timeAwardToAdd = 2;
        }
        else
        {
            timeAwardToAdd = 1;

        }
        GameDataManager.Instance.SaveData();
        ControlButtonInteractable();
    }
    public void StartGame(){

        playScreen.SetActive(true);
        upgradeScreen.SetActive(false);
        GameManager.Instance.gameStarted = true;
    }
    public void AddArcher()
    {

    }
    public void sceneChange()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlButtonInteractable()
    {
        //speed up button
        if(GameDataManager.Instance.totalMoney >= (int)(GameDataManager.Instance.speedPrice * Mathf.Pow(1.25f, GameDataManager.Instance.SpeedLevel - 1)))
        {
            speedButton.interactable = true;
            speedButtonArrow.SetActive(true);
        }
        else
        {
            speedButton.interactable = false;
            speedButtonArrow.SetActive(false);
        }
        //size up button
        if (GameDataManager.Instance.totalMoney >= (int)(GameDataManager.Instance.sizePrice * Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel - 1)))
        {
            sizeButton.interactable = true;
            sizeButtonArrow.SetActive(true);
        }
        else
        {
            sizeButton.interactable = false;
            sizeButtonArrow.SetActive(false);
        }
        //time up button
        if (GameDataManager.Instance.totalMoney >= (int)(GameDataManager.Instance.timePrice * Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel - 1)))
        {
            timeButton.interactable = true;
            timeButtonArrow.SetActive(true);
        }
        else
        {
            timeButton.interactable = false;
            timeButtonArrow.SetActive(false);
        }
    }
}
