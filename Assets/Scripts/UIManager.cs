using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshPro timerText;
    public SpriteRenderer fillObject;
    public TextMeshProUGUI archCount;
    public TextMeshProUGUI knightCount;

private void Awake() {
    if (Instance == null)
    {
        Instance =this;
    }
}
    private void Start()
    {
        timerText.text = GameManager.Instance.timer.ToString();
    }

    private void Update()
    {
        GameManager.Instance.timer =GameManager.Instance.timer - Time.deltaTime;
        timerText.text =((int) GameManager.Instance.timer).ToString();
        fillObject.material.SetFloat("_Arc1", (( GameManager.Instance.maxTimer - GameManager.Instance.timer) / GameManager.Instance.maxTimer) * 360);
        if (GameManager.Instance.timer < 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void sizeButtonClick()
    
    {
        GameManager.Instance.scale= GameManager.Instance.scale * 1.25f;
        GameManager.Instance.player.transform.DOScale(GameManager.Instance.scale,1f);
    }
    public void speedButtonClick()
    {
        GameManager.Instance.speed= GameManager.Instance.speed * 1.25f;

    }
    public void timeButtonClick()
    {
        GameManager.Instance.timer= GameManager.Instance.timer * 1.25f;
        timerText.text = GameManager.Instance.timer.ToString();

    }
    public void AddArcher()
    {
        
    }
    public void sceneChange()
    {
        SceneManager.LoadScene(1);
    }
}
