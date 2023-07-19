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
        
        timerText.text = GameDataManager.Instance.maxTimer.ToString();
        sizeL.text = "LV " + GameDataManager.Instance.SizeLevel.ToString();
        timeL.text = "LV " + GameDataManager.Instance.TimeLevel.ToString();
        speedL.text = "LV " + GameDataManager.Instance.SpeedLevel.ToString();
        
        sizePrice.text = ((int)(GameDataManager.Instance.sizePrice*Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1))).ToString();
        speedPrice.text =((int)(GameDataManager.Instance.speedPrice*Mathf.Pow(1.25f, GameDataManager.Instance.SpeedLevel-1))).ToString();
        timePrice.text = ((int)(GameDataManager.Instance.timePrice*Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1))).ToString();

        totalMoney.text = GameDataManager.Instance.totalMoney.ToString();

        ControlButtonInteractable();
    }

    private void Update()
    {
        

        if (GameManager.Instance.gameStarted == true)
        {
            GameManager.Instance.timer = GameManager.Instance.timer - Time.deltaTime;
            timerText.text = ((int)GameManager.Instance.timer).ToString();
            fillObject.material.SetFloat("_Arc1", ((GameDataManager.Instance.maxTimer - GameManager.Instance.timer) / GameDataManager.Instance.maxTimer) * 360);
        }
        if (GameManager.Instance.timer < 5)
        {
            fillObject.GetComponent<SpriteRenderer>().color = Color.red;
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
        GameDataManager.Instance.size += (sizeAwardToAdd*Vector3.one )/10f;
        GameDataManager.Instance.SizeLevel++;
        if (GameDataManager.Instance.SizeLevel % 5 == 0)
        {
            sizeAwardToAdd = 2;
        }
        else
        {
            sizeAwardToAdd = 1;

        }

        sizeL.text = "LV " + GameDataManager.Instance.SizeLevel.ToString();
        GameDataManager.Instance.sizePrice = (int)(GameDataManager.Instance.sizePrice*Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1));
        sizePrice.text = GameDataManager.Instance.sizePrice.ToString();
        GameManager.Instance.player.transform.DOScale(GameDataManager.Instance.size, 0.5f);
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
        GameDataManager.Instance.speed += speedAwardToAdd / 10f;
        GameDataManager.Instance.SpeedLevel++;
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            speedAwardToAdd = 2;
        }
        else
        {
            speedAwardToAdd = 1;

        }
        speedL.text = "LV " + GameDataManager.Instance.SpeedLevel.ToString();
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
        GameDataManager.Instance.maxTimer += timeAwardToAdd;

        GameDataManager.Instance.timePrice = (int)(GameDataManager.Instance.timePrice*Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1));

        timerText.text = GameDataManager.Instance.maxTimer.ToString();
        timePrice.text = GameDataManager.Instance.timePrice.ToString();
        timeL.text = "LV " + GameDataManager.Instance.TimeLevel.ToString();
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
        GameManager.Instance.timer = GameDataManager.Instance.maxTimer;
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
