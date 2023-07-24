using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.Universal.Internal;
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
    public GameObject playerGameObject;
    public TextMeshPro fpsText;
    public GameObject speedUpParticle;
    public CinemachineVirtualCamera cam;
    public Camera mainCamera;
    public bool shineLoopStarted = false;
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
        
        sizePrice.text = ((int)GameDataManager.Instance.sizePrice).ToString();
        speedPrice.text =((int)GameDataManager.Instance.speedPrice).ToString();
        timePrice.text = ((int)GameDataManager.Instance.timePrice).ToString();

        totalMoney.text = GameDataManager.Instance.totalMoney.ToString();

        playerGameObject.transform.DOScale(GameDataManager.Instance.size,0.2f);
        cam.m_Lens.FieldOfView = GameDataManager.Instance.cameraLens;
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
        if (GameManager.Instance.timer < 6 && shineLoopStarted == false)
        {
            shineLoopStarted= true;
            TimerLoop();
            ShineLoop();
        }
        if (GameManager.Instance.timer < 0)
        {
            TimeUp();
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
        
        GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.sizePrice;
        UIManager.Instance.totalMoney.text = GameDataManager.Instance.totalMoney.ToString();
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
        GameDataManager.Instance.sizePrice = (int)(GameDataManager.Instance.sizePrice*1.25f);
        sizePrice.text = GameDataManager.Instance.sizePrice.ToString();
        GameDataManager.Instance.cameraLens += sizeAwardToAdd / 2f;
        cam.m_Lens.FieldOfView = GameDataManager.Instance.cameraLens;
        GameManager.Instance.player.transform.DOScale(GameDataManager.Instance.size*1.5f, 0.2f).OnComplete(() =>
        {
            GameManager.Instance.player.transform.DOScale(GameDataManager.Instance.size, 0.5f);
            PlayerManager.Instance.counterObject.transform.DOScale(GameDataManager.Instance.size, 0.5f);


        });
        ControlButtonInteractable();

        GameDataManager.Instance.SaveData();
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
        GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.sizePrice;
        UIManager.Instance.totalMoney.text = GameDataManager.Instance.totalMoney.ToString();
        GameDataManager.Instance.SpeedLevel++;
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            speedAwardToAdd = 2;
        }
        else
        {
            speedAwardToAdd = 1;

        }
        speedUpParticle.SetActive(true);
        speedL.text = "LV " + GameDataManager.Instance.SpeedLevel.ToString();
        GameDataManager.Instance.speedPrice = (int)(GameDataManager.Instance.speedPrice *1.25f);
        speedPrice.text = GameDataManager.Instance.speedPrice.ToString();
        ControlButtonInteractable();
        GameDataManager.Instance.SaveData();
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
        GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.sizePrice;
        UIManager.Instance.totalMoney.text = GameDataManager.Instance.totalMoney.ToString();
        GameDataManager.Instance.TimeLevel++;
        GameDataManager.Instance.maxTimer += timeAwardToAdd;

        GameDataManager.Instance.timePrice = (int)(GameDataManager.Instance.timePrice*1.25f);
        timerText.gameObject.transform.parent.DOPunchScale(new Vector3(0.2f,0.2f,0.2f),0.5f,1);
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
        ControlButtonInteractable();
        GameDataManager.Instance.SaveData();
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
        if (GameDataManager.Instance.totalMoney >= (int)GameDataManager.Instance.speedPrice)
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
        if (GameDataManager.Instance.totalMoney >= (int)GameDataManager.Instance.sizePrice )
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
        if (GameDataManager.Instance.totalMoney >= (int)GameDataManager.Instance.timePrice)
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
 
    public void ShineLoop()
    {
        fillObject.color= Color.red;
        fillObject.GetComponent<SpriteRenderer>().DOFade(1, 0.2F).OnComplete(() =>
        {
            fillObject.GetComponent<SpriteRenderer>().DOFade(0.2f, 0.2f).OnComplete(() =>ShineLoop());
        });
    }

    public void TimeUp()
    {
        Destroy(cam.gameObject);

        mainCamera.transform.DOMoveY(10, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            SceneManager.LoadScene(1);
        });
        
    }

    public void TimerLoop()
    {
        timerText.color = Color.red;
        timerText.gameObject.transform.DOScale(Vector3.one * 1.5f, 0.5f).OnComplete(() =>
        {
            timerText.gameObject.transform.DOScale(Vector3.one, 0.5f).OnComplete(()=>TimerLoop());
        });
    }

}
