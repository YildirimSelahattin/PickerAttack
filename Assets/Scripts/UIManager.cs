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
using Unity.VisualScripting;

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

    public TextMeshProUGUI levelText;

    public TextMeshProUGUI sizeL;

    public TextMeshProUGUI sizePrice;

    public TextMeshProUGUI sizeInfo;

    public TextMeshProUGUI timeL;

    public TextMeshProUGUI timePrice;

    public TextMeshProUGUI timeInfo;

    public TextMeshProUGUI speedL;

    public TextMeshProUGUI speedPrice;

    public TextMeshProUGUI speedInfo;

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
        if (Instance == null)
        {
            Instance = this;
        }
        levelText.text =
            "LV" + GameDataManager.Instance.currentLevel.ToString();
        upgradeScreen.SetActive(true);
        playScreen.SetActive(false);
        timerText.text = GameDataManager.Instance.maxTimer.ToString();
        sizeL.text = "LV " + GameDataManager.Instance.SizeLevel.ToString();
        timeL.text = "LV " + GameDataManager.Instance.TimeLevel.ToString();
        speedL.text = "LV " + GameDataManager.Instance.SpeedLevel.ToString();

        sizePrice.text = ((int) GameDataManager.Instance.sizePrice).ToString();
        speedPrice.text =
            ((int) GameDataManager.Instance.speedPrice).ToString();
        timePrice.text = ((int) GameDataManager.Instance.timePrice).ToString();

        totalMoney.text = GameDataManager.Instance.totalMoney.ToString();

        if (GameDataManager.Instance.SizeLevel % 5 == 0)
        {
            sizeInfo.text = "+2";
        }
        else
        {
            sizeInfo.text = "+1";
        }
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            speedInfo.text = "+2 m/s";
        }
        else
        {
            speedInfo.text = "+1 m/s";
        }
        if (GameDataManager.Instance.TimeLevel % 5 == 0)
        {
            timeInfo.text = "+2 s";
        }
        else
        {
            timeInfo.text = "+1 s";
        }
        cam.m_Lens.FieldOfView = GameDataManager.Instance.cameraLens;
        ControlButtonInteractable();
    }

    private void Update()
    {
        PlayerManager.Instance.endCamera.LookAt =
            SoldierHouseManager.Instance.transform;

        if (GameManager.Instance.timer < 6 && shineLoopStarted == false)
        {
            shineLoopStarted = true;
            TimerLoop();
            ShineLoop();
        }
        if (GameManager.Instance.timer < 0)
        {
            TimeUp();
        }
        else
        {
            if (GameManager.Instance.gameStarted == true)
            {
                GameManager.Instance.timer =
                    GameManager.Instance.timer - Time.deltaTime;
                timerText.text = ((int) GameManager.Instance.timer).ToString();
                fillObject
                    .material
                    .SetFloat("_Arc1",
                    (
                    (
                    GameDataManager.Instance.maxTimer -
                    GameManager.Instance.timer
                    ) /
                    GameDataManager.Instance.maxTimer
                    ) *
                    360);
            }
        }
    }

    public void sizeButtonClick()
    {
        float sizeAwardToAdd;
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            sizeAwardToAdd = 4;
        }
        else
        {
            sizeAwardToAdd = 2;
        }
        GameDataManager.Instance.size += (sizeAwardToAdd * Vector3.one) / 10f;
        GameDataManager.Instance.totalMoney -=
            (int) GameDataManager.Instance.sizePrice;
        UIManager.Instance.totalMoney.text =
            GameDataManager.Instance.totalMoney.ToString();
        if (GameDataManager.Instance.SizeLevel % 5 == 0)
        {
            sizeAwardToAdd = 4;
        }
        else
        {
            sizeAwardToAdd = 2;
        }
        GameDataManager.Instance.SizeLevel++;
        if (GameDataManager.Instance.SizeLevel % 5 == 0)
        {
            sizeInfo.text = "+4";
        }
        else
        {
            sizeInfo.text = "+2";
        }
        sizeL.text = "LV " + GameDataManager.Instance.SizeLevel.ToString();
        GameDataManager.Instance.sizePrice =
            (int)(GameDataManager.Instance.sizePrice * 1.25f);
        sizePrice.text = GameDataManager.Instance.sizePrice.ToString();

        GameDataManager.Instance.cameraLens += 3;
        cam.m_Lens.FieldOfView = GameDataManager.Instance.cameraLens;
        PlayerManager
            .Instance
            .counterObject
            .transform
            .GetChild(0)
            .gameObject
            .transform
            .DOLocalMoveZ(PlayerManager
                .Instance
                .counterObject
                .transform
                .GetChild(0)
                .gameObject
                .transform
                .localPosition
                .z -
            0.3f,
            0.2f);
        GameManager
            .Instance
            .player
            .transform
            .DOScale(GameDataManager.Instance.size * 1.5f, 0.2f)
            .OnComplete(() =>
            {
                GameManager
                    .Instance
                    .player
                    .transform
                    .DOScale(GameDataManager.Instance.size, 0.5f);
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
        GameDataManager.Instance.totalMoney -=
            (int) GameDataManager.Instance.speedPrice;
        UIManager.Instance.totalMoney.text =
            GameDataManager.Instance.totalMoney.ToString();
        GameDataManager.Instance.SpeedLevel++;
        if (GameDataManager.Instance.SpeedLevel % 5 == 0)
        {
            speedInfo.text = "+2 m/s";
        }
        else
        {
            speedInfo.text = "+1 m/s";
        }
        speedUpParticle.SetActive(true);
        speedL.text = "LV " + GameDataManager.Instance.SpeedLevel.ToString();
        GameDataManager.Instance.speedPrice =
            (int)(GameDataManager.Instance.speedPrice * 1.25f);
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
        GameDataManager.Instance.totalMoney -=
            (int) GameDataManager.Instance.timePrice;
        UIManager.Instance.totalMoney.text =
            GameDataManager.Instance.totalMoney.ToString();
        GameDataManager.Instance.TimeLevel++;
        if (GameDataManager.Instance.TimeLevel % 5 == 0)
        {
            timeInfo.text = "+2 s";
        }
        else
        {
            timeInfo.text = "+1 s";
        }
        GameDataManager.Instance.maxTimer += timeAwardToAdd;
        GameDataManager.Instance.timePrice =
            (int)(GameDataManager.Instance.timePrice * 1.25f);
        timeButton.interactable = false;
        timerText
            .gameObject
            .transform
            .parent
            .DOPunchScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f, 1)
            .OnComplete(() =>
            {
                timeButton.interactable = true;
            });
        timerText.text = GameDataManager.Instance.maxTimer.ToString();
        timePrice.text = GameDataManager.Instance.timePrice.ToString();
        timeL.text = "LV " + GameDataManager.Instance.TimeLevel.ToString();
        ControlButtonInteractable();
        GameDataManager.Instance.SaveData();
    }

    public void StartGame()
    {
        playScreen.SetActive(true);
        upgradeScreen.SetActive(false);
        cam
            .gameObject
            .transform
            .DORotate(new Vector3(42.2f,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z),
            0.5f);
        GameManager.Instance.gameStarted = true;
        GameManager.Instance.tictocSound.Play();
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
        if (
            GameDataManager.Instance.totalMoney >=
            (int) GameDataManager.Instance.speedPrice
        )
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
        if (
            GameDataManager.Instance.totalMoney >=
            (int) GameDataManager.Instance.sizePrice
        )
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
        if (
            GameDataManager.Instance.totalMoney >=
            (int) GameDataManager.Instance.timePrice
        )
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
        fillObject.color = Color.red;
        fillObject
            .GetComponent<SpriteRenderer>()
            .DOFade(1, 0.2F)
            .OnComplete(() =>
            {
                fillObject
                    .GetComponent<SpriteRenderer>()
                    .DOFade(0.2f, 0.2f)
                    .OnComplete(() => ShineLoop());
            });
    }

    public IEnumerator endGame()
    {
        yield return new WaitForSeconds(0.9f);
    }

    public void TimeUp()
    {
        PlayerManager.Instance.endCamera.Priority = 11;

        PlayerManager
            .Instance
            .endCamera
            .transform
            .DOMoveY(6f, 1f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(1);
            });
    }

    public void TimerLoop()
    {
        timerText.color = Color.red;
        timerText
            .gameObject
            .transform
            .DOScale(Vector3.one * 1.5f, 0.5f)
            .OnComplete(() =>
            {
                timerText
                    .gameObject
                    .transform
                    .DOScale(Vector3.one, 0.5f)
                    .OnComplete(() => TimerLoop());
            });
    }
}
